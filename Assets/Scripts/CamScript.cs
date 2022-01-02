using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    [SerializeField] private float smoothness;
    [SerializeField] private Vector3 offset = Vector3.forward * -10;
    [SerializeField] private LeanTweenType interpolationType;
    private Transform target;
    private bool canFollowTarget;
    private void Start()
    {
        GameEvents.current.playerChangedControl.AddListener(OnPlayerChangedControl);
        //target = Player.current.controlledLetter.transform;
    }
    private void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
        canFollowTarget = false;
        LeanTween.move(gameObject, target.position + offset, smoothness/Vector2.Distance(target.position + offset, transform.position)).setOnComplete(() => canFollowTarget = true).setEase(interpolationType);
    }
    private void OnPlayerChangedControl(GameObject obj)
    {
        ChangeTarget(obj.transform);
    }
    private void FixedUpdate()
    {
        if(canFollowTarget && target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, 1f/smoothness);
        }
    }

}
