using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour
{

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    if(hit.transform.gameObject.name.Contains("House"))
                    {
                        // TODO: Establecer como contenido, la cantidad de integrantes de la familia.
                        BuildingPopulation population = hit.transform.gameObject.GetComponent<BuildingPopulation>();
                        if (population != null)
                        {
                            TooltipSystem.Show("Adultos: " + population.adults.ToString() + "\nNiños: " + population.children.ToString(), "Residencia");
                        }
                        
                    } else
                    {
                        TooltipSystem.Hide();
                    }
                }
                else
                {
                    TooltipSystem.Hide();

                }
            }
        }
    }

    // Funcionalidad encargada de mostrar la información
    private void OpenInfoDialog()
    {

    }

    private void PrintName(GameObject go)
    {
        print(go.name);
        // TODO: Open dialog
        OpenInfoDialog();
    }
}
