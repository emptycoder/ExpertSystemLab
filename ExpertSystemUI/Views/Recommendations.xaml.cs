using System.Collections.Generic;
using System.Windows.Controls;
using ExpertSystemUI.Entities.KnowledgeBases;
using ExpertSystemUI.Entities.Models;

namespace ExpertSystemUI.Views
{
    public partial class Recommendations
    {
        public List<Instance> Output { get; }

        public Recommendations(Frame frame, KnowledgeBaseWithRules knowledgeBase)
        {
            Output = knowledgeBase.Conclude();
            InitializeComponent();
        }
    }
}