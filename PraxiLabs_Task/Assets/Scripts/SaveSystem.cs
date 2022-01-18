using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
   public static void Save<T>(T _object, string file_name) where T : ISave
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, file_name);
        Debug.Log("save path: " + path);
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, _object);
        stream.Close();
    }

    public static T Load<T>(string file_name) where T: ISave
    {
        string path = Path.Combine(Application.persistentDataPath, file_name);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            T data = (T)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogError("Save file not found in " + path);
            return default(T);
        }
    }
}
