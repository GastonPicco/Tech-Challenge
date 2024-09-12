using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Interface;
using Model;
using UnityEngine;

namespace ListGeneration 
{ 
    public class PositionLoader : IPositionLoader
    {
        public List<Position> LoadPositionFromXml(string positionFileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Position>));
            string path = Path.Combine(Application.persistentDataPath, positionFileName);

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return (List<Position>)serializer.Deserialize(stream);
            }
        }
    }
}
