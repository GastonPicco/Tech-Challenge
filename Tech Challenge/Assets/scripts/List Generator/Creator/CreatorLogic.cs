using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Model;

namespace Creator
{


    public class CreatorLogic : MonoBehaviour
    {
        [SerializeField] int id;
        [SerializeField] bool isPreSet;
        [SerializeField] TMP_Text windowsName;
        [SerializeField] GameObject position;
        [SerializeField] GameObject[] seniority , baseSalary , increase, count;
        [SerializeField] string positionName;
        [SerializeField] string[] initialSeniority , initialBaseSalary , initialIncrease, initialCount;



        private void Start()
        {
    
            if (isPreSet)
            {
                position.GetComponent<TMP_InputField>().text = positionName;
                if (seniority.Length == 0 || baseSalary.Length == 0 || increase.Length == 0 || count.Length == 0)
                {
                    Debug.LogError("Los arrays no están asignados correctamente en el Inspector.");
                    return;
                }

                if (initialSeniority.Length != seniority.Length ||
                    initialBaseSalary.Length != baseSalary.Length ||
                    initialIncrease.Length != increase.Length ||
                    initialCount.Length != count.Length)
                {
                    Debug.LogError("El tamaño de los arrays no coincide con los datos iniciales.");
                    return;
                }
                for (int i = 0; i < seniority.Length; i++)
                {
                    if (seniority[i] != null && baseSalary[i] != null && increase[i] != null && count[i] != null)
                    {
                        seniority[i].GetComponent<TMP_InputField>().text = initialSeniority[i];
                        baseSalary[i].GetComponent<TMP_InputField>().text = initialBaseSalary[i];
                        increase[i].GetComponent<TMP_InputField>().text = initialIncrease[i];
                        count[i].GetComponent<TMP_InputField>().text = initialCount[i];
                    }
                    else
                    {
                        Debug.LogError($"Uno de los campos en la posición {i} no está asignado correctamente.");
                    }
                }
            }
        }
        private void Update()
        {
            UpdateName();
        }

        //actualiza ale nombre de la ventana
        public void UpdateName()
        {
            if (position.GetComponent<TMP_InputField>().text != "" && position.GetComponent<TMP_InputField>().text.Length > 1)
            {
                if (position.GetComponent<TMP_InputField>().text.Length < 12)
                {
                    windowsName.text = position.GetComponent<TMP_InputField>().text;
                }
                else 
                {
                    windowsName.text = "";
                    for (int i = 0; i < 11; i++) { windowsName.text += position.GetComponent<TMP_InputField>().text[i]; }
                    windowsName.text += "...";             
                }
            
            }
        }

        public void SendData()
        {
            if (isActiveAndEnabled)
            {
                if (ListDataReciver.Instance)
                {
                    Position newPosition = new Position();
                    string newPositionName = position.GetComponent<TMP_InputField>().text;

                    List<string> newSeniorities = new List<string>();
                    List<string> newBaseSalarys = new List<string>();
                    List<string> newIncreases = new List<string>();
                    List<string> newCounts = new List<string>();

                    for (int i = 0; i < seniority.Length; i++)
                    {
                        if (seniority[i].GetComponent<TMP_InputField>().text != "")
                        {
                            string newSeniority = seniority[i].GetComponent<TMP_InputField>().text;
                            newSeniorities.Add(newSeniority);
                        }

                        if (i < baseSalary.Length && seniority[i].GetComponent<TMP_InputField>().text != "")
                        {
                            string newBaseSalary = baseSalary[i].GetComponent<TMP_InputField>().text;
                            newBaseSalarys.Add(newBaseSalary);
                        }

                        if (i < increase.Length && seniority[i].GetComponent<TMP_InputField>().text != "")
                        {
                            string newIncrease = increase[i].GetComponent<TMP_InputField>().text;
                            newIncreases.Add(newIncrease);
                        }

                        if (i < count.Length && seniority[i].GetComponent<TMP_InputField>().text != "")
                        {
                            string newCount = count[i].GetComponent<TMP_InputField>().text;
                            newCounts.Add(newCount);
                        }
                    }

                    // Agregar la nueva posición a la lista
                    ListDataReciver.Instance.positions.Add(new Position(id, newPositionName, newSeniorities, newBaseSalarys, newIncreases, newCounts));
                }
                else
                {
                    Debug.Log("no hay lista");
                }
            }
        }
    }
}
