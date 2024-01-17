using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class AnimationSwitcher : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private Slider _slider;
    // Start is called before the first frame update

    // Update is called once per frame

    public void SwitchAnimations(string animationName)
    {
        switch (animationName)
        {
            case "Idle":
            animator.SetFloat("movementSpeed",0);  
                break;
            case "Walking":
                animator.SetFloat("movementSpeed",1);  
                break;
            case "Running":
                animator.SetFloat("movementSpeed",2);  
                break;
        }
        //int stateHash = Animator.StringToHash(animationName

    }
    public void BlendAnimation()
    {
        animator.SetFloat("movementSpeed", _slider.value);
    }
    
}