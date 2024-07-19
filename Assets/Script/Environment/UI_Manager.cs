using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
   public void DisableBoolAnimator(Animator anim)
    {
        anim.SetBool("IsDisplayed", false);
    }

    public void EnableBoolAnimator(Animator anim)
    {
        anim.SetBool("IsDisplayed", true);
    }
}
