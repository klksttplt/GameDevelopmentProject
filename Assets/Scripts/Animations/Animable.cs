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

        // Public API

        public void Move(float move)
        {
            animator.SetFloat(Move1, Mathf.Abs(move));
        }

        public void Jump(bool jump)
        {
            animator.SetBool(Jumping, jump);
        }

        public void Attack()
        {
            animator.SetTrigger(Attack1);
        }
        
        // Methods: Lifecycle

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }
    }
}
