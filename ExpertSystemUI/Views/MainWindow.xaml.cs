using System;
using System.Windows;
using ExpertSystemUI.Entities.FileParsers;
using ExpertSystemUI.Entities.KnowledgeBases;
using Microsoft.Win32;

namespace ExpertSystemUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly KnowledgeBaseWithRules _knowledgeBase = new();
        
        public MainWindow()
        {
            InitializeComponent();
            if (_knowledgeBase.SeeValues() == string.Empty)
                Frame.Navigate(new MathExperience(Frame, _knowledgeBase));
        }

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
    }
}