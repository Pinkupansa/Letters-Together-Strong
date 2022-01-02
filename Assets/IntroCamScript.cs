using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCamScript : MonoBehaviour
{
    [SerializeField] private Vector2 target;
    [SerializeField] private float timeWatchingsplashScreen, transitionTime;

    private void Start()
    {
        LeanTween.move(gameObject, target, transitionTime).setDelay(timeWatchingsplashScreen).setEase(LeanTweenType.easeInOutSine);
    }
}
