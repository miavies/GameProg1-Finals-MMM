using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageableCharacter : MonoBehaviour, IDamage
{
    public HealthBar healthBar; // Reference to the HealthBar script
    public GameObject healthTextPrefab; // Prefab for displaying health text
    public bool disabledSimulation = false;
    public float invincibilityTime = 0.25f;
    public bool isInvincibilityEnabled = false;
    public event Action OnDeath;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected bool isAlive = true;
    Collider2D physicsCollider;

    [SerializeField] private float _health = 3;
    [SerializeField] private bool _targetable = true;
    [SerializeField] private bool _invincible = false;
    [SerializeField] private float invincibleTimeElapsed = 0f;

    public float Health
    {
        set
        {
            if (!isAlive) return; // Prevent any changes if already dead

            if (value < _health)
            {
                animator.SetTrigger("Hit");

                GameObject healthTextObject = Instantiate(healthTextPrefab);
                RectTransform textTransform = healthTextObject.GetComponent<RectTransform>();
                textTransform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                Canvas canvas = FindObjectOfType<Canvas>();
                healthTextObject.transform.SetParent(canvas.transform);

                Destroy(healthTextObject, 1.0f);
            }

            _health = value;

            if (_health <= 0)
            {
                if (isAlive)
                {
                    animator.SetBool("Alive", false);
                    isAlive = false;
                    Targetable = false;
                    rb.simulated = false;
                    OnDeath?.Invoke();
                    DestroyAfterDelay(5.0f);

                    // Check if the gameObject has the "Player" tag before loading scene
                    if (gameObject.CompareTag("Player"))
                    {
                        StartCoroutine(LoadGameOverAfterDelay(3.0f));
                    }
                }
            }

            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(Mathf.CeilToInt(_health));
            }
        }
        get { return _health; }
    }

    public bool Targetable
    {
        get { return _targetable; }
        set
        {
            _targetable = value;

            if (disabledSimulation)
            {
                rb.simulated = false;
            }

            physicsCollider.enabled = value;
        }
    }

    public bool Invincible
    {
        get { return _invincible; }
        set
        {
            _invincible = value;
            if (_invincible == true)
            {
                invincibleTimeElapsed = 0f;
            }
        }
    }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        animator.SetBool("Alive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }

    public void OnHit(float damage)
    {
        if (!Invincible)
        {
            Health -= damage;
            if (isInvincibilityEnabled)
            {
                Invincible = true;
            }
        }

    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    private bool IsValidSource(GameObject source)
    {
        // Prevent non-player characters from damaging each other
        if (!gameObject.CompareTag("Player") && !source.CompareTag("Player"))
        {
            return false;
        }

        return (gameObject.CompareTag("Player") && source.CompareTag("Enemy")) ||
               (gameObject.CompareTag("Enemy") && source.CompareTag("Player"));
    }

    public void TakeDamage(float damage, GameObject source)
    {
        if (IsValidSource(source))
        {
            OnHit(damage);
        }
    }

    public void TakeDamage(float damage, Vector2 knockback, GameObject source)
    {
        if (IsValidSource(source))
        {
            OnHit(damage, knockback);
        }
    }

    public void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }

    private void DestroyAfterDelay(float delay)
    {
        Invoke(nameof(OnObjectDestroyed), delay);
    }

    public void FixedUpdate()
    {
        if (Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;

            if (invincibleTimeElapsed > invincibilityTime)
            {
                Invincible = false;
            }
        }
    }

    IEnumerator LoadGameOverAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("GameOver");
    }
}
