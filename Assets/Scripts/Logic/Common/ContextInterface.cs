using System;
using Logic.Damage;
using UnityEngine;

namespace Logic.Common
{
    public abstract class ContextInterface : MonoBehaviour
    {
        protected bool hasAwaked;
        
        // Public API
        
        public Stats.StatsHandler Stats { get; private set; }
        public bool HasStats { get; private set; }
        
        public BaseHealth Health { get; private set; }
        public bool HasHealth { get; private set; }
        
        // Lifecycle

        public virtual void Awake()
        {
            if (!hasAwaked)
            {
                Stats = GetComponent<Stats.StatsHandler>();
                HasStats = Stats != null;
                
                Health = GetComponent<BaseHealth>();
                HasHealth = Health != null;
            }
        }
    }
}