using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DDD.Core.QueryService
{
    public class LocalFileReadModeRepository<T> : IReadModeRepository<T> where T : ReadMode
    {
        private readonly string _dir;

        public LocalFileReadModeRepository(string dir = "D:\\temp")
        {
            _dir = dir;
        }

        public IEnumerable<T> GetAll()
        {
            List<T> list = new List<T>();
            var filePaths = Directory.GetFiles(_dir).Where(a => a.StartsWith($"{_dir}\\{typeof(T).Name}"));
            foreach (var filePath in filePaths)
            { 
                var jsonStr = File.ReadAllText(filePath);
                var readModel = JsonConvert.DeserializeObject<T>(jsonStr,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
                list.Add(readModel);
            }
            return list;
        }

        public void Save(T t)
        {
            var path = $"{_dir}\\{typeof(T).Name}_{t.Id}";
            var stringValue = JsonConvert.SerializeObject(t,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented });
            File.WriteAllText(path, stringValue);
        }

        public T GetById(Guid id)
        {
            var path = $"{_dir}\\{typeof(T).Name}_{id}";
            var jsonStr = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(jsonStr,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }


    }
}