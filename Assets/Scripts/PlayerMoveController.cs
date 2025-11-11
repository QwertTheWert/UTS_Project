using UnityEngine;

public class PlayerMoveController : PlayerController
{
    [SerializeField] private float moveSpeed = 120f;
    private Vector2 movement;
    private PlayerHurtController hurtController;

    protected override void Awake()
    {
        base.Awake();
        hurtController = GetComponent<PlayerHurtController>();
    }

    void Update()
    {
        if (!isActive) return;

        movement.Set(0, 0);
        movement.x = -CheckKeyPress(KeyCode.A) + CheckKeyPress(KeyCode.D);
        movement.y = -CheckKeyPress(KeyCode.S) + CheckKeyPress(KeyCode.W);

        rb.linearVelocity = moveSpeed * Time.deltaTime * movement.normalized;

        OrientSprite();
        SetAnimatorState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("NPC"))
        {
            NPCController npc = other.GetComponent<NPCController>();
            npc.OnCollision(this);
        }
        ;
    }

    public override void ToggleState()
    {
        base.ToggleState();
        hurtController.SetDiection(movement);
        hurtController.isActive = true;
        hurtController.OnEnterState();
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
