using Infrastructure.Services;
using Logic.Common;
using Services.Input;
using UnityEngine;

public class PlayerMovementController : ContextComponent
{
    // Fields: Editor

    [SerializeField] 
    private GameObject graphics;
    
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
        if (inputService.Axis.x < 0)
            Flip(false);
        else if (inputService.Axis.x > 0)
            Flip(true);
    }
    
    // Methods: Internal State

    private void Flip(bool facingLeft)
    {
        var currentFacingLeft = graphics.transform.localScale.x > 0;
        if(facingLeft && currentFacingLeft)
            graphics.transform.localScale = new Vector3( graphics.transform.localScale.x * -1, graphics.transform.localScale.y, graphics.transform.localScale.z);
        else if(!facingLeft && !currentFacingLeft)
            graphics.transform.localScale = new Vector3(graphics.transform.localScale.x * -1, graphics.transform.localScale.y, graphics.transform.localScale.z);
    }

}
