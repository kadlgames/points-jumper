using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homer : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetBool("to_home", !GameManager.IsOnGOScreen);
    }
}
