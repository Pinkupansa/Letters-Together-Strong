using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneCustom : MonoBehaviour
{
    private List<Rigidbody2D> rigidbody2Ds;
    [SerializeField] private float windForce = 100f;
    private void Start()
    {
        rigidbody2Ds = new List<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        foreach(Rigidbody2D rb in rigidbody2Ds)
        {
            rb.AddForce(Vector2.up*windForce, ForceMode2D.Force);
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.GetComponent<Rigidbody2D>() != null)
        {
            rigidbody2Ds.Add(coll.GetComponent<Rigidbody2D>());
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.GetComponent<Rigidbody2D>() != null)
        {
            rigidbody2Ds.Remove(coll.GetComponent<Rigidbody2D>());
        }
    }
}
