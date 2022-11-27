using Infrastructure.Services;
using Logic.Common;
using Services.Input;

namespace Logic.Player
{
    public class PlayerInputController : ContextComponent
    {
        // Fields: Editor

        // Services

        private IInputService inputService;

        // Methods: Lifecycle

        public override void Awake()
        {
            base.Awake();
            inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            if (Health.IsDead)
                return;
            
            if(inputService.IsJumpButtonDown())
                Moveable.Jump();
            if (inputService.IsAttackButtonDown())
                AttackController.Attack();
        }

        private void FixedUpdate()
        {
            if (Health.IsDead)
                return;
            
            Moveable.Move(inputService.Axis);
        }
    
        // Methods: Internal State

        

    }
}
