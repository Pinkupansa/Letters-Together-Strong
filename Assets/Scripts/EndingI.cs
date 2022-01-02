using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingI : MonoBehaviour
{
    [SerializeField] private Transform pointSlot;
    [SerializeField] private float rotationAnimTime;
    [SerializeField] private float pointAnimTime;
    [SerializeField] private float targetRotation;
    private bool triggered = false;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "IPoint")
        {
            coll.transform.SetParent(transform);
            coll.GetComponent<Rigidbody2D>().simulated = false;
            LeanTween.move(coll.gameObject, pointSlot.position, pointAnimTime).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>  LeanTween.rotateZ(gameObject, targetRotation, rotationAnimTime).setEase(LeanTweenType.easeInOutSine).setOnComplete(GameEvents.current.LevelComplete));
        }
    }
    


}
