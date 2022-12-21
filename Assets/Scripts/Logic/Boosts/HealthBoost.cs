using Logic.EffectsFeedbacks;

namespace Logic.Boosts
{
    public class HealthBoost : Boost
    {
        protected override void ApplyBoostToTarget(Boostable target)
        {
            target.GetComponent<Feedbacks>().HealthBoostFeedbacks.PlayFeedbacks();
            base.ApplyBoostToTarget(target);
        }
    }
}