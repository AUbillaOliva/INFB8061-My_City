using SVS;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LightHelper : MonoBehaviour
{

    public Light spotLight;
    Dictionary<Vector3Int, Light> dictionary = new Dictionary<Vector3Int, Light>();
    public DayNightCycle cycle;

    internal void PlaceLightsPositions(List<Vector3Int> roadPositions)
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
            rotation = Quaternion.Euler(90, 0, 0);

            foreach (Vector3Int position in roadPositions)
            {

                if (!dictionary.ContainsKey(position))
                {
                    var light = SpawnPrefab(position, rotation);
                    dictionary.Add(position, light);
                }
            }
        }


    }

    private Light SpawnPrefab(Vector3Int position, Quaternion rotation)
    {
        position.y = 2;
        var newStructure = Instantiate(spotLight, position, rotation, transform);
        return newStructure;
    }

    private Dictionary<Vector3Int, Direction> FindFreeSpacesAroundRoad(List<Vector3Int> roadPositions)
    {
        Dictionary<Vector3Int, Direction> freeSpaces = new Dictionary<Vector3Int, Direction>();
        foreach (var position in roadPositions)
        {
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var newPosition = position;
                if (freeSpaces.ContainsKey(newPosition))
                {
                    continue;
                }
                freeSpaces.Add(newPosition, PlacementHelper.GetReverseDirection(direction));
            }
        }
        return freeSpaces;
    }
}
