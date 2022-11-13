using UnityEngine;

namespace Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string JumpButton = "Jump";
        private const string AttackButton = "Fire";

        public abstract Vector2 Axis { get; }

        public bool IsJumpButtonDown() => 
            SimpleInput.GetButtonDown(JumpButton);

        public bool IsAttackButtonUp() =>
            SimpleInput.GetButtonUp(AttackButton);

        protected static Vector2 SimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}
