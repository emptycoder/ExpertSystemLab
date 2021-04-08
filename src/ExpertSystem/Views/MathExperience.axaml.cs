using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KnowledgeBaseLib.Entities.KnowledgeBases;
using KnowledgeBaseLib.Entities.Models;

namespace ExpertSystem.Views
{
    public class MathExperience : UserControl
    {
        public LinkedList<Concept> ListOfConcepts { get; }
        public MathExperience(KnowledgeBase knowledgeBase)
        {
            ListOfConcepts = knowledgeBase.Concepts;
            AvaloniaXamlLoader.Load(this);
        }

        public MathExperience()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}