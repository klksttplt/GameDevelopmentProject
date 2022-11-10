using System;
using UnityEngine;

namespace Logic.Stats
{
    [Serializable]
    public struct StatData
    {
        [SerializeField]
        private StatDef statDef;
        
        [SerializeField] 
        private StatValueProvider defaultBaseValueProvider;
        
        public StatDef Def => statDef;
        
        public StatValueProvider DefaultBaseValueProvider => defaultBaseValueProvider;

        public float BaseValue => defaultBaseValueProvider.BaseValue;

    }
}