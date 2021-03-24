using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpertSystemUI.Entities.FileParsers;
using ExpertSystemUI.Entities.Models;
using ExpertSystemUI.Utils;
using Attribute = ExpertSystemUI.Entities.Models.Attribute;

namespace ExpertSystemUI.Entities.KnowledgeBases
{
    public class KnowledgeBase
    {
        private LinkedList<Concept> Concepts { get; } = new();
        private LinkedList<Constant> Constants { get; } = new();

        public KnowledgeBase()
        {
            Attribute attribute = new Attribute
            {
                Name = "матеріали до вивчення",
                Type = typeof(string[]),
                Value = new[] {"Англ", "Рус", "Укр"}
            };
            Concept concept = new Concept {Name = "дискретна математика"};
            Instance instance = new Instance {Name = "основи теорії графів"};
            instance.Attributes.Add(attribute);
            instance.Values.Add(new ItemType
            {
                Name = "посилання",
                Type = typeof(string),
                Value = "https://ela.kpi.ua/bitstream/123456789/35854/1/Teoriia_hrafiv.pdf"
            });
            instance.Values.Add(new ItemType
            {
                Name = "мова",
                Type = typeof(string),
                Value = "Ukr"
            });
            instance.Values.Add(new ItemType
            {
                Name = "вивчено",
                Type = typeof(bool),
                Value = false
            });
            concept.Instances.Add(instance);
            Concepts.AddFirst(concept);

            Concept mainConcept = new Concept {Name = "основні функції программи"};
            Instance planingMode = new Instance { Name = "режим планового навчання" };
            planingMode.Values.Add(new ItemType
            {
                Name = "увімкнено",
                Type = typeof(bool),
                Value = true
            });
            mainConcept.Instances.Add(planingMode);
            Concepts.AddFirst(mainConcept);

            ObjectParser.SaveObjects("testConcepts.json", Concepts);
        }

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