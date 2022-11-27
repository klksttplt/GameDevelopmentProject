using System;
using Logic.Common;
using UnityEngine;

namespace Logic.Boosts
{
    public class Boostable : ContextComponent
    {
        public void Boost(Boost boost)
        {
            Debug.Log(name + " got boost!");
            var statData = GetStatValue(boost.BoostStatDef);
            if (statData != null)
            {
                switch (boost.BoostType)
                {
                    case BoostType.Health:
                        Health.Heal(boost.BoostStatValue.BaseValue);
                        break;
                    case BoostType.Speed:
                        var speedBoost = boost as SpeedBoost;
                        Moveable.BoostSpeed(speedBoost.BoostStatValue.BaseValue, speedBoost.BoostDurationStatData.BaseValue);
                        break;
                    case BoostType.Damage:
                        var damageBoost = boost as DamageBoost;
                        AttackController.BoostDamage(damageBoost.BoostStatValue.BaseValue, damageBoost.BoostDurationStatData.BaseValue);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}