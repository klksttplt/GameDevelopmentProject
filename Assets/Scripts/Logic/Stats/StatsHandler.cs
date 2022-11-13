using System.Collections.Generic;
using UnityEngine;

namespace Logic.Stats
{
    [DefaultExecutionOrder(-21)] // before components that use Stats
    public class StatsHandler : MonoBehaviour
    {
        //Fields: Editor
        
        [SerializeField] 
        private StatsMap statsMap;

        // Public API
        
        public IReadOnlyDictionary<StatDef, StatValueProvider> StatsMap => statsMap.ToDictionary_ProviderByDef();

    }
}