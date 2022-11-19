using System;
using System.Collections;
using Infrastructure.Factory;
using Infrastructure.Services;
using Logic.Combat.Player;
using UnityEngine;

namespace Logic.Combat.Enemy
{
    public class EnemyAttackController : AttackController
    {
        // Fields: Editor
        
        [SerializeField] 
        private Blade blade;
        [SerializeField]
        protected float playerDetectionDistance = 2.0f;

        
        //Fields: InternalState
        
        private bool resetAttack = true;

        protected GameObject player;

        private Damage damage;

        // Services

        private IGameFactory gameFactory;
        
        // Methods: Lifecycle

        public override void Awake()
        {
            base.Awake();
            gameFactory = AllServices.Container.Single<IGameFactory>();
            blade.OnHit.AddListener(OnHit);
        }

        private void FixedUpdate()
        {
            if (!player)
                if (gameFactory.Player)
                    player = gameFactory.Player;
                else
                    return;

            var distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
            if(distance > playerDetectionDistance)
            {
                isHit = false;
                Animable.Attack(false);
            }

            var direction = player.transform.localPosition.x - transform.localPosition.x;
            if (direction > 0 && Animable.GetBool("InCombat"))
                Moveable.Flip(true);
            else if (direction < 0 && Animable.GetBool("InCombat"))
                Moveable.Flip(false);

        }

        // Public API
        
        public override void Attack()
        {
        }
        
        // Methods: Internal State

        private void OnHit(Collider2D damagedTarget)
        {
            if (!resetAttack)
                return;
            
            var attackable = damagedTarget.gameObject.GetComponent<Attackable>();
            if (attackable)
            {
                // damagedTargets.Add(attackable);
                attackable.TakeDamage(damage);
                Debug.Log("Enemy damaged " + damagedTarget.name);
                resetAttack = false;

                StartCoroutine(ResetAttackRoutine());
            }
        }
        
        protected override IEnumerator ResetAttackRoutine()
        {
            yield return new WaitForSeconds(attackRechargeTime);
            resetAttack = true;
        }
    }
}
