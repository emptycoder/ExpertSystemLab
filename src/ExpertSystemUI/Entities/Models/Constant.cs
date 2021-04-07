using System.Collections.Generic;

namespace ExpertSystemUI.Entities.Models
{
    public class Constant : ItemType
    {
        public string Description { get; set; }
        public string Unit { get; set; }
        public List<Attribute> Attributes { get; set; }
    }
}