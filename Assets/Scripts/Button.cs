using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private int ID;
    [SerializeField] private float animTime;
    [SerializeField] private float pressAmount;
    
    private bool isPressed;

    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Letter" && !isPressed)
        {
            Press();
        }
        
    }
    private void Press()
    {
        GameEvents.current.ButtonPressed(ID);
        LeanTween.move(gameObject, transform.position - transform.up * pressAmount, animTime).setEase(LeanTweenType.easeOutSine);
        isPressed = true;
    }
}
