using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private float timeToAppear;

    private void Start()
    {
        Color baseColor = GetComponent<TMPro.TMP_Text>().color;
        LeanTween.value(gameObject, 0, 255, timeToAppear).setEase(LeanTweenType.easeOutSine).setOnUpdate((v) => GetComponent<TMPro.TMP_Text>().color = new Color(baseColor.r, baseColor.g, baseColor.a, v));
    }
}
