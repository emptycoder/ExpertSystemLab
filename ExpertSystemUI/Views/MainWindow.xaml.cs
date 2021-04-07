using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using ExpertSystemUI.Entities.FileParsers;
using ExpertSystemUI.Entities.KnowledgeBases;
using ExpertSystemUI.Entities.Models;
using ExpertSystemUI.Utils;
using Microsoft.Win32;

namespace ExpertSystemUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const string DataDirectoryName = "KDBData";
        private readonly KnowledgeBaseWithRules _knowledgeBase = new();
        
        public MainWindow()
        {
            bool firstStart = false;
            InitializeComponent();
            if (!Directory.Exists(DataDirectoryName))
                Directory.CreateDirectory(DataDirectoryName);

            string savedConceptsPath = Path.Combine(DataDirectoryName, "concepts.json");
            if (!File.Exists(savedConceptsPath)) // Load standard concepts
            {
                var loadedConcepts = ObjectParser.LoadObjects<Concept>(
                    Application.GetResourceStream(new Uri(Path.Combine("Data", "concepts.json"), UriKind.Relative))?.Stream);
                _knowledgeBase.Concepts.AddRangeFirst(loadedConcepts);
                Frame.Navigate(new MathExperience(Frame, _knowledgeBase));
                firstStart = true;
            }
            else
            {
                var loadedConcepts = ObjectParser.LoadObjects<Concept>(savedConceptsPath);
                _knowledgeBase.Concepts.AddRangeFirst(loadedConcepts);
            }

            string savedRulesPath = Path.Combine(DataDirectoryName, "rules.json");
            if (!File.Exists(savedRulesPath)) // Load standard rules
                RuleParser.LoadRulesFromFile(
                    _knowledgeBase,
                    Application.GetResourceStream(new Uri(Path.Combine("Data", "rules.dat"), UriKind.Relative))?.Stream);
            else
                RuleParser.LoadRulesFromFile(_knowledgeBase, savedRulesPath);
            
            if (!firstStart)
                Frame.Navigate(new Recommendations(Frame, _knowledgeBase));
        }
        
        private void Recommendations_OnClick(object sender, RoutedEventArgs e) =>
            Frame.Navigate(new Recommendations(Frame, _knowledgeBase));

        private void MyExperience_OnClick(object sender, RoutedEventArgs e) =>
            Frame.Navigate(new MathExperience(Frame, _knowledgeBase));

        private void LoadRules_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "DAT files|*.dat",
                Multiselect = false
            };
            var result = fileDialog.ShowDialog();
            if (result.HasValue && result.Value)
                RuleParser.LoadRulesFromFile(_knowledgeBase, fileDialog.FileName);
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show(
                    Application.Current.MainWindow ?? throw new InvalidOperationException("Can't find window!"),
                    "Save dialog", "Do u want to save knowledgeBase?", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            
            ObjectParser.SaveObjects(Path.Combine(DataDirectoryName, "concepts.json"), _knowledgeBase.Concepts);
            RuleParser.SaveObjects(Path.Combine(DataDirectoryName, "rules.dat"), _knowledgeBase.Rules);
        }
    }
}