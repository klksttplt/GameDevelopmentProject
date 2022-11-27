using Logic.Stats;
using UnityEngine;

namespace Logic.Boosts
{
    public class SpeedBoost : Boost
    {
        [SerializeField] private StatData boostDurationStatData;

        public StatValueProvider BoostDurationStatData => boostDurationStatData.DefaultBaseValueProvider;
    }
}