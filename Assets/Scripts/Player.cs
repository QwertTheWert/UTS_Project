using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerState startingState;
    public Statistics stats;
    public Vector2 lastMovement;

    private PlayerMoveState moveState;
    private PlayerHurtState hurtState;

    private PlayerState activeState;

    private void Awake()
    {
        moveState = GetComponent<PlayerMoveState>();
        hurtState = GetComponent<PlayerHurtState>();

        Debug.Assert(startingState != null, "Must have a starting state");
        activeState = startingState;
        activeState.EnterState();
    }

    private void Update()
    {
        activeState.UpdateState();
    }

    public void SetState(string stateName)
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
            default: 
                break;
        }

        if (activeState)
        {
            oldState.ExitState();
            activeState.EnterState();
        }
        else
        {
            activeState = oldState;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("NPC"))
        {
            NPC npc = other.GetComponent<NPC>();
            npc.OnCollision(this);
        }
        ;
    }
}
