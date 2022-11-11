using System.Collections.Generic;
using UnityEngine;

namespace Logic.Stats
{
    public class StatsHandler : MonoBehaviour
    {
        //Fields: Editor
        
        [SerializeField] 
        private StatsMap statsMap;

        // Public API
        
        public IReadOnlyDictionary<StatDef, StatValueProvider> StatsMap => statsMap.ToDictionary_ProviderByDef();

    }
}