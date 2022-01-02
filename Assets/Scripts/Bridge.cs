using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour, IPressurePlateTriggeredBehaviour
{
    [SerializeField] private Vector2 baseScale, targetScale;
    [SerializeField] private float triggerTime;

    private bool open = false;
    private void Start()
    {
       transform.localScale = baseScale;
    }
    public void Trigger()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, targetScale, triggerTime).setEase(LeanTweenType.easeOutSine);
        open = true;
    }
    public void Untrigger()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, baseScale, triggerTime).setEase(LeanTweenType.easeOutSine);
        open = false;
    }
    public bool IsTriggered()
    {
        return open;
    }
}
