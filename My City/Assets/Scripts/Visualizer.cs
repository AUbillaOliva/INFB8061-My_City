using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SVS.SimpleVisualizer;

namespace SVS
{
    public class Visualizer : MonoBehaviour
    {
        public LSystemGenerator lSystem;
        List<Vector3> positions = new List<Vector3>();

        public RoadHelper roadHelper;
        public LightHelper lightHelper;
        public StructureHelper structureHelper;

        private int length = 8;
        private float angle = 90;

        private Vector3 lightsPosition;

        public int Length
        {
            get
            {
                if (length > 0)
                {
                    return length;
                }
                else
                {
                    return 1;
                }
            }
            set => length = value;
        }

        private void Start()
        {
            var sequence = lSystem.GenerateSentence();
            VisualizeSequence(sequence);
        }

        private void VisualizeSequence(string sequence)
        {
            Stack<AgentParametes> savePoints = new Stack<AgentParametes>();
            var currentPosition = Vector3.zero;
            Vector3 direction = Vector3.forward;
            Vector3 tmpPosition = Vector3.zero;

            positions.Add(currentPosition);
            foreach (var letter in sequence)
            {
                EncodingLetters encoding = (EncodingLetters)letter;
                switch (encoding)
                {
                    case EncodingLetters.save:
                        savePoints.Push(new AgentParametes
                        {
                            position = currentPosition,
                            direction = direction,
                            length = Length
                        });
                        break;
                    case EncodingLetters.load:
                        if (savePoints.Count > 0)
                        {
                            var agentParameter = savePoints.Pop();
                            currentPosition = agentParameter.position;
                            direction = agentParameter.direction;
                            Length = agentParameter.length;
                        }
                        else
                        {
                            throw new System.Exception("Don't have saved point in our stack");
                        }
                        break;
                    case EncodingLetters.draw:
                        var tempPosition = currentPosition;
                        currentPosition += direction * length;
                        roadHelper.PlaceStreetPositions(tempPosition, Vector3Int.RoundToInt(direction), length);
                        Length -= 1;
                        positions.Add(currentPosition);
                        break;
                    case EncodingLetters.turnRight:
                        direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                        break;
                    case EncodingLetters.turnLeft:
                        direction = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
                        break;
                }
                
            }
            roadHelper.FixRoad();
            structureHelper.PlaceStructuresAroundRoad(roadHelper.GetRoadPositions());
            lightHelper.PlaceLightsPositions(roadHelper.GetRoadPositions());
        }
    }
}
