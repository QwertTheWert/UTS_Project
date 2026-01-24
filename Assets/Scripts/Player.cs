using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public Vector2 lastMovement;

    [Header("Reference")]
    [SerializeField] PlayerState startingState;
    public PlayerActionState actionState;
    public SpriteRenderer toolSprite;


    PlayerMoveState moveState;
    PlayerHurtState hurtState;


    [NonSerialized] public Rigidbody2D rb;
    [NonSerialized] public SpriteRenderer sprite;
    [NonSerialized] public Animator anim;
    [NonSerialized] public AudioSource audioSource;

    PlayerState activeState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = rb.GetComponent<SpriteRenderer>();
        anim = rb.GetComponent<Animator>();
        audioSource = rb.GetComponent<AudioSource>();

        moveState = GetComponent<PlayerMoveState>();
        hurtState = GetComponent<PlayerHurtState>();

        Debug.Assert(startingState != null, "Must have a starting state");
        activeState = startingState;
        activeState.EnterState(0);
    }

    void Update()
    {
        if (TutorialUI.isActive) { return;  }
        activeState.UpdateState();
    }

    public void SetState(string stateName, int args = 0)
    {
        PlayerState oldState = activeState;
        switch (stateName)
        {
            case "Hurt":
                activeState = hurtState;
                break;
            case "Move":
                activeState = moveState;
                break;
            case "Action":
                activeState = actionState;
                break;
            default: 
                break;
        }

        if (activeState)
        {
            oldState.ExitState();
            activeState.EnterState(args);
        }
        else
        {
            activeState = oldState;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("FriendlyNPC") || other.CompareTag("HostileNPC"))
        {
            NPC npc = other.GetComponent<NPC>();
            npc.OnCollision(this);
        }
        ;
    }
}
