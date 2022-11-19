using System;
using Infrastructure.Factory;
using UnityEngine;

namespace Logic.Combat.Enemy
{
    public class EnemtAttackController : AttackController
    {
        // Fields: Editor
        
        protected float playerDetectionDistance = 2.0f;

        
        //Fields: InternalState
        
        protected GameObject player;

        // Services

        private IGameFactory gameFactory;
        
        // Methods: Lifecycle

        public override void Awake()
        {
            base.Awake();
            player = gameFactory.Player;
        }

        private void FixedUpdate()
        {
            float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
            if(distance > playerDetectionDistance)
            {
                isHit = false;
                Animable.Attack(false);
            }

            float direction = player.transform.localPosition.x - transform.localPosition.x;
            if (direction > 0 && Animable.GetBool("InCombat"))
                Moveable.Flip(false);
            else if (direction < 0 && Animable.GetBool("InCombat"))
                Moveable.Flip(true);

        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}
