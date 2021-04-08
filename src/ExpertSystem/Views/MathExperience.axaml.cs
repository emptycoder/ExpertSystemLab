using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KnowledgeBaseLib.Entities.Models;

namespace ExpertSystem.Views
{
    public class MathExperience : UserControl, INotifyPropertyChanged
    {
        private static readonly PropertyChangedEventArgs listOfConceptsPropertyChanged = new(nameof(ListOfConcepts));

        private LinkedList<Concept>? _listOfConcepts;
        public LinkedList<Concept>? ListOfConcepts
        {
            get => _listOfConcepts;
            set
            {
                _listOfConcepts = value;
                PropertyChanged?.Invoke(this, listOfConceptsPropertyChanged);
            }
        }
        public new event PropertyChangedEventHandler? PropertyChanged;

        public MathExperience() =>
            AvaloniaXamlLoader.Load(this);
    }
}