using System;
using System.IO;
using UnityEngine;

public class SaveDataHandler
{
    private string _path;
    private string _fileName;

    public SaveDataHandler(string path, string fileName)
    {
        _path = path;
        _fileName = fileName;
    }

    public SaveData Load()
    {
        string fullPath = Path.Combine(_path, _fileName);

        SaveData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = string.Empty;
                using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {                    
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        dataToLoad = sr.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<SaveData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.Log("Can`t load game from file" + fullPath + '\n' + e);
                throw;
            }
        }

        return loadedData;
    }

    public void Save(SaveData data)
    {
        string fullPath = Path.Combine(_path, _fileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string storedData = JsonUtility.ToJson(data, true);

            using(FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(storedData);
                }
            }
        }
        catch(Exception e)
        {
#if DEBUG
            Debug.Log("Can`t save game to file" + fullPath + '\n' + e);
#endif
            throw;
        }
    }
}
