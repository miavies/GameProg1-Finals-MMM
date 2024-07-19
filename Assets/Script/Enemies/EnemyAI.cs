using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private DamageableCharacter baseEnemy;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        baseEnemy = GetComponent<DamageableCharacter>();

        if (baseEnemy != null)
        {
            baseEnemy.OnDeath += HandleDeath; 
        }
    }

    private void Start()
    {
        state = State.Roaming;
        StartCoroutine(RoamingRoutine());
    }

    private void HandleDeath()
    {
        StopAllCoroutines(); 
        StartCoroutine(DisappearAfterDelay());
    }

    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(5f); 
        Destroy(gameObject); 
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(1f);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void OnDestroy()
    {
        if (baseEnemy != null)
        {
            baseEnemy.OnDeath -= HandleDeath; 
        }
    }
}
