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

        [SerializeField, Header("Movement")] 
        private bool turnToMovementDir = true;

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

            if (turnToMovementDir)
            {
                if (input.x < 0)
                    Flip(false);
                else if (input.x > 0)
                    Flip(true);
            }
            
            Animable?.Move(input.x);
        }

        public void MoveTowards(Vector3 position)
        {
            var direction = position - transform.position;
            if (turnToMovementDir)
            {
                if (direction.x < 0)
                    Flip(false);
                else if (direction.x > 0)
                    Flip(true);
            }

            var movePosition = transform.position;
            movePosition.x = Mathf.MoveTowards(
                transform.position.x, position.x, movementSpeed * Time.deltaTime);
            Rigidbody.MovePosition(movePosition);
        }
        
        public void Jump()
        {
            if (IsGrounded())
            {
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, jumpForce);
                Animable?.Jump(true);
                StartCoroutine(ResetJumpRoutine());
            }
        }
        
        public void Flip(bool facingLeft)
        {
            var currentFacingLeft = transform.localScale.x > 0;
            if(facingLeft && currentFacingLeft)
                transform.localScale = new Vector3( transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            else if(!facingLeft && !currentFacingLeft)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        public void BoostSpeed(float multiplier, float time)
        {
            
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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .2f, groundMask);
            // Debug.DrawRay(transform.position, Vector2.down, Color.green);

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
