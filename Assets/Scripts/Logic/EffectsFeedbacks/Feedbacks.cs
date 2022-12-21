using Logic.Common;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Logic.EffectsFeedbacks
{
    public class Feedbacks : ContextComponent
    {
        [SerializeField] private MMFeedbacks attackFeedbacks;
        [SerializeField] private MMFeedbacks attackedFeedbacks;
        [SerializeField] private MMFeedbacks deathFeedbacks;
        
        [SerializeField, Header("Optional")] private MMFeedbacks healthBoostFeedbacks;
        [SerializeField] private MMFeedbacks damageBoostFeedbacks;
        [SerializeField] private MMFeedbacks collectiblePickupFeedbacks;

        public MMFeedbacks AttackFeedbacks => attackFeedbacks;
        public MMFeedbacks AttackedFeedbacks => attackedFeedbacks;
        public MMFeedbacks DeathFeedbacks => deathFeedbacks;
        
        public MMFeedbacks HealthBoostFeedbacks => healthBoostFeedbacks;
        public MMFeedbacks DamageBoostFeedbacks => damageBoostFeedbacks;
        public MMFeedbacks CollectiblePickupFeedbacks => collectiblePickupFeedbacks;
    }
}
