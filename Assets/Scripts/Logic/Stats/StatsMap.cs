using System.Collections.Generic;
using UnityEngine;

namespace Logic.Stats
{
    [CreateAssetMenu(fileName = "StatsMap.asset", menuName = "ScriptableObjects/Stats/StatsMap")]

    public class StatsMap : ScriptableObject
    {
        [SerializeField] 
        private List<StatData> mapEntries = new List<StatData>();
    }
}