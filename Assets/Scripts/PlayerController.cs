using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;
    protected Animator anim;
    public bool isActive = true;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = rb.GetComponent<SpriteRenderer>();
        anim = rb.GetComponent<Animator>();
    }

    public virtual void OnEnterState()
    {

    }

    public virtual void ToggleState()
    {
        isActive = false;

    }

}
