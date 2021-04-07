using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public static string SaveObjects<T>([NotNull] LinkedList<T> concepts) =>
            JsonConvert.SerializeObject(concepts, settings);

        public static void SaveObjects<T>([NotNull] string path, [NotNull] LinkedList<T> concepts) =>
            File.WriteAllText(path, SaveObjects(concepts));

        public static LinkedList<T> LoadObjects<T>([NotNull] string path) =>
            LoadObjects<T>(File.OpenRead(path));

        public static LinkedList<T> LoadObjects<T>([NotNull] Stream stream)
        {
            using StreamReader streamReader = new StreamReader(stream);
            string value = streamReader.ReadToEnd();
            streamReader.Close();
            return JsonConvert.DeserializeObject<LinkedList<T>>(value);
        }
    }
}