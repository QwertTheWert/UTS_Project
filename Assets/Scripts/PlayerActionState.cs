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
        if (arg == 0)
        {
            hitbox.offset = new Vector2(1f, 0.5f);
            hitbox.radius = 1;
        } else
        {
            hitbox.offset = new Vector2(.75f, 0.125f);
            hitbox.radius = .75f;
        }

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
            npc.OnHurt(player);
        }
        if (collision.CompareTag("CropPlant") && actionType == 1)
        {
            CropPlant cropPlant = collision.GetComponent<CropPlant>();
            cropPlant.OnShoveled();
        }

    }
}
