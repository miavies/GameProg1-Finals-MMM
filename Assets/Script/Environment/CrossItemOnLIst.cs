using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CrossItemOnList : MonoBehaviour
{
    public Animator anim;
    public Image[] crosses;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void crossItem(string itemType)
    {
        if (itemType == "Baguette")
        {
            anim.SetBool("IsDisplayed", true);
            crosses[0].enabled = true;
        }

    }



}