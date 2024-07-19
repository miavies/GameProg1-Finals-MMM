using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Rover_Controller : MonoBehaviour
{
    #region Enums
    private enum Directions { UP, DOWN, RIGHT } // Removed LEFT from the enum
    #endregion

    #region Editor Data
    [Header("Dependencies")]
    [SerializeField] float moveSpeed = 50f;

    [Header("Dependencies")]
    [SerializeField] Rigidbody2D rover_rb;
    [SerializeField] Animator rover_anim;
    [SerializeField] SpriteRenderer rover_sprite;
    #endregion

    #region Internal Data
    private Vector2 moveDir = Vector2.zero;
    private Directions rover_facing = Directions.RIGHT; // Default facing direction
    private readonly int rover_animMoveRight = Animator.StringToHash("Rover_Right_Move");
    private readonly int rover_animIdleRight = Animator.StringToHash("Rover_Right_Idle");
    private readonly int rover_animMoveUp = Animator.StringToHash("Rover_Up_Move");
    private readonly int rover_animIdleUp = Animator.StringToHash("Rover_Up_Idle");
    private readonly int rover_animMoveDown = Animator.StringToHash("Rover_Down_Move");
    private readonly int rover_animIdleDown = Animator.StringToHash("Rover_Down_Idle");
    private readonly int rover_animAttackRight = Animator.StringToHash("Rover_Right_Attack");
    private bool isAttacking = false; // New bool for attacking
    #endregion

    #region Tick
    private void Update()
    {
        GatherInput();
        UpdateFacingDirection();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }
    #endregion

    #region Input Logic
    private void GatherInput()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        // Check for attack input
        if (Input.GetKeyDown(KeyCode.J))
        {
            isAttacking = true;
            Attack();
        }
    }
    #endregion

    #region Movement Logic
    private void MovementUpdate()
    {
        if (!isAttacking)
        {
            rover_rb.velocity = moveDir.normalized * moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            rover_rb.velocity = Vector2.zero; // Stop movement while attacking
        }
    }
    #endregion

    #region Animation Logic
    private void UpdateFacingDirection()
    {
        if (!isAttacking)
        {
            if (moveDir.x > 0)
            {
                rover_facing = Directions.RIGHT;
            }
            else if (moveDir.x < 0)
            {
                rover_facing = Directions.RIGHT; // Treat left as right, but flip the sprite
            }
            else if (moveDir.y != 0)
            {
                if (moveDir.y > 0)
                {
                    rover_facing = Directions.UP;
                }
                else
                {
                    rover_facing = Directions.DOWN;
                }
            }
        }
    }

    private void UpdateAnimation()
    {
        if (!isAttacking)
        {
            switch (rover_facing)
            {
                case Directions.UP:
                    if (moveDir.y > 0)
                    {
                        rover_anim.CrossFade(rover_animMoveUp, 0);
                    }
                    else
                    {
                        rover_anim.CrossFade(rover_animIdleUp, 0);
                    }
                    break;
                case Directions.DOWN:
                    if (moveDir.y < 0)
                    {
                        rover_anim.CrossFade(rover_animMoveDown, 0);
                    }
                    else
                    {
                        rover_anim.CrossFade(rover_animIdleDown, 0);
                    }
                    break;
                case Directions.RIGHT:
                    if (moveDir.x != 0)
                    {
                        rover_anim.CrossFade(rover_animMoveRight, 0);
                        rover_sprite.flipX = (moveDir.x < 0); // Flip sprite if moving left
                    }
                    else
                    {
                        rover_anim.CrossFade(rover_animIdleRight, 0);
                    }
                    break;
            }
        }
    }

    private void Attack()
    {
        if (rover_facing == Directions.RIGHT)
        {
            rover_anim.CrossFade(rover_animAttackRight, 0);
        }
        // Add cases for other directions if you have attack animations for them

        // Reset attacking state after a short delay
        StartCoroutine(ResetAttackState());
    }

    private IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(0.5f); // Adjust duration based on your attack animation length
        isAttacking = false;
    }
    #endregion
}
