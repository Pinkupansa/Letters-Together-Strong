using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IDamageable
{
    
    [SerializeField] private float maxDurability;
    [SerializeField] private AnimationCurve shakeCurve;
    [SerializeField] private float shakeMultiplier;
    [SerializeField] private float shakeTime;

    [SerializeField] private AudioClip rockSound;
    private bool canTakeDamage = true;
    private float currentDurability;

    private Vector2 basePos;
    private void Start()
    {
        currentDurability = maxDurability;
        basePos = transform.position;
    }
    public void TakeDamage(float damage)
    {
        SoundManager.current.PlaySound(rockSound);
        currentDurability -= damage;
        canTakeDamage = false;
        LeanTween.value(-shakeMultiplier, shakeMultiplier, shakeTime).setEase(shakeCurve).setOnUpdate((v) => transform.position = new Vector2(basePos.x + v, basePos.y)).setOnComplete(() => canTakeDamage = true);
        if(currentDurability <= 0)
        {
            Break();
        }
    }
    public bool CanTakeDamage()
    {
        return canTakeDamage;
    }
    private void Break()
    {
        LeanTween.cancel(gameObject);
        gameObject.SetActive(false);
    }
}
