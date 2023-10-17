using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    Vector3 moveDirection;
    Transform cameraObject;

    private void Start()
    {
        cameraObject = Camera.main.transform;
    }

    public void HandlesAllMovement()
    {
        HandlesFallingAndLanding();
        //if our player is interacting, we will not gonna run the code below
        if (PlayerManager.Instance.isInteracting)
            return;
        HandlesMovement();
        HandlesRotation();
    }

    private void HandlesMovement()
    {
        moveDirection = cameraObject.forward * PlayerManager.Instance.inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * PlayerManager.Instance.inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (PlayerManager.Instance.isSprinting == true)
        {
            moveDirection = moveDirection * PlayerManager.Instance.sprintSpeed;
        }
        else
        {
            if (PlayerManager.Instance.inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * PlayerManager.Instance.moveSpeed;
            }
            else
            {
                moveDirection = moveDirection * PlayerManager.Instance.walkingSpeed;
            }

        }
       
        //line that moves the player based on movespeed
        //speed if we are sprinting
        //speed if we are walking
        //speed if we are moving

        //line that moves the player based on speed Stats
       
        Vector3 movementVelocity = moveDirection;


     
        PlayerManager.Instance.rigidBody.velocity = movementVelocity;
    }

    private void HandlesRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = 
            cameraObject.forward * PlayerManager.Instance.inputManager.verticalInput;
        targetDirection = targetDirection + 
            cameraObject.right * PlayerManager.Instance.inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        //line that makes the rotation to not reset
        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp
            (transform.rotation,targetRotation,PlayerManager.Instance.rotSpeed);
        transform.rotation = playerRotation;
    }

    private void HandlesFallingAndLanding()
    {
        Debug.DrawRay(transform.position, -Vector3.up, Color.green);

        RaycastHit hit;
        Ray rayCastOrigin = new Ray(transform.position,Vector3.down);
        //rayCastOrigin.y = rayCastOrigin.y + PlayerManager.Instance.rayCastheight;


        if (!PlayerManager.Instance.isGrounded)
        {
            if (!PlayerManager.Instance.isInteracting)
            {
                PlayerManager.Instance.animatorManager.PlayTargetAnimation("Falling",true);
            }
            PlayerManager.Instance.inAirTimer = PlayerManager.Instance.inAirTimer + Time.deltaTime;
            PlayerManager.Instance.rigidBody.AddForce(transform.forward * PlayerManager.Instance.leapingVelocity);
            PlayerManager.Instance.rigidBody.AddForce(-Vector3.up * PlayerManager.Instance.fallingSpeed * PlayerManager.Instance.inAirTimer);
        }

        if (Physics.Raycast(rayCastOrigin,out hit, PlayerManager.Instance.rayCastheight, PlayerManager.Instance.groundLayer))
        {
            if (!PlayerManager.Instance.isGrounded && !PlayerManager.Instance.isInteracting)
            {
                PlayerManager.Instance.animatorManager.PlayTargetAnimation("Land",true);
            }

            PlayerManager.Instance.inAirTimer = 0;
            PlayerManager.Instance.isGrounded = true;
        }
        else
        {
            PlayerManager.Instance.isGrounded = false;
        }

    }
}
