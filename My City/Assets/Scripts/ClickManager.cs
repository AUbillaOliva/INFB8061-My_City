using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
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
                    PrintName(hit.transform.gameObject);
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
        // TODO: Open dialog
        OpenInfoDialog();
    }
}
