using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Bob_Controller : MonoBehaviour
{
    #region Enums
    private enum Directions { UP, DOWN, RIGHT } // Removed LEFT from the enum
    #endregion

    #region Editor Data
    [Header("Dependencies")]
    [SerializeField] float moveSpeed = 50f;

    [Header("Dependencies")]
    [SerializeField] Rigidbody2D bob_rb;
    [SerializeField] Animator bob_anim;
    [SerializeField] SpriteRenderer bob_sprite;
    #endregion

    #region Internal Data
    private Vector2 moveDir = Vector2.zero;
    private Directions bob_facing = Directions.RIGHT; // Default facing direction
    private readonly int bob_animMoveRight = Animator.StringToHash("Bob_Right_Move");
    private readonly int bob_animIdleRight = Animator.StringToHash("Bob_Right_Idle");
    private readonly int bob_animMoveUp = Animator.StringToHash("Bob_Up_Move");
    private readonly int bob_animIdleUp = Animator.StringToHash("Bob_Up_Idle");
    private readonly int bob_animMoveDown = Animator.StringToHash("Bob_Down_Move");
    private readonly int bob_animIdleDown = Animator.StringToHash("Bob_Down_Idle");
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
    }
    #endregion

    #region Movement Logic
    private void MovementUpdate()
    {
        bob_rb.velocity = moveDir.normalized * moveSpeed * Time.fixedDeltaTime;
    }
    #endregion

    #region Animation Logic
    private void UpdateFacingDirection()
    {
        if (moveDir.x > 0)
        {
            bob_facing = Directions.RIGHT;
        }
        else if (moveDir.x < 0)
        {
            bob_facing = Directions.RIGHT; // Treat left as right, but flip the sprite
        }
        else if (moveDir.y != 0)
        {
            if (moveDir.y > 0)
            {
                bob_facing = Directions.UP;
            }
            else
            {
                bob_facing = Directions.DOWN;
            }
        }
    }

    private void UpdateAnimation()
    {
        switch (bob_facing)
        {
            case Directions.UP:
                if (moveDir.y > 0)
                {
                    bob_anim.CrossFade(bob_animMoveUp, 0);
                }
                else
                {
                    bob_anim.CrossFade(bob_animIdleUp, 0);
                }
                break;
            case Directions.DOWN:
                if (moveDir.y < 0)
                {
                    bob_anim.CrossFade(bob_animMoveDown, 0);
                }
                else
                {
                    bob_anim.CrossFade(bob_animIdleDown, 0);
                }
                break;
            case Directions.RIGHT:
                if (moveDir.x != 0)
                {
                    bob_anim.CrossFade(bob_animMoveRight, 0);
                    bob_sprite.flipX = (moveDir.x < 0); // Flip sprite if moving left
                }
                else
                {
                    bob_anim.CrossFade(bob_animIdleRight, 0);
                }
                break;
        }
    }
    #endregion
}
