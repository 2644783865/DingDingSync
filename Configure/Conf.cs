using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Configure
{
    [XmlRootAttribute("Root", IsNullable = false)]
    public class Root
    {
        [XmlElement]
        public DingParam DingDing;

        [XmlElement]
        public Connection ConnectParam;

        [XmlElement]
        public Apis ApiChoice;

        [XmlElement]
        public SyncParam SyncSet;
    }

    public class DingParam
    {
        public string CorpID;
        public string CorpSecret;
    }

    public class Connection
    {
        public string Server;
        public string User;
        public string Password;
        public string DataBase;
    }

    public class Apis
    {
        public int AttendSchedule;
        public int AttendSource;
        public int AttendSign;
        public int AuditResult;
    }

    public class SyncParam
    {
        public string StartTime;
        public int Interval;
    }

    //序列化类
    public class SerializeConfigure
    {
        public static void SerializeObject(string xmlfile, Root rootObject)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Root));

            using (Stream fs = new FileStream(xmlfile, FileMode.Create))
            {
                XmlWriter writer = new XmlTextWriter(fs, Encoding.UTF8);

                serializer.Serialize(writer, rootObject);
            }
        }

        public static Root DeserializeObject(string xmlfile)
        {
            Root root;
            XmlSerializer serializer = new XmlSerializer(typeof(Root));

            using (FileStream fs = new FileStream(xmlfile, FileMode.Open))
            {
                XmlReader reader = XmlReader.Create(fs);
                root = (Root)serializer.Deserialize(reader);
            }
            return root;
        }
    }
}