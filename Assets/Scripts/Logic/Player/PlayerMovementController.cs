using Infrastructure.Services;
using Logic.Common;
using Services.Input;

public class PlayerMovementController : ContextComponent
{
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
}
