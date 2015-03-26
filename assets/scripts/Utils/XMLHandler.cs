using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

public class XMLHandler : MonoBehaviour
{

    private static XMLHandler _instance;

    public MovementLog _CurrentLog { get; set; }


    public static MovementLog Load(string fileName) {
        print("Loading : " + fileName);

        using (FileStream stream = new FileStream(fileName, FileMode.Open)) {
            XmlSerializer XML = new XmlSerializer(typeof(MovementLog));
            print("Done!");
            MovementLog temp = (MovementLog)XML.Deserialize(stream);
            XMLHandler.instance._CurrentLog = temp;
            return temp;
        }

    }

    public static void Save(string fileName, MovementLog _MovementLog) {
        print("Saving : " + fileName);
        try {
            using (FileStream stream = new FileStream(fileName, FileMode.CreateNew)) {
                XmlSerializer XML = new XmlSerializer(typeof(MovementLog));
                XML.Serialize(stream, _MovementLog);
                print("Done!");
            }
        }
        catch (IOException e) {
            Debug.Log(e.Message);
        }
    }


    public static XMLHandler instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<XMLHandler>();
            }
            return _instance;
        }
    }


}
