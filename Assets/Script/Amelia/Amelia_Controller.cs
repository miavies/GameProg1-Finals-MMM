using UnityEngine;

public class Amelia_Controller : MonoBehaviour
{
    #region Enums
    private enum Directions { UP, DOWN, RIGHT }
    #endregion

    #region Editor Data
    [Header("Dependencies")]
    [SerializeField] float moveSpeed = 50f;

    [Header("Dependencies")]
    [SerializeField] Rigidbody2D amelia_rb;
    [SerializeField] Animator amelia_anim;
    [SerializeField] SpriteRenderer amelia_sprite;
    #endregion

    #region Internal Data
    private Vector2 moveDir = Vector2.zero;
    private Directions amelia_facing = Directions.RIGHT; // Default facing direction
    private readonly int amelia_animMoveRight = Animator.StringToHash("Amelia_Right_Move");
    private readonly int amelia_animIdleRight = Animator.StringToHash("Amelia_Right_Idle");
    private readonly int amelia_animMoveUp = Animator.StringToHash("Amelia_Up_Move");
    private readonly int amelia_animIdleUp = Animator.StringToHash("Amelia_Up_Idle");
    private readonly int amelia_animMoveDown = Animator.StringToHash("Amelia_Down_Move");
    private readonly int amelia_animIdleDown = Animator.StringToHash("Amelia_Down_Idle");
    #endregion

    #region Tick
    private void Update()
    {
        GatherControllerInput();
        UpdateFacingDirection();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }
    #endregion

    #region Input Logic
    private void GatherControllerInput()
    {
        // Gather input from controller axes
        float controllerHorizontal = Input.GetAxis("Horizontal");
        float controllerVertical = Input.GetAxis("Vertical");

        // Check if any controller input is detected
        if (Mathf.Abs(controllerHorizontal) > 0.1f || Mathf.Abs(controllerVertical) > 0.1f)
        {
            moveDir.x = controllerHorizontal;
            moveDir.y = controllerVertical;
        }
        else
        {
            // If no controller input detected, reset moveDir to zero
            moveDir = Vector2.zero;
        }
    }
    #endregion

    #region Movement Logic
    private void MovementUpdate()
    {
        amelia_rb.velocity = moveDir.normalized * moveSpeed * Time.fixedDeltaTime;
    }
    #endregion

    #region Animation Logic
    private void UpdateFacingDirection()
    {
        if (moveDir.x > 0)
        {
            amelia_facing = Directions.RIGHT;
        }
        else if (moveDir.x < 0)
        {
            amelia_facing = Directions.RIGHT; // Treat left as right, but flip the sprite
        }
        else if (moveDir.y != 0)
        {
            if (moveDir.y > 0)
            {
                amelia_facing = Directions.UP;
            }
            else
            {
                amelia_facing = Directions.DOWN;
            }
        }
    }

    private void UpdateAnimation()
    {
        switch (amelia_facing)
        {
            case Directions.UP:
                if (moveDir.y > 0)
                {
                    amelia_anim.CrossFade(amelia_animMoveUp, 0);
                }
                else
                {
                    amelia_anim.CrossFade(amelia_animIdleUp, 0);
                }
                break;
            case Directions.DOWN:
                if (moveDir.y < 0)
                {
                    amelia_anim.CrossFade(amelia_animMoveDown, 0);
                }
                else
                {
                    amelia_anim.CrossFade(amelia_animIdleDown, 0);
                }
                break;
            case Directions.RIGHT:
                if (moveDir.x != 0)
                {
                    amelia_anim.CrossFade(amelia_animMoveRight, 0);
                    amelia_sprite.flipX = (moveDir.x < 0); // Flip sprite if moving left
                }
                else
                {
                    amelia_anim.CrossFade(amelia_animIdleRight, 0);
                }
                break;
        }
    }
    #endregion
}
