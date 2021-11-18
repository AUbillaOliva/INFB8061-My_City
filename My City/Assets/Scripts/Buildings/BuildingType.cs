using System;
using System.Collections.Generic;
using UnityEngine;

namespace SVS
{
    [Serializable]
    public class BuildingType
    {
        [SerializeField]
        private GameObject[] prefabs;
        public int sizeRequired;
        public int quantity;
        public int quantityAlreadyPlaced;
        [SerializeField]
        public string type;

        public GameObject GetPrefab()
        {
            quantityAlreadyPlaced++;
            if (prefabs.Length > 1)
            {
                var random = UnityEngine.Random.Range(0, prefabs.Length);
                return prefabs[random];
            }
            return prefabs[0];
        }

        public bool IsBuildingAvaiable ()
        {
            return quantityAlreadyPlaced < quantity;
        }

        public void Reset ()
        {
            quantityAlreadyPlaced = 0;
        }

        public string GetBuildingType()
        {
            return type;
        }
    }
}
