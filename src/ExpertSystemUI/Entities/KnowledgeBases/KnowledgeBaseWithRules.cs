using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ExpertSystemUI.Entities.Enums;
using ExpertSystemUI.Entities.Models;
using ExpertSystemUI.Entities.Models.LogicalEquations;

namespace ExpertSystemUI.Entities.KnowledgeBases
{
    public class KnowledgeBaseWithRules : KnowledgeBase
    {
        public LinkedList<LogicalAxiom> Rules { get; } = new();

        public LogicalAxiom PRead(string str)
        {
            int startIndex = 5;
            Match match = Regex.Match(str, " АБО | І |[(]|[)]| то ");
            
            LogicalEquation currentEquation = new LogicalEquation();
            Stack<LogicalEquation> equationTree = new Stack<LogicalEquation>();
            equationTree.Push(currentEquation);

            while (match.Success)
            {
                string value = match.Value;
                if (value is "(")
                {
                    if (currentEquation.FirstInstance is not null && currentEquation.SecondInstance is not null)
                        currentEquation = new LogicalEquation();
                    equationTree.Push(currentEquation);
                    startIndex = match.Index + value.Length;
                    match = match.NextMatch();
                    continue;
                }

                var parsedObj = str.Substring(startIndex, match.Index - startIndex).Split('-');
                if (parsedObj.Length == 2)
                {
                    Instance instance = FindObject(parsedObj[0].ToLower()) ??
                                        throw new NullReferenceException(parsedObj[0].ToLower());
                    var variableName = parsedObj[1].ToLower();

                    if (currentEquation.FirstInstance is null)
                    {
                        if (value is not " то ")
                            currentEquation.LogicalConnection = LogicalConnectionUtil.ToEnum(value);
                        currentEquation.FirstInstance = new LogicalEquationPart
                        {
                            Instance = instance,
                            VariableName = variableName
                        };
                    }
                    else
                    {
                        currentEquation.SecondInstance = new LogicalEquationPart
                        {
                            Instance = instance,
                            VariableName = variableName
                        };

                        if (value is not " то " && value is not ")")
                        {
                            currentEquation = new LogicalEquation
                            {
                                FirstInstance = currentEquation,
                                LogicalConnection = LogicalConnectionUtil.ToEnum(value)
                            };
                        }
                    }
                }
                
                if (value is ")")
                    currentEquation = equationTree.Pop();

                startIndex = match.Index + value.Length;
                if (value is " то ")
                    break;
                
                match = match.NextMatch();
            }
            
            return new LogicalAxiom
            {
                Name = str,
                LogicalEquationPart = equationTree.Last(),
                Result = str.Substring(startIndex).Split(" АБО ").Select(item => FindObject(item.ToLower()) ??
                    throw new NullReferenceException(item.ToLower()))
            };
        }

        public LogicalAxiom FindRule(string ruleName) =>
            Rules.FirstOrDefault(rule => rule.Name == ruleName);

        public List<Instance> Conclude<T>(string propertyName, T value)
        {
            List<Instance> instances = Rules.Where(item => 
                item.LogicalEquationPart.Result)
                .SelectMany(instance => instance.Result).Distinct().ToList();
            
            // Simplify logicalAxioms
            instances.RemoveAll(instance => 
                EqualityComparer<T>.Default.Equals((T)instance[propertyName].Value, value));

            return instances;
        }
    }
}