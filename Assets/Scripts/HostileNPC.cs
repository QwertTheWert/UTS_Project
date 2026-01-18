using System.Collections;
using UnityEngine;

public class HostileNPC : NPC
{

    [SerializeField] int defaultHealth = 3;
    [SerializeField] AudioClip hurtAudio;
    [SerializeField] AudioClip deathAudio;

    int currentHealth;
    CircleCollider2D collider;
    Animator anim;

    protected override void Awake()
    {
        base.Awake();
        collider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        currentHealth = defaultHealth;
    }

    public override void OnCollision(Player player)
    {
        base.OnCollision(player);
        player.SetState("Hurt");
        Debug.Log("Player is hurt");
    }


    public override void OnHurt()
    {
        currentHealth--;
        if (currentHealth < 0) { return; }
        else if (currentHealth == 0) {
            StopAllCoroutines();
            StartCoroutine(Die());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(TakeDamage());
        }
    }

    IEnumerator TakeDamage()
    {
        anim.SetTrigger("toHurt");

        audioSource.Stop();
        audioSource.resource = hurtAudio;
        audioSource.pitch = Random.Range(.8f, 1.2f);
        audioSource.Play();

        yield return new WaitForSeconds(2f);
        anim.SetTrigger("toIdle");
    }

    IEnumerator Die()
    {
        anim.SetTrigger("toDeath");

        audioSource.Stop();
        audioSource.resource = deathAudio;
        audioSource.pitch = Random.Range(.8f, 1.2f);
        audioSource.Play();

        collider.enabled = false;

        yield return new WaitForSeconds(10f);

        currentHealth = defaultHealth;
        collider.enabled = true;
        anim.SetTrigger("toIdle");
    }


}
