using System.Collections;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    [SerializeField] private float knockbackSpeed = 1000f;
    private Vector2 direction;

    public override void EnterState()
    {
        direction = -player.lastMovement;
        StartCoroutine(Knockback());
    }

    public override void UpdateState()
    {
        rb.linearVelocity = knockbackSpeed * Time.deltaTime * direction;
    }

    IEnumerator Knockback()
    {
        anim.SetTrigger("startHurt");

        yield return new WaitForSeconds(.66f);

        anim.SetTrigger("endHurt");
        player.SetState("Move");
    }


}
