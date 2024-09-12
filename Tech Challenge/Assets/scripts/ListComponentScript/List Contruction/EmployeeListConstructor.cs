using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Xml.Linq;
using UnityEngine.UIElements;
using Interface;
using Model;
using ListGeneration;

namespace ListComponent
{
    public class EmployeeListConstructor : MonoBehaviour
    {
        public GameObject employeePrefab , categoiePrefab , seniorityPrefab; // Prefab del elemento de UI
        public Dictionary<string,GameObject> ListCategorie;
        public Scrollbar scrollbar;
        public Transform contentParent; // El contenedor donde se generarán los elementos de UI

        private string employeesFileName = "EmployeesData.xml";
        private string positionsFileName = "PositionsData.xml";
        private IEmployeeLoader employeeLoader;
        private IPositionLoader positionLoader;


        //lista de empleados
        private List<Employee> employees = new List<Employee>();
        //lista de posiciones y sus caracteristicas
        private List<Model.Position> positions = new List<Model.Position>();


        public void LoadEmployeesFromXml()
        {
            employeeLoader = new EmployeeLoader();
            positionLoader = new PositionLoader();
            employees = employeeLoader.LoadEmployeesFromXml(employeesFileName);
            positions = positionLoader.LoadPositionFromXml(positionsFileName);
            GenerateEmployeeList();
        }
        public void GenerateListCategories()
        {
            ListCategorie = new Dictionary<string, GameObject>();
            positionLoader = new PositionLoader();
            positions = positionLoader.LoadPositionFromXml(positionsFileName);
            foreach (Model.Position position in positions)
            {
                 GameObject childPanel = GetComponentsToConstruct(categoiePrefab, contentParent, position , -1);


                //logica seniorityes
                for (int i = 0; i < position.Seniorities.Count; i++) 
                {
                     GameObject childsubPanel = GetComponentsToConstruct(seniorityPrefab, childPanel.transform, position , i);
                     ListCategorie.Add( position.PositionName + position.Seniorities[i] , childsubPanel);
                     childsubPanel.SetActive(false);
                }
            }

            //Debug.Log(ListCategorie.Values);
        }
        private GameObject GetComponentsToConstruct(GameObject prefab, Transform parent, Model.Position position, int i)
        {
            GameObject newCategory = Instantiate(prefab, parent);
            newCategory.name = position.PositionName;
            FindPanel findPanel = newCategory.GetComponent<FindPanel>();
            GameObject childPanel = findPanel.GetPanel();
            var button = newCategory.GetComponentInChildren<UnityEngine.UI.Button>();
            Transform secondtextButton = button.transform.GetChild(1);
            TMP_Text Secondtext = secondtextButton.GetComponent<TMP_Text>();
            var buttonText = button.GetComponentInChildren<TMP_Text>();

        
            if (i == -1)
            {
                int EmployeesCount = GetCount(position);
                buttonText.text = $"{position.PositionName}";
                Secondtext.text = $"({EmployeesCount})";
            }
            else {
                int EmployeesCount = GetCountByPositionAndSeniority(position , i);
                buttonText = button.GetComponentInChildren<TMP_Text>();
                buttonText.text = $"{position.Seniorities[i]}";
                Secondtext.text = $"({EmployeesCount})";
            }

            childPanel.SetActive(false);
            RectTransform rect = newCategory.transform.GetComponent<RectTransform>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            Canvas.ForceUpdateCanvases();
            return (childPanel);
        }
        public int GetCount(Model.Position position)
        {
            string employeesFilePath = Path.Combine(Application.persistentDataPath, employeesFileName);

            XDocument doc = XDocument.Load(employeesFilePath);
            int EmployeesCount = doc.Descendants("Employee")
                                    .Where(emp => (string)emp.Element("Position") == position.PositionName)
                                    .Count();
            return EmployeesCount;
        }
        public int GetCountByPositionAndSeniority(Model.Position position, int i)
        {
            string employeesFilePath = Path.Combine(Application.persistentDataPath, employeesFileName);

            XDocument doc = XDocument.Load(employeesFilePath);

            int EmployeesCount = doc.Descendants("Employee")
                                    .Where(emp => (string)emp.Element("Position") == position.PositionName &&
                                                  (string)emp.Element("Seniority") == position.Seniorities[i])
                                    .Count();

            return EmployeesCount;
        }

        private void GenerateEmployeeList()
        {
            DestroyAllChildren();
            GenerateListCategories();
            foreach (Employee employee in employees)
            {
                // Instanciar el prefab
                if (ListCategorie.TryGetValue(employee.Position + employee.Seniority, out GameObject categoryGameObject)) 
                {
                    GameObject listElement = Instantiate(employeePrefab, categoryGameObject.transform);
                    //Debug.Log("se intento instanciar");

                    // Obtener el script y configurar los datos del empleado
                    EmployeeUI uiComponent = listElement.GetComponent<EmployeeUI>();
                    if (uiComponent != null)
                    {
                        uiComponent.SetEmployeeData(employee, positions);
                    }

                }
                else
                {
                    Debug.LogWarning($"No se encontró la clave {employee.Position + employee.Seniority} en ListCategorie.");
                }
            }

        }

        public void DestroyAllChildren()
        {
            foreach (Transform child in contentParent)
            {
                Destroy(child.gameObject);
            }
        }
    }

}
