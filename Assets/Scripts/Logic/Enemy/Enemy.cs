using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic.Common;
using UnityEngine;

namespace Logic.Enemy
{
    public class Enemy : ContextComponent
    {
        // Fields: Editor

        [SerializeField, Header("Patrolling")] 
        private bool isPatrolling;
        [SerializeField]
        protected Transform pointA;
        [SerializeField]
        protected Transform pointB;
        [SerializeField] 
        protected float distanceToStop;
        
        [SerializeField, Header("Items")] 
        private bool spawnItemsAfterDeath;
        [SerializeField]
        private List<GameObject> itemsToSpawn;

        // Fields: Internal State

        protected Vector3 currentTarget;

        // Services

        private IGameFactory gameFactory;
        
        // Lifecycle
        
        public override void Awake()
        {
            base.Awake();
            gameFactory = AllServices.Container.Single<IGameFactory>();
            Health.OnDied.AddListener(() =>
            {
                if (spawnItemsAfterDeath)
                {
                    spawnItemsAfterDeath = false;
                    foreach (var item in itemsToSpawn)
                        gameFactory.CreateItem(transform.position);}
                Destroy(gameObject);
            });
        }

        private void FixedUpdate()
        {
            if (Animable.IsInState("Idle") && Animable.GetBool("InCombat") == false)
                return;
            
            if(Health.IsAlive)
                Movement();
        }

        // Methods: Internal State
        
        public virtual void Movement()
        {
            if (Vector3.Distance(transform.position, pointA.position) <= distanceToStop)
            {
                currentTarget = pointB.position;
                Animable.Idle();
            }
            else if (Vector3.Distance(transform.position, pointB.position) <= distanceToStop)
            {
                currentTarget = pointA.position;
                Animable.Idle();
            }
            if(!AttackController.isHit && isPatrolling)
                Moveable.MoveTowards(currentTarget);
        }
    }
}
