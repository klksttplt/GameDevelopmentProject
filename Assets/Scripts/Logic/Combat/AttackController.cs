using System.Collections;
using Animations;
using Logic.Common;
using Logic.Stats;
using UnityEngine;

namespace Logic.Combat
{
    public abstract class AttackController : ContextComponent
    {
        // Fields: Editor
        
        [SerializeField, Header("Stats")] 
        protected StatData damageValueData;
        [SerializeField, Header("Stats")] 
        protected StatData attackRechargeTimeData;
        
        // Fields: Internal State
        
        protected StatValueProvider damageValueProvider;
        protected float damageValue;
        
        protected StatValueProvider attackRechargeTimeProvider;
        protected float attackRechargeTime;
        
        // Public API
        
        public StatDef DamageStatDef => damageValueData.Def;
        public StatDef AttackRechargeTimeDef => attackRechargeTimeData.Def;

        public bool isHit;
        
        public abstract void Attack();

        public void BoostDamage(float multiplier, float duration)
        {
            damageValue *= multiplier;
            StartCoroutine(ResetBoost(multiplier, duration));
        }
        
        // Methods: Lifecycle

        public override void Awake()
        {
            base.Awake();
            var damageProvidedData = GetStatValue(DamageStatDef);
            damageValueProvider = damageProvidedData ?? damageValueData.DefaultBaseValueProvider;
            damageValue = damageValueProvider.BaseValue;
            
            var attackRechargeProvidedData = GetStatValue(AttackRechargeTimeDef);
            attackRechargeTimeProvider = attackRechargeProvidedData ?? attackRechargeTimeData.DefaultBaseValueProvider;
            attackRechargeTime = attackRechargeTimeProvider.BaseValue;
        }
        
        // Methods: Internal State

        protected abstract IEnumerator ResetAttackRoutine();

        private IEnumerator ResetBoost(float multiplier, float duration)
        {
            yield return new WaitForSeconds(duration);
            damageValue /= multiplier;
        }
    }
}
