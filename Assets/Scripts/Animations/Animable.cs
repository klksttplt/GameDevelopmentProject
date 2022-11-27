using System;
using Logic.Common;
using UnityEngine;

namespace Animations
{
    public class Animable : ContextComponent
    {
        // Fields: Editor
        
        
        // Fields: Internal State

        private Animator animator;
        
        private static readonly int Attack1 = Animator.StringToHash("Attack");
        private static readonly int Jumping = Animator.StringToHash("Jumping");
        private static readonly int Move1 = Animator.StringToHash("Move");
        private static readonly int Idle1 = Animator.StringToHash("Idle");
        private static readonly int InCombat = Animator.StringToHash("InCombat");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Death = Animator.StringToHash("Death");

        // Public API

        public void Idle()
        {
            animator.SetTrigger(Idle1);
        }
        
        public void Move(float move)
        {
            animator.SetFloat(Move1, Mathf.Abs(move));
        }

        public void Move(bool move)
        {
            animator.SetBool(Move1, move);
        }
        
        public void Jump(bool jump)
        {
            animator.SetBool(Jumping, jump);
        }

        public void Attack()
        {
            animator.SetTrigger(Attack1);
        }

        public void Attack(bool _)
        {
            animator.SetBool(InCombat, _);
        }

        public bool GetBool(string id)
        {
            return animator.GetBool(id);
        }

        public void TakeDamage()
        {
            animator.SetTrigger(Hit);
        }

        public bool IsInState(string id)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(id);
        }

        public void Die()
        {
            animator.SetTrigger(Death);
        }
        // Methods: Lifecycle

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }
    }
}
