using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SVS
{
    public class RoadHelper : MonoBehaviour
    {
        // Los objetos de las calles
        public GameObject roadStraight, roadCorner, road3way, road4way, roadEnd;
        // El diccionario basicamente es una lista KEY-VALUE, donde está la posicion en el plano y el
        // objeto de la calle a colocar dentro de la posicion.
        Dictionary<Vector3Int, GameObject> dictionary = new Dictionary<Vector3Int, GameObject>();
        // Si una calle después de insertarla, tiene fallas, se agregará a esta lista hash para volver
        // a validar su posicionamiento.
        HashSet<Vector3Int> fixRoadCandidates = new HashSet<Vector3Int>();


        // Entrega la lista de las calles posicionadas en el mapa. Se puede llamar de
        // cualquier parte del codigo del proyecto para obtener la lista de las posiciones y calles del mapa
        public List<Vector3Int> GetRoadPositions()
        {
            return dictionary.Keys.ToList();
        }

        // Función que se encarga de guardar en una lista, las posiciones de cada calle.
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

        // Arregla los caminos fallados
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
