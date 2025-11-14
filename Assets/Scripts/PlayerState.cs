using UnityEngine;

public class PlayerState : MonoBehaviour
{
    protected Player player;
    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;
    protected Animator anim;
    protected virtual void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        sprite = rb.GetComponent<SpriteRenderer>();
        anim = rb.GetComponent<Animator>();
    }

    public virtual void EnterState()
    {

    }

    public virtual void UpdateState()
    {

    }

    public virtual void ExitState()
    {

    }
}
