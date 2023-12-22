using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class BinarySerialize
{
    public static void Serialize(string path, object data)
    {
        using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }
    }
    public static Data Deserialise<Data>(string path)
    {
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Data data = (Data) formatter.Deserialize(stream);
            return data;
        }
    }
}