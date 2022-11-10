using System;
using UnityEngine;

namespace Logic.Stats
{
    [Serializable]
    public struct StatValueProvider
    {
        // Fields: Editor
        
        [SerializeField]
        private float baseValue;
        
        [SerializeField, ]
        private float perLevelIncrement;

        // Public API
        
        public float BaseValue => baseValue;

        public float PerLevelIncrement => perLevelIncrement;
        
        public float GetValue(int upgradeLevel)
        {
            var k = perLevelIncrement * 0.01f;
            var increaseStep = baseValue * k;
            var totalIncrease = increaseStep * (upgradeLevel - 1);
            return baseValue + totalIncrease;
        }

        public int GetLevel(float value)
        {
            var k = perLevelIncrement * 0.01f;
            var increaseStep = baseValue * k;
            if (increaseStep == 0)
            {
                return Math.Abs(value - baseValue) < 0.001f ? 1 : -1;
            }
            else if (value < baseValue)
            {
                return -1;
            }
            else
            {
                var diff = value - baseValue;
                return Mathf.RoundToInt(diff / increaseStep) + 1;
            }
        }
    }
}