using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float damage = 1;
    public float knockbackForce = 100f;
    public float moveSpeed = 2f;

    public DetectionZone detectionZone;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (detectionZone.detectObjs.Count > 0)
        {
            Collider2D detectedObject0 = detectionZone.detectObjs[0];

            Vector2 direction = (detectionZone.detectObjs[0].transform.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IDamage damageable = collision.collider.GetComponent<IDamage>();
        if (damageable != null)
        {
            Vector3 parentPosition = transform.parent.position;

            Vector2 direction = (Vector2)(collision.gameObject.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;

            //col.collider.SendMessage("OnHit", swordDamage, knockback);
            damageable.OnHit(damage);
            Debug.Log("Ouch");
        }
    }
}