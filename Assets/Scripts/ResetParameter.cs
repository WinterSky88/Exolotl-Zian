using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetParameter : StateMachineBehaviour {
    public string[] parameters;
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        for (int x = 0; x < parameters.Length; x++) {
            animator.ResetTrigger(parameters[x]);
        }
    }
}