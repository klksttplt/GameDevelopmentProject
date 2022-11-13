using System.Collections;
using UnityEngine;

namespace Logic.Combat.Player
{
    public class PlayerAttackController : AttackController
    {
        // Fields: Editor
        
        [SerializeField] 
        private Blade playerBlade;

        
        // Fields: Internal State

        private bool resetAttack = true;
        
        // Public API
        
        public override void Attack()
        {
            if (resetAttack)
            {
                Debug.Log("Player Attacks");
                playerBlade.gameObject.SetActive(true);
                StartCoroutine(ResetJumpRoutine());
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

        // Methods: Internal State
        
        private IEnumerator ResetJumpRoutine()
        {
            resetAttack = true;
            yield return new WaitForSeconds(attackRechargeTime);
            resetAttack = false;
        }

        private void OnHit(Collider2D damagedTarget)
        {
            var attackable = damagedTarget.gameObject.GetComponent<Attackable>();
            if (attackable)
            {
                Debug.Log("Player damaged " + damagedTarget.name);
            }
        }
    }
}