
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        IDamageable damageable = coll.gameObject.GetComponent<IDamageable>();
        if(damageable != null && damageable.CanTakeDamage())
        {
            damageable.TakeDamage(damage);
        }
    }
}
