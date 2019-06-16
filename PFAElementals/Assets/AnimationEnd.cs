using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnd : StateMachineBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= animator.GetCurrentAnimatorClipInfo(0)[0].clip.length-0.1f)
        {
            animator.SetInteger("condition", 0);
            animator.GetComponentInParent<PlayerController>().CanPlay = true;
        }
    }

}
