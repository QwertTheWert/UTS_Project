using System.Collections;
using UnityEngine;

public class PlayerActionState : PlayerState
{
    public CircleCollider2D hitbox;

    [Header("Audio")]
    [SerializeField] AudioClip swordAudio;
    [SerializeField] AudioClip shovelAudio;

    int actionType;
    AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    public override void EnterState(int arg)
    {
        hitbox.enabled = true;
        player.rb.linearVelocity = Vector3.zero;
        actionType = arg;
        StartCoroutine(RunAnimation());
    }

    public override void ExitState()
    {
        hitbox.enabled = false;
    }

    IEnumerator RunAnimation()
    {
        player.anim.SetTrigger(actionType == 0 ? "startAttack" : "startDig");


        audioSource.Stop();
        audioSource.resource = actionType == 0 ? swordAudio : shovelAudio;
        audioSource.volume = actionType == 0 ? 0.33f : 1f;
        audioSource.pitch = Random.Range(.8f, 1.2f);
        audioSource.Play();

        yield return new WaitForSeconds(.66f);

        player.anim.SetTrigger("startIdle");
        player.SetState("Move");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("FriendlyNPC") || collision.CompareTag("HostileNPC")) && actionType == 0)
        {
            NPC npc = collision.CompareTag("FriendlyNPC") ? 
                collision.GetComponent<FriendlyNPC>() : collision.GetComponent<HostileNPC>();
            npc.OnHurt();
        }


    }
}
