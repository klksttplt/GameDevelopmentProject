using System;
using System.Collections.Generic;
using System.Linq;
using GameUtils;
using UnityEngine;

namespace Logic.Stats
{
    [CreateAssetMenu(fileName = "StatsMap.asset", menuName = "ScriptableObjects/Stats/StatsMap")]

    public class StatsMap : ScriptableObject
    {
        [SerializeField] 
        private List<StatData> mapEntries = new List<StatData>();
        
        // Public API
        
        public Dictionary<StatDef, StatData> ToDictionary_DataByDef()
        {
            return ToDictionary(
                data => data.Def, 
                data => data);
        }
        
        public Dictionary<StatDef, StatValueProvider> ToDictionary_ProviderByDef()
        {
            return ToDictionary(
                data => data.Def, 
                data => data.DefaultBaseValueProvider);
        }
        
        // Methods: Internal State
        private Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
            Func<StatData, TKey> keySelector, Func<StatData, TValue> elementSelector)
        {
            var baseMap = new Dictionary<TKey, TValue>();
            var inheritedMap = mapEntries.ToDictionary(keySelector, elementSelector);
            var mergedMap = new Dictionary<TKey, TValue>();

            var uniqueKeys = new HashSet<TKey>();
            inheritedMap.Keys.ForEach(k => uniqueKeys.Add(k)); // inherited ones first
            baseMap.Keys.ForEach(k => uniqueKeys.Add(k));
            
            foreach (var key in uniqueKeys)
            {
                if (inheritedMap.ContainsKey(key)) mergedMap.Add(key, inheritedMap[key]);
                else if (baseMap.ContainsKey(key)) mergedMap.Add(key, baseMap[key]);
            }
            return mergedMap;
        }
    }
}