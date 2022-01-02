using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tremplin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float bounciness;
    [SerializeField] private float maxForce;
    [SerializeField] private AudioClip bounceSound;
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.AddForce(Vector2.up * Mathf.Clamp(bounciness * -rb.velocity.y , 0, maxForce)* rb.mass, ForceMode2D.Impulse);
            
            SoundManager.current.PlaySound(bounceSound);
        }
    }
}
