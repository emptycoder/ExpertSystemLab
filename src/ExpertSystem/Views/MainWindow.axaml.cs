using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using KnowledgeBaseLib.Entities.FileParsers;
using KnowledgeBaseLib.Entities.KnowledgeBases;
using KnowledgeBaseLib.Entities.Models;
using KnowledgeBaseLib.Utils;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;

namespace ExpertSystem.Views
{
    public class MainWindow : Window
    {
        private static readonly IAssetLoader assetLoader = AvaloniaLocator.Current.GetService<IAssetLoader>();
        private const string DataDirectoryName = "KDBData";
        private bool _isExitDialogProceed;
        private readonly TabControl _menuControl;
        private readonly KnowledgeBaseWithRules _knowledgeBase = new();
        
        // Pages
        private readonly Recommendations _recommendations;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            _menuControl = this.FindControl<TabControl>("Menu");
            _recommendations = this.FindControl<Recommendations>("Recommendations");
            _recommendations.KnowledgeBaseWithRules = _knowledgeBase;
            this.FindControl<MathExperience>("MathExperience").ListOfConcepts = _knowledgeBase.Concepts;

            if (!Directory.Exists(DataDirectoryName))
                Directory.CreateDirectory(DataDirectoryName);

            string savedConceptsPath = Path.Combine(DataDirectoryName, "concepts.json");
            
            if (!File.Exists(savedConceptsPath)) // Load standard concepts
            {
                LinkedList<Concept>? loadedConcepts = ObjectParser.LoadObjects<Concept>(assetLoader.Open(
                    new Uri($"avares://{nameof(ExpertSystem)}/Data/concepts.json")));
                _knowledgeBase.Concepts.AddRangeFirst(loadedConcepts);
                _menuControl.SelectedIndex = 1;
            }
            else
            {
                LinkedList<Concept>? loadedConcepts = ObjectParser.LoadObjects<Concept>(savedConceptsPath);
                _knowledgeBase.Concepts.AddRangeFirst(loadedConcepts);
            }

            string savedRulesPath = Path.Combine(DataDirectoryName, "rules.json");
            if (!File.Exists(savedRulesPath)) // Load standard rules
                RuleParser.LoadRulesFromFile(
                    _knowledgeBase,
                    assetLoader.Open(new Uri($"avares://{nameof(ExpertSystem)}/Data/rules.dat")));
            else
                RuleParser.LoadRulesFromFile(_knowledgeBase, savedRulesPath);

            if (_menuControl.SelectedIndex == 1) return;
            
            _menuControl.SelectedIndex = 0;
            _recommendations.Update();
        }

        private void InitializeComponent() =>
            AvaloniaXamlLoader.Load(this);

        private async void LoadRules_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new()
            {
                Filters = new List<FileDialogFilter>
                { 
                    new() {
                    Name = "DAT files",
                    Extensions = new List<string> { ".dat" }}
                },
                AllowMultiple = false
            };
            string[] result = await fileDialog.ShowAsync(this);
            if (result is not null && result.Length == 1)
                RuleParser.LoadRulesFromFile(_knowledgeBase, result[0]);
            _menuControl.SelectedIndex = 0;
            _recommendations.Update();
        }
        
        private async void Window_OnClosing(object sender, CancelEventArgs e)
        {
            if (_isExitDialogProceed)
                return;
            
            var messageBox = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams{
                    ButtonDefinitions = ButtonEnum.YesNo,
                    ContentTitle = "Save dialog",
                    ContentMessage = "Save changes?",
                    ShowInCenter = true,
                    Style = Style.None
                });
            e.Cancel = true;
            var dialogResult = await messageBox.Show(this);
            if (dialogResult == ButtonResult.Yes)
            {
                ObjectParser.SaveObjects(Path.Combine(DataDirectoryName, "concepts.json"), _knowledgeBase.Concepts);
                RuleParser.SaveObjects(Path.Combine(DataDirectoryName, "rules.dat"), _knowledgeBase.Rules);
            }

            _isExitDialogProceed = true;
            Close();
        }

        private void UpdateRecommendations_OnTapped(object? sender, RoutedEventArgs e) =>
            _recommendations.Update();
    }
}