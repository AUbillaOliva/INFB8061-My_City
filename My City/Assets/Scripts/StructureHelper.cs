using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS
{
    // Script encargado de colocar edificaciones cerca de las calles de la ciudad.
    public class StructureHelper : MonoBehaviour
    {
        // Diccionario con las posiciones y modelos de las edificaciones.
        public Dictionary<Vector3Int, GameObject> dictionary = new Dictionary<Vector3Int, GameObject>();
        // Tipos de edificaciones.
        public BuildingType[] buildingTypes;
        // Tipos de modelos de naturaleza.
        public GameObject[] naturePrefabs;
        // Generar aleatoriamente posiciones de modelos de naturaleza.
        public bool randomNaturePlacement = false;
        [Range(0, 1)]
        public float randomNaturePlacementThreshold = 0.3f;
        // Posicion y objetos de naturaleza.
        public Dictionary<Vector3Int, GameObject> natureDictionary = new Dictionary<Vector3Int, GameObject>();

        // Coloca edificaciones alrededor de caminos depeniendo de este mismo.
        public void PlaceStructuresAroundRoad(List<Vector3Int> roadPositions)
        {
            Dictionary<Vector3Int, Direction> freeEstateSpots = FindFreeSpacesAroundRoad(roadPositions);
            List<Vector3Int> blockedPositions = new List<Vector3Int>();
            foreach (var freeSpot in freeEstateSpots)
            {
                if (blockedPositions.Contains(freeSpot.Key))
                {
                    continue;
                }
                var rotation = Quaternion.identity;
                switch (freeSpot.Value)
                {
                    case Direction.Up:
                        rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case Direction.Down:
                        rotation = Quaternion.Euler(0, 180, 0);
                        break;
                    case Direction.Right:
                        rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case Direction.Left:
                        rotation = Quaternion.Euler(0, -90, 0);
                        break;
                    default: break;
                }

                for (int i = 0; i < buildingTypes.Length; i++)
                {
                    if (buildingTypes[i].quantity == -1)
                    {
                        if (randomNaturePlacement)
                        {
                            var random = UnityEngine.Random.value;
                            if (random < randomNaturePlacementThreshold)
                            {
                                var nature = SpawnPrefab(naturePrefabs[UnityEngine.Random.Range(0, naturePrefabs.Length)], freeSpot.Key, Quaternion.Euler(-90, 0, 0));
                                natureDictionary.Add(freeSpot.Key, nature);
                                break;
                            }
                        }
                        var newStructure = Instantiate(buildingTypes[i].GetPrefab(), freeSpot.Key, rotation, transform);
                        dictionary.Add(freeSpot.Key, newStructure);
                        break;
                    }
                    if (buildingTypes[i].IsBuildingAvaiable())
                    {
                        if (buildingTypes[i].sizeRequired > 1)
                        {
                            var halfSize = Mathf.FloorToInt(buildingTypes[i].sizeRequired / 2.0f);
                            List<Vector3Int> tmpPositionsBlocked = new List<Vector3Int>();
                            if (VerifyIfBuildingFits(halfSize, freeEstateSpots, freeSpot, blockedPositions, ref tmpPositionsBlocked))
                            {
                                blockedPositions.AddRange(tmpPositionsBlocked);
                                var building = SpawnPrefab(buildingTypes[i].GetPrefab(), freeSpot.Key, rotation);
                                dictionary.Add(freeSpot.Key, building);
                                foreach (var pos in tmpPositionsBlocked)
                                {
                                    dictionary.Add(pos, building);
                                    // TODO: Establecer familia a la propiedad solo si el tipo de estructura es residencial.
                                    if (buildingTypes[i].GetBuildingType().Equals("residential"))
                                    {
                                        Debug.Log("residential");
                                    } else
                                    {
                                        Debug.Log(buildingTypes[i].GetType().GetType());
                                    }
                                }
                                break;
                            }
                        } 
                        else
                        {
                            var building = SpawnPrefab(buildingTypes[i].GetPrefab(), freeSpot.Key, rotation);
                            Debug.Log(buildingTypes[i].GetPrefab().ToString());
                            dictionary.Add(freeSpot.Key, building);
                        }
                        break;
                    }
                }
            }
        }

        // Verifica si el tamaño de la edificacion corresponde al espacio asignado.
        private bool VerifyIfBuildingFits(
            int halfSize, 
            Dictionary<Vector3Int, Direction> freeEstateSpots, 
            KeyValuePair<Vector3Int, Direction> freeSpot, 
            List<Vector3Int> blockedPositions,
            ref List<Vector3Int> tmpPositionsBlocked
        )
        {
            Vector3Int direction = Vector3Int.zero;
            if (freeSpot.Value == Direction.Down || freeSpot.Value == Direction.Up)
            {
                direction = Vector3Int.right;
            }
            else
            {
                direction = new Vector3Int(0, 0, 1);
            }
            for (int i = 1; i <= halfSize; i++)
            {
                var pos1 = freeSpot.Key + direction * i;
                var pos2 = freeSpot.Key - direction * i;
                if (!freeEstateSpots.ContainsKey(pos1) 
                    || !freeEstateSpots.ContainsKey(pos2)
                    || blockedPositions.Contains(pos1)
                    || blockedPositions.Contains(pos2))
                {
                    return false;
                }
                tmpPositionsBlocked.Add(pos1);
                tmpPositionsBlocked.Add(pos2);
            }
            return true;
        }

        // Coloca un prefab en la posicion señalada junto con la rotacion y objeto.
        private GameObject SpawnPrefab(GameObject prefab, Vector3Int position, Quaternion rotation)
        {
            var newStructure = Instantiate(prefab, position, rotation, transform);
            return newStructure;
        }

        // Encuentra posiciones libres al rededor de las calles.
        private Dictionary<Vector3Int, Direction> FindFreeSpacesAroundRoad(List<Vector3Int> roadPositions)
        {
            // Diccionario de la direccion y posicion de los espacios libres.
            Dictionary<Vector3Int, Direction> freeSpaces = new Dictionary<Vector3Int, Direction>();
            foreach (var position in roadPositions)
            {
                // Encuentra las posiciones vecinas de cada estructura
                var neightbourDirections = PlacementHelper.findNeighbour(position, roadPositions);
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    if (!neightbourDirections.Contains(direction))
                    {
                        var newPosition = position + PlacementHelper.GetOffsetFromDirection(direction);
                        if (freeSpaces.ContainsKey(newPosition))
                        {
                            continue;
                        }
                        freeSpaces.Add(newPosition, PlacementHelper.GetReverseDirection(direction));
                    }
                }
            }
            return freeSpaces;
        }

        public void Reset()
        {
            foreach (var item in dictionary.Values)
            {
                Destroy(item);
            }
            dictionary.Clear();
            foreach (var item in natureDictionary.Values)
            {
                Destroy(item);
            }
            natureDictionary.Clear();
            foreach (var buildingType in buildingTypes)
            {
                buildingType.Reset();
            }

        }
    }
}

