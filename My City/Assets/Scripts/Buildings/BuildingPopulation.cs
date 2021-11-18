using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPopulation : MonoBehaviour
{
    public int adults;
    public int children;

    private List<int> family;

    // Asigna con valores aleatorios los integrantes de la familia.
    private void SetFamily()
    {
        adults = UnityEngine.Random.Range(1, 3);
        children = UnityEngine.Random.Range(0, 4);
    }

    // Obtiene los valores de los integrantes de la familia.
    public List<int> GetFamily()
    {
        family.Add(adults);
        family.Add(children);
        return family;
    }

    // Obtiene la cantidad total de integrantes de la familia.
    public int GetFamilyLength()
    {
        return adults + children;
    }

    public void Start()
    {
        SetFamily();
    }
}