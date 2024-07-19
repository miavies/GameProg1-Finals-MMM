using UnityEngine;
using System.Collections;


public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private bool canMove = true;
    private DamageableCharacter baseEnemy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        baseEnemy = GetComponent<DamageableCharacter>();

        if (baseEnemy != null)
        {
            baseEnemy.OnDeath += HandleDeath; 
        }
    }

    private void FixedUpdate()
    {
        if (canMove && moveDir != Vector2.zero)
        {
            rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }

    private void HandleDeath()
    {
        canMove = false; 
        StartCoroutine(DisappearAfterDelay());
    }

    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(5f); 
        Destroy(gameObject); 
    }


    private void OnDestroy()
    {
        if (baseEnemy != null)
        {
            baseEnemy.OnDeath -= HandleDeath; 
        }
    }
}
