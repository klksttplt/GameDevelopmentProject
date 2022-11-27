using System;
using Logic.Stats;
using UnityEngine;

namespace Logic.Boosts
{
    public class Boost : MonoBehaviour
    {
        // Fields: Editor

        [SerializeField] 
        private BoostType boostType;
        [SerializeField] 
        private StatData boostStatData;

        // Public API

        public BoostType BoostType => boostType;
        public StatDef BoostStatDef => boostStatData.Def;
        public StatValueProvider BoostStatValue => boostStatData.DefaultBaseValueProvider;
        
        // Methods: Lifecycle
        
        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            var target = col.GetComponentInChildren<Boostable>();
            if(target != null)
                ApplyBoostToTarget(target);
        }

        // Methods: Internal State

        protected virtual void ApplyBoostToTarget(Boostable target)
        {
            target.Boost(this);
            Destroy(gameObject);
        }
    }

    public enum BoostType
    {
        Health,
        Speed,
        Damage
    }
}
