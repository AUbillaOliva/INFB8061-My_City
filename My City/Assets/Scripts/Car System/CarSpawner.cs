using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

    public GameObject[] carPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(SelectACarPrefab(), transform);
    }

    
    private GameObject SelectACarPrefab()
    {
        var random = Random.Range(0, carPrefabs.Length);
        return carPrefabs[random];
    }
}
