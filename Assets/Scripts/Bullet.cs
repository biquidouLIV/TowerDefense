using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public Enemy target;
    public int damage;
    
    [SerializeField] private float speed = 0.01f;
    [SerializeField] private float hitDistance = 0.15f;
    private Vector3 direction;
    


    private void Update()
    {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            if (Vector3.Distance(transform.position, target.transform.position) < hitDistance)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
        
        direction = (target.transform.position - transform.position).normalized;
        transform.Translate(direction * speed);
    }
}
