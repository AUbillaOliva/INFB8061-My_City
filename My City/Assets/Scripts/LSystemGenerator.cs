using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SVS
{
    public class LSystemGenerator : MonoBehaviour
    {
        public Rule[] rules;
        public string rootSentence;
        [Range(0, 10)]
        public int iterationLimit = 1;

        public bool randomIgnoreRuleModifier = true;
        [Range(0, 1)]
        public float chanceToIgnoreRule = 0.3f;

        private void Start()
        {
            // Muestra la sentencia creada para el sistema L.
            // Debug.Log(GenerateSentence());
        }

        public string GenerateSentence(string word = null)
        {
            if(word == null)
            {
                word = rootSentence;
            }
            return GrowRecursive(word);
        }

        private string GrowRecursive(string word, int iterationIndex = 0)
        {
            if(iterationIndex >= iterationLimit)
            {
                return word;
            }
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var c in word)
            {
                stringBuilder.Append(c);
                ProcessRulesRecursivelly(stringBuilder, c, iterationIndex);
            }

            return stringBuilder.ToString();
        }

        private void ProcessRulesRecursivelly(StringBuilder stringBuilder, char c, int iterationIndex)
        {
            foreach (var rule in rules)
            {
                if(rule.letter == c.ToString())
                {
                    if (randomIgnoreRuleModifier && iterationIndex > 1)
                    {
                        if (UnityEngine.Random.value < chanceToIgnoreRule)
                        {
                            return;
                        }
                    }
                    stringBuilder.Append(GrowRecursive(rule.GetResult(), iterationIndex + 1));
                }
            }
        }
    }

}
