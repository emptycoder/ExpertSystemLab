using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using ExpertSystemUI.Entities.FileParsers;
using ExpertSystemUI.Entities.Models;
using ExpertSystemUI.Utils;
using Attribute = ExpertSystemUI.Entities.Models.Attribute;

namespace ExpertSystemUI.Entities.KnowledgeBases
{
    public class KnowledgeBase
    {
        public LinkedList<Concept> Concepts { get; } = new();
        public LinkedList<Constant> Constants { get; } = new();

        public LinkedListNode<Concept> MakeNode(Concept concept) =>
            Concepts.AddFirst(concept);

        public Instance FindObject(string objectName) =>
            Concepts.SelectMany(concept => concept.Instances).FirstOrDefault(instance => instance.Name == objectName);

        public static string[] Split(string str) =>
            str.Split('=');

        public bool Test(string objectName, string valueName)
        {
            var instance = FindObject(objectName);
            return instance != null && instance.Values.Any(instanceValue => instanceValue.Name == valueName);
        }

        public void AddObject(string conceptName, string objectName, ItemType itemType)
        {
            var concept = Concepts.FirstOrDefault(item => item.Name == conceptName);
            if (concept is null)
                Concepts.AddFirst(concept = new Concept {Name = conceptName});
            
            Instance instance = new Instance { Name = objectName };
            instance.Values.Add(itemType);
            concept.Instances.Insert(0, instance);
        }
        
        public string SeeObject()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var conceptInstance in Concepts.SelectMany(concept => concept.Instances))
            {
                stringBuilder.Append(conceptInstance);
            }

            return stringBuilder.ToString();
        }

        public string SeeValues()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var conceptInstance in Concepts.SelectMany(concept => concept.Instances))
            {
                foreach (var conceptInstanceValue in conceptInstance.Values)
                {
                    stringBuilder.Append(conceptInstanceValue);
                }
            }

            return stringBuilder.ToString();
        }

        public override string ToString() =>
            SeeValues();
    }
}