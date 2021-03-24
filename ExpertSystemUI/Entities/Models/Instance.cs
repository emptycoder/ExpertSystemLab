﻿using System.Collections.Generic;

namespace ExpertSystemUI.Entities.Models
{
    public class Instance
    {
        public string Name { get; init; }
        public List<Attribute> Attributes { get; } = new();
        public List<ItemType> Values { get; } = new();

        public override string ToString() =>
            $"{nameof(Name)}: {Name}, {nameof(Attributes)}: {string.Join(',', Attributes)}, {nameof(Values)}: {string.Join(',', Values)}";
    }
}