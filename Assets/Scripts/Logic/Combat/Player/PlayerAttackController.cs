using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Combat.Player
{
    public class PlayerAttackController : AttackController
    {
        // Fields: Editor
        
        [SerializeField] 
        private Blade playerBlade;

        [SerializeField] 
        private float attackDuration; // change further to attack anim time
        
        // Fields: Internal State

        private bool resetAttack = true;

        private Damage damage;
        
        private List<Attackable> damagedTargets = new List<Attackable>();
        
        // Public API
        
        public override void Attack()
        {
            if (resetAttack)
            {
                playerBlade.gameObject.SetActive(true);
                StartCoroutine(ResetAttackRoutine());
            }
        }
        
        // Methods: Lifecycle

        public override void Awake()
        {
            base.Awake();
            playerBlade = GetComponentInChildren<Blade>();
            playerBlade.OnHit.AddListener(OnHit);
            playerBlade.gameObject.SetActive(false);
        }

        private void Start()
        {
            damage = new Damage()
            {
                amount = damageValue
            };
        }
        
        // Methods: Internal State
        
        private IEnumerator ResetAttackRoutine()
        {
            resetAttack = false;

            yield return new WaitForSeconds(attackDuration);
            playerBlade.gameObject.SetActive(false);

            yield return new WaitForSeconds(attackRechargeTime);
            damagedTargets.Clear();
            resetAttack = true;
        }

        private void OnHit(Collider2D damagedTarget)
        {
            var attackable = damagedTarget.gameObject.GetComponent<Attackable>();
            if (attackable & !damagedTargets.Contains(attackable))
            {
                damagedTargets.Add(attackable);
                attackable.TakeDamage(damage);
                Debug.Log("Player damaged " + damagedTarget.name);
            }
        }
    }
}