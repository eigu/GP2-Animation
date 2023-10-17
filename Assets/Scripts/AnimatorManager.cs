using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    int horizontal,vertical;

    private void Awake()
    {
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        PlayerManager.Instance.animator.SetBool("isInteracting", isInteracting);
        PlayerManager.Instance.animator.CrossFade(targetAnimation,0.2f);
        
    }
    
    //new script, added bool isSprinting
    public void UpdateAnimatorValues(float horizontalMovement,float verticalMovement, bool isSprinting)
    {
        if (isSprinting)
        {
            horizontalMovement = 2;
        }
        PlayerManager.Instance.animator.SetFloat(horizontal, horizontalMovement,0.1f,Time.deltaTime);
        PlayerManager.Instance.animator.SetFloat(vertical, verticalMovement, 0.1f,Time.deltaTime);
    }
}
