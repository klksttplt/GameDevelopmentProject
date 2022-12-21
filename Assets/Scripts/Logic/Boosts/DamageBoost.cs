using Logic.EffectsFeedbacks;
using Logic.Stats;
using UnityEngine;

namespace Logic.Boosts
{
    public class DamageBoost : Boost
    {
        [SerializeField] private StatData boostDurationStatData;

        public StatValueProvider BoostDurationStatData => boostDurationStatData.DefaultBaseValueProvider;
        
        protected override void ApplyBoostToTarget(Boostable target)
        {
            target.GetComponent<Feedbacks>()?.DamageBoostFeedbacks.PlayFeedbacks();
            base.ApplyBoostToTarget(target);
        }
    }
}