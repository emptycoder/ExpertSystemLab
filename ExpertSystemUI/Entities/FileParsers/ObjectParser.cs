using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ExpertSystemUI.Entities.FileParsers
{
    public static class ObjectParser
    {
        private static readonly JsonSerializerSettings settings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };

        public static string SaveObjects<T>(LinkedList<T> concepts) =>
            JsonConvert.SerializeObject(concepts, settings);

        public static void SaveObjects<T>(string path, LinkedList<T> concepts) =>
            File.WriteAllText(path, SaveObjects(concepts));

        public static LinkedList<T> LoadObjects<T>(string path) =>
            JsonConvert.DeserializeObject<LinkedList<T>>(path);
    }
}