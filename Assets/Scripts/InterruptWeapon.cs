using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptWeapon : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        CharacterHandleWeapon[] WeaponList;
        WeaponList = animator.GetComponents<CharacterHandleWeapon>();

        for (int x = 0; x < WeaponList.Length; x++) {
            if (WeaponList[x].CurrentWeapon != null) {
                WeaponList[x].CurrentWeapon.Interrupt();
            }
        }
    }
}
