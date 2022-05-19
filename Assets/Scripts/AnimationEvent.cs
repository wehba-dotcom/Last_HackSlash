using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationEvent : MonoBehaviour
{
    public CharacterMovment charmove;
public void playerAttack()
    {
        Debug.Log("player attack");
        charmove.DoAttack();
    }


   
}
