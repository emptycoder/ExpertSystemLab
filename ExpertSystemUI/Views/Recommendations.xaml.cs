using System.Collections.Generic;
using System.Windows.Controls;
using ExpertSystemUI.Entities.KnowledgeBases;
using ExpertSystemUI.Entities.Models;

namespace ExpertSystemUI.Views
{
    public partial class Recommendations
    {
        public List<Instance> Output { get; } = new();

        public Recommendations(Frame frame, KnowledgeBaseWithRules knowledgeBase)
        {
            foreach (var item in knowledgeBase.Conclude())
                Output.AddRange(item.Result);

            InitializeComponent();
        }
    }
}