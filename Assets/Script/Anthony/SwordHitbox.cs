using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public float swordDamage = 1f;
    public float knockbackForce = 15f;
    public Collider2D swordCollider;
    public Vector3 faceRight = new Vector3 (0.72f, 0.59f, 0);
    public Vector3 faceLeft = new Vector3(-0.72f, 0.59f, 0);
    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        IDamage damageableObject = col.GetComponent<IDamage>();
        if (damageableObject != null)
        {
            Vector3 parentPosition = transform.parent.position;

            Vector2 direction = (Vector2)(col.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;

            //col.collider.SendMessage("OnHit", swordDamage, knockback);
            damageableObject.OnHit(swordDamage);
            Debug.Log("HIT");
        }
        else
        {
            Debug.LogWarning("Collider does not implement IDamage");
        }


        
    }

    void IsFacingRight(bool isFacingRight)
    {
        if (isFacingRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;
        }
    }
}
