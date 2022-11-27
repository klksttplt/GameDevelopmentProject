using Logic.Stats;
using UnityEngine;

namespace Logic.Boosts
{
    public class DamageBoost : Boost
    {
        [SerializeField] private StatData boostDurationStatData;

        public StatValueProvider BoostDurationStatData => boostDurationStatData.DefaultBaseValueProvider;
    }
}