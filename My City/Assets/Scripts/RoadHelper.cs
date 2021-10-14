using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SVS
{
    public class RoadHelper : MonoBehaviour
    {
        public GameObject roadStraight, roadCorner, road3way, road4way, roadEnd;
        Dictionary<Vector3Int, GameObject> dictionary = new Dictionary<Vector3Int, GameObject>();
        HashSet<Vector3Int> fixRoadCandidates = new HashSet<Vector3Int>();

        public List<Vector3Int> GetRoadPositions()
        {
            return dictionary.Keys.ToList();
        }

        public void PlaceStreetPositions(Vector3 startPosition, Vector3Int direction, int length)
        {
            var rotation = Quaternion.identity;
            if (direction.x == 0)
            {
                rotation = Quaternion.Euler(0, 90, 0);
            }

            for (int i = 0; i < length; i++)
            {
                var position = Vector3Int.RoundToInt(startPosition + direction * i);
                if (dictionary.ContainsKey(position))
                {
                    continue;
                }
                var road = Instantiate(roadStraight, position, rotation, transform);
                dictionary.Add(position, road);

                if (i == 0 || i == length - 1)
                {
                    fixRoadCandidates.Add(position);
                }
            }

        }

        public void FixRoad()
        {
            foreach (var position in fixRoadCandidates)
            {
                List<Direction> neighbourDirections = PlacementHelper.findNeighbour(position, dictionary.Keys);
                Quaternion rotation = Quaternion.identity;

                if (neighbourDirections.Count == 1)
                {
                    Destroy(dictionary[position]);
                    if (neighbourDirections.Contains(Direction.Down))
                    {
                        rotation = Quaternion.Euler(0, 90, 0);
                    } else if (neighbourDirections.Contains(Direction.Left))
                    {
                        rotation = Quaternion.Euler(0, 180, 0);
                    } else if (neighbourDirections.Contains(Direction.Up))
                    {
                        rotation = Quaternion.Euler(0, -90, 0);
                    }
                    dictionary[position] = Instantiate(roadEnd, position, rotation, transform);
                }
                else if (neighbourDirections.Count == 2)
                {
                    if(neighbourDirections.Contains(Direction.Up) && neighbourDirections.Contains(Direction.Down)
                        || neighbourDirections.Contains(Direction.Right) && neighbourDirections.Contains(Direction.Left))
                    {
                        continue;
                    }
                    Destroy(dictionary[position]);
                    if (neighbourDirections.Contains(Direction.Up) && neighbourDirections.Contains(Direction.Right))
                    {
                        rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (neighbourDirections.Contains(Direction.Right) && neighbourDirections.Contains(Direction.Down))
                    {
                        rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else if (neighbourDirections.Contains(Direction.Down) && neighbourDirections.Contains(Direction.Left))
                    {
                        rotation = Quaternion.Euler(0, -90, 0);
                    }
                    dictionary[position] = Instantiate(roadCorner, position, rotation, transform);
                }
                else if (neighbourDirections.Count == 3)
                {
                    Destroy(dictionary[position]);
                    if (neighbourDirections.Contains(Direction.Right) 
                        && neighbourDirections.Contains(Direction.Down)
                        && neighbourDirections.Contains(Direction.Left))
                    {
                        rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (neighbourDirections.Contains(Direction.Down) 
                        && neighbourDirections.Contains(Direction.Left)
                        && neighbourDirections.Contains(Direction.Up))
                    {
                        rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else if (neighbourDirections.Contains(Direction.Left) 
                        && neighbourDirections.Contains(Direction.Up)
                        && neighbourDirections.Contains(Direction.Right))
                    {
                        rotation = Quaternion.Euler(0, -90, 0);
                    }
                    dictionary[position] = Instantiate(road3way, position, rotation, transform);
                } else
                {
                    Destroy(dictionary[position]);
                    dictionary[position] = Instantiate(road4way, position, rotation, transform);
                }
            }
        }

    }

}
