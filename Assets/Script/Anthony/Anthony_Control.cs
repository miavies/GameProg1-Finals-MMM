using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class Anthony_Control : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canAttack = false; // Track if the player can attack
    bool hasList = false;   // Track if the player has picked up the List item
    bool canMove = true;

    public bool hasBaguette = false;
    public bool hasPotato = false;
    public bool hasSpices = false;
    public bool hasWine = false;
    public bool hasSteak = false;
    public bool hasAsparagus = false;
    public bool hasButter = false;
    public bool listComplete = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        LayerMask layersToInclude = ~(1 << LayerMask.NameToLayer("Ignore"));

        // Setup the movement filter to include only the specified layers
        movementFilter.useLayerMask = true;
        movementFilter.layerMask = layersToInclude;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }
                if (!success)
                {
                    TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("isMoving", success);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        if (canAttack)
        {
            animator.SetTrigger("swordAttack");
        }
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void PickUpItem(string itemType)
    {
        switch (itemType)
        {
            case "Baguette":
                hasBaguette = true;
                canAttack = true;
                break;
            case "Butter":
                hasButter = true;
                break;
            case "Potato":
                hasPotato = true;
                break;
            case "Spices":
                hasSpices = true;
                break;
            case "Wine":
                hasWine = true;
                break;
            case "Steak":
                hasSteak = true;
                break;
            case "Asparagus":
                hasAsparagus = true;
                break;
            case "List":
                hasList = true;
                break;
        }

        CheckListComplete();
    }

    private void CheckListComplete()
    {
        if (hasBaguette && hasButter && hasPotato && hasSpices && hasWine && hasSteak && hasAsparagus)
        {
            listComplete = true;
            Debug.Log("List is complete!");
        }
        else
        {
            Debug.Log("List is not complete yet.");
            Debug.Log($"hasBaguette: {hasBaguette}, hasButter: {hasButter}, hasPotato: {hasPotato}, hasSpices: {hasSpices}, hasWine: {hasWine}, hasSteak: {hasSteak}, hasAsparagus: {hasAsparagus}");
        }
    }

    public bool HasList()
    {
        return hasList;
    }

    public bool IsListComplete()
    {
        return listComplete;
    }
}
