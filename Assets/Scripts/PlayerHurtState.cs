using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerHurtState : PlayerState
{
    [SerializeField] private float knockbackSpeed = 1000f;
    [SerializeField] private AudioClip hurtAudio;
    private Vector2 direction;

    public override void EnterState(int arg)
    {
        direction = -player.lastMovement;
        StartCoroutine(Knockback());
    }

    public override void UpdateState()
    {
        player.rb.linearVelocity = knockbackSpeed * Time.deltaTime * direction;
    }

    IEnumerator Knockback()
    {
        player.anim.SetTrigger("startHurt");

        player.audioSource.Stop();
        player.audioSource.resource = hurtAudio;
        player.audioSource.volume = .4f;
        player.audioSource.pitch = Random.Range(.8f, 1.2f);
        player.audioSource.Play();

        yield return new WaitForSeconds(.66f);

        player.anim.SetTrigger("startIdle");
        player.SetState("Move");
    }


}
