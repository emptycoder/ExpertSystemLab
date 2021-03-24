using System.Collections.Generic;
using ExpertSystemUI.Entities.Models;

namespace ExpertSystemUI.Entities.KnowledgeBases
{
    public class KnowledgeBaseWithRules : KnowledgeBase
    {
        public LinkedList<LogicalAxiom> Rules { get; } = new();

        public void PRead(string str)
        {
            
        }

        public void EnterRule()
        {
            
        }
    }
}