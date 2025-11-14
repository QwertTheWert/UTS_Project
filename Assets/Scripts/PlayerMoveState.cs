using UnityEngine;

public class PlayerMoveState : PlayerState
{
    [SerializeField] private float moveSpeed = 120f;
    private Vector2 movement;

    public override void UpdateState()
    {
        movement.Set(0, 0);
        movement.x = -CheckKeyPress(KeyCode.A) + CheckKeyPress(KeyCode.D);
        movement.y = -CheckKeyPress(KeyCode.S) + CheckKeyPress(KeyCode.W);

        rb.linearVelocity = moveSpeed * Time.deltaTime * movement.normalized;

        OrientSprite();
        SetAnimatorState();
    }

    public override void ExitState()
    {
        player.lastMovement = movement;
    }

    int CheckKeyPress(KeyCode key)
    {
        return Input.GetKey(key) ? 1 : 0;
    }

    void OrientSprite()
    {
        if (movement.x < 0)
        {
            sprite.flipX = true;
        }
        else if (movement.x > 0)
        {
            sprite.flipX = false;
        }
    }

    void SetAnimatorState()
    {
        anim.SetBool("isMoving", movement != Vector2.zero);
    }
}
