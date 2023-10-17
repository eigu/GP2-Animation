using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Player Controls
    private PlayerControl playerControl;

    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;

    public float moveAmount;

    //new script
    public bool sprint_Input;
    public bool walk_Input;

    public void OnEnable()
    {
        if (playerControl == null)
        {
            playerControl = new PlayerControl();
            //When we hit WASD, record the movement to the variable movementInput using lambda expression
            playerControl.PlayerControls.WASD.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControl.PlayerActions.Sprint.performed += i => sprint_Input = true;
            playerControl.PlayerActions.Sprint.canceled += i => sprint_Input = false;
        }
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    public void HandleAllInput()
    {
        HandleMovementInput();
        HandleSprinting();
    }

    //Movement
    private void HandleMovementInput()
    {
        verticalInput  = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));
        PlayerManager.Instance.animatorManager.UpdateAnimatorValues(0, moveAmount,PlayerManager.Instance.isSprinting);
    }
    //new script
    private void HandleSprinting()
    {
        if (sprint_Input && moveAmount >0.5f)
        {
            PlayerManager.Instance.isSprinting = true;
        }
        else
        {
            PlayerManager.Instance.isSprinting = false;
        }
    } 
}
