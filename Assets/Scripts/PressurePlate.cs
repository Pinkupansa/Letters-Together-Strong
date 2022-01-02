using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    
    [SerializeField] private int ID;
    [SerializeField] private Vector2 releasedScale, pressedScale;
    [SerializeField] private float animTime;
    private int bodiesOverPlateCount;

    private void Start()
    {
        transform.localScale = releasedScale;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        bodiesOverPlateCount += 1;
        if(bodiesOverPlateCount == 1)
        {
            Press();
        }
    } 
    private void OnTriggerExit2D(Collider2D coll)
    {
        bodiesOverPlateCount -= 1;
        if(bodiesOverPlateCount == 0)
        {
            Release();
        }
    }
    private void Press()
    {
        GameEvents.current.ButtonPressed(ID);
        LeanTween.scale(gameObject, pressedScale, animTime).setEase(LeanTweenType.easeOutSine);
    }
    private void Release()
    {
        GameEvents.current.PressurePlateReleased(ID);
        LeanTween.scale(gameObject, releasedScale, animTime).setEase(LeanTweenType.easeOutSine);
    }
}
