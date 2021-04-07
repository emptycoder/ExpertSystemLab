using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using ExpertSystemUI.Entities.KnowledgeBases;
using ExpertSystemUI.Entities.Models;
using ExpertSystemUI.Entities.Readers;

namespace ExpertSystemUI.Entities.FileParsers
{
    public static class RuleParser
    {
        public static void LoadRulesFromFile(KnowledgeBaseWithRules knowledgeBase, string filePath)
        {
            using FileStream fileStream = File.OpenRead(filePath);
            LoadRulesFromFile(knowledgeBase, fileStream);
        }
        
        public static void LoadRulesFromFile(KnowledgeBaseWithRules knowledgeBase, Stream stream)
        {
            using CommandReader commandReader = new CommandReader(stream);
            while (!commandReader.EndOfStream)
            {
                string command = commandReader.ReadNextCommand();
                if (string.IsNullOrWhiteSpace(command))
                    continue;
                
                var rule = knowledgeBase.PRead(command);
                knowledgeBase.Rules.AddFirst(rule);
            }
        }
        
        public static string SaveObjects([NotNull] LinkedList<LogicalAxiom> rules)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var rule in rules)
            {
                stringBuilder.Append($"якщо {rule};\n");
            }

            return stringBuilder.ToString();
        }

        public static void SaveObjects([NotNull] string path, [NotNull] LinkedList<LogicalAxiom> rules) =>
            File.WriteAllText(path, SaveObjects(rules));
    }
}