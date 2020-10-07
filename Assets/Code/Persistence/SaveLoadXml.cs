using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class SaveLoadXml : ISaveLoad
{
    private readonly string saveName;

    public SaveLoadXml(string saveName = "Persistence")
    {
        this.saveName = saveName;
    }

    public GameData Load()
    {
        string xml = PlayerPrefs.GetString(saveName);

        GameData deserializedObject = new GameData();
        XmlSerializer xsSubmit = new XmlSerializer(typeof(GameData));

        try
        {
            XmlReader reader = XmlReader.Create(new StringReader(xml));
            deserializedObject = xsSubmit.Deserialize(reader) as GameData;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Unknown exception caught when xml file loading. Exception: {e.InnerException} Message: {e.Message} Stacktrace: {e.StackTrace}");
            return new GameData();
        }

        return deserializedObject;
    }

    public void Save(GameData data)
    {
        XmlSerializer xsSubmit = new XmlSerializer(typeof(GameData));
        var xml = "";

        using (var sww = new StringWriter())
        {
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, data);
                xml = sww.ToString();
            }
        }

        PlayerPrefs.SetString(saveName, xml);
        PlayerPrefs.Save();
    }
}
