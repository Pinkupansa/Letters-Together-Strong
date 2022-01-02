using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Bomb : MonoBehaviour, ISeizable
{
    [SerializeField] private float timeBeforeExplosion;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float explosionRadius;

    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionDamage;

    [SerializeField] private AudioClip explosionSound;
    private void Start()
    {
        LeanTween.value(gameObject, 0, 1, timeBeforeExplosion).setEase(animationCurve).setOnUpdate(Blink).setOnComplete(Explode);
    }
    private void Blink(float value)
    {
        GetComponentInChildren<Light2D>().intensity = value;
    }
    private void Explode()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(Collider2D coll in colls)
        {
            Rigidbody2D rb = coll.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.AddForce((coll.transform.position - transform.position).normalized * explosionForce, ForceMode2D.Impulse);
            }
            IDamageable damageable = coll.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(explosionDamage);
            }
        }
        SoundManager.current.PlaySound(explosionSound);
        Destroy(gameObject);
    }
    public void OnSeize()
    {

    }
    public void OnUnseize()
    {

    }
    public bool Tiltable()
    {
        return false;
    }
}
