using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using KnowledgeBaseLib.Entities.KnowledgeBases;
using KnowledgeBaseLib.Entities.Models;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;

namespace ExpertSystem.Views
{
    public class Recommendations : UserControl, INotifyPropertyChanged
    {
        private KnowledgeBaseWithRules? _knowledgeBaseWithRules;
        public KnowledgeBaseWithRules? KnowledgeBaseWithRules
        {
            set
            {
                _knowledgeBaseWithRules = value;
                Update();
            }
        }
        public List<Instance>? Output { get; private set; }
        public new event PropertyChangedEventHandler? PropertyChanged;

        public Recommendations() =>
            AvaloniaXamlLoader.Load(this);

        public void Update()
        {
            Output = _knowledgeBaseWithRules?.Conclude("вивчено", true);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Output)));
        }

        private async void InputElement_OnDoubleTapped(object? sender, RoutedEventArgs e)
        {
            if (this.FindControl<ListBox>("ListOfSources").SelectedItem is not Instance instance
                || instance["посилання"].Value is not string link)
                return;
            
            var messageBox = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams{
                    ButtonDefinitions = ButtonEnum.YesNo,
                    ContentTitle = "Open link",
                    ContentMessage = link,
                    ShowInCenter = true,
                    Style = Style.None
                });
            // TODO: Add support for another platforms
            if (Application.Current.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
                return;

            var dialogResult = await messageBox.Show(desktop.MainWindow);
            if (dialogResult != ButtonResult.Yes) return;
            
            Process myProcess = new();
            try
            {
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.FileName = link;
                myProcess.Start();
            }
            catch
            {
                // ignored
            }
        }
    }
}