using System;
using System.IO;
using ExpertSystemUI.Entities.KnowledgeBases;
using ExpertSystemUI.Entities.Readers;

namespace ExpertSystemUI.Entities.FileParsers
{
    public static class RuleParser
    {
        public static void LoadRulesFromFile(KnowledgeBaseWithRules knowledgeBase, string filePath)
        {
            using FileStream fileStream = File.OpenRead(filePath);
            LoadRulesFromFile(knowledgeBase, fileStream);
        }
        
        public static void LoadRulesFromFile(KnowledgeBaseWithRules knowledgeBase, Stream stream)
        {
            using CommandReader commandReader = new CommandReader(stream);
            while (!commandReader.EndOfStream)
            {
                string command = commandReader.ReadNextCommand();
                Console.WriteLine(command);
            }
        }
    }
}