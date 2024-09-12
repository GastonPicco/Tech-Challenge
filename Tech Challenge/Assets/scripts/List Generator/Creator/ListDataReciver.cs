using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using Model;

namespace Creator
{
    public class ListDataReciver : MonoBehaviour
    {
        public static ListDataReciver Instance;
        public List<GameObject> positionObject;
        public List<Position> positions = new List<Position>();
        private PositionReader positionReader;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void MakePositionsSendData()
        {
            positions.Clear();
            for (int i = 0; i < positionObject.Count; ++i)
            {         
                CreatorLogic creatorLogic = positionObject[i].GetComponent<CreatorLogic>();
                creatorLogic.SendData();
                positionReader = gameObject.GetComponent<PositionReader>();
            }
            // Guardar la lista en un archivo XML
            SavePositionsToXml("PositionsData.xml");
            positionReader.LoadPositions();
        }
        public void SavePositionsToXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Position>));
            string path = Path.Combine(Application.persistentDataPath, fileName);

            // Guardar el archivo XML
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, positions); // Serializar la lista de posiciones
            }

            Debug.Log($"Lista guardada en {path}");
        }

    }
}
