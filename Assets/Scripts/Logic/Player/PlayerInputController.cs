using Infrastructure.Services;
using Logic.Common;
using Services.Input;
using UnityEngine;

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
            if(inputService.IsJumpButtonDown())
                Moveable.Jump();
        }

        private void FixedUpdate()
        {
            Moveable.Move(inputService.Axis);
            
        }
    
        // Methods: Internal State

        

    }
}
