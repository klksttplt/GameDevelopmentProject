using System.Collections;
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
        // change to stats in further
        [SerializeField] 
        protected float distanceToStop;
        [SerializeField] 
        protected float waitingTime;
        
        [SerializeField, Header("Items")] 
        private bool spawnItemsAfterDeath;
        [SerializeField]
        private List<Item> itemsToSpawn;

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
                Animable.Die();
                if (spawnItemsAfterDeath)
                {
                    spawnItemsAfterDeath = false;
                    foreach (var item in itemsToSpawn)
                        gameFactory.CreateItem(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), item);}
                Destroy(gameObject, 5f);
            });
            currentTarget = pointA.position;
        }

        private void FixedUpdate()
        {
            if (Animable.IsInState("Idle") && Animable.GetBool("InCombat") == false)
                return;
            
            if(Health.IsAlive)
                Movement();
        }

        // Methods: Internal State
        
        protected virtual void Movement()
        {
            if (Vector3.Distance(transform.position, currentTarget) <= distanceToStop && isPatrolling)
            {
                StartCoroutine(StopAndWait());
            }
            
            if(!AttackController.isHit && isPatrolling)
                Moveable.MoveTowards(currentTarget);
        }

        protected virtual IEnumerator StopAndWait()
        {
            // Debug.Log("Stop and wait");
            Animable.Move(false);
            Animable.Idle();
            isPatrolling = false;
            yield return new WaitForSeconds(waitingTime);

            currentTarget = currentTarget == pointA.position ? pointB.position : pointA.position;

            isPatrolling = true;
            Animable.Move(true);
            

        }
    }
}
