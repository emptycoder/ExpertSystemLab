using System.Collections.Generic;
using System.Windows.Controls;
using ExpertSystemUI.Entities.KnowledgeBases;
using ExpertSystemUI.Entities.Models;

namespace ExpertSystemUI.Views
{
    public partial class MathExperience
    {
        public LinkedList<Concept> ListOfConcepts { get; }
        
        public MathExperience(Frame frame, KnowledgeBase knowledgeBase)
        {
            ListOfConcepts = knowledgeBase.Concepts;
            InitializeComponent();
        }
    }
}