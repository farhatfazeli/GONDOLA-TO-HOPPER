using System.IO;
using ScriptableObjects;
using UnityEngine;

namespace Persistence
{
    public interface IDataService
    {
        public void Save(SaveData sd, bool overwrite = true);
        public SaveData Load(string name);
    }

    public class FileDataService : IDataService
    {
        private readonly ISerializer _serializer;

        private readonly string _fileName;
        private readonly string _dataPath;
        private readonly string _fileExtension;
    
        public FileDataService(ISerializer serializer)
        {
            _serializer = serializer;
            _fileName = SO_GameParameters.I.saveFileName;
            _dataPath = Application.persistentDataPath;
            Debug.Log(_dataPath);
            _fileExtension = SO_GameParameters.I.fileExtension;
        }
    
        private string GetFullFilePath(string fileName)
        {
            return Path.Combine(_dataPath, $"{fileName}.{_fileExtension}");
        }
    
        public void Save(SaveData sd, bool overwrite = true)
        {
            string fileLocation = GetFullFilePath(_fileName);
            if (!overwrite && File.Exists(fileLocation))
            {
                throw new IOException($"File {fileLocation} already exists and overwrite is set to false");
            }
            File.WriteAllText(fileLocation, _serializer.Serialize(sd));
        }

        public SaveData Load(string name)
        {
            string fileLocation = GetFullFilePath(name);
        
            if (!File.Exists(fileLocation))
            {
                throw new FileNotFoundException($"File {fileLocation} not found");
            }
        
            return _serializer.Deserialize<SaveData>(File.ReadAllText(fileLocation));
        }
    }
}