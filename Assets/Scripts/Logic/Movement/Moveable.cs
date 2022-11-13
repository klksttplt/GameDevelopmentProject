using System.Collections;
using Logic.Common;
using Logic.Stats;
using UnityEngine;

namespace Logic.Movement
{
    public class Moveable : ContextComponent
    {
        // Fields: Editor

        [SerializeField, Header("Stats")] 
        private StatData movementSpeedData;
        [SerializeField] 
        private StatData jumpForceData;

        [SerializeField, Header("Ground Check")] 
        private LayerMask groundMask;
        // Fields: Internal State

        private StatValueProvider movementSpeedProvider;
        private float movementSpeed;
        
        private StatValueProvider jumpForceProvider;
        private float jumpForce;
        
        private bool resetJump;

        // Public API
        
        public StatDef MovementSpeedStatDef => movementSpeedData.Def;
        public StatDef JumpForceStatDef => jumpForceData.Def;

        public void Move(Vector2 input)
        {
            Rigidbody.velocity = new Vector2(input.x * movementSpeed, Rigidbody.velocity.y);
        }

        public void Jump()
        {
            if (IsGrounded())
            {
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, jumpForce);
                StartCoroutine(ResetJumpRoutine());
            }
        }
        
        // Methods: Lifecycle

        public override void Awake()
        {
            base.Awake();
            FillStats();
        }

        // Methods: Internal State

        private void FillStats()
        {
            var speedProvidedData = GetStatValue(MovementSpeedStatDef);
            movementSpeedProvider = speedProvidedData ?? movementSpeedData.DefaultBaseValueProvider;
            movementSpeed = movementSpeedProvider.BaseValue;
            
            var jumpProvidedData = GetStatValue(JumpForceStatDef);
            jumpForceProvider = jumpProvidedData ?? jumpForceData.DefaultBaseValueProvider;
            jumpForce = jumpForceProvider.BaseValue;
        }

        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, groundMask);
            Debug.DrawRay(transform.position, Vector2.down, Color.green);

            if (hit.collider != null)
                if (!resetJump)
                    return true;

            return false; 
        }
        
        private IEnumerator ResetJumpRoutine()
        {
            resetJump = true;
            yield return new WaitForSeconds(0.1f);
            resetJump = false;
        }
    }
}
