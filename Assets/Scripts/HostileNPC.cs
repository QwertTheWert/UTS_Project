using System.Collections;
using UnityEngine;

public class HostileNPC : NPC
{

    [SerializeField] int defaultHealth = 3;
    [SerializeField] private float knockbackSpeed = 500f;
    [SerializeField] private float moveSpeed = 500f;
    [SerializeField] AudioClip hurtAudio;
    [SerializeField] AudioClip deathAudio;

    int currentHealth;
    CapsuleCollider2D hitbox;
    CircleCollider2D detectionRadius;
    Rigidbody2D rb;
    Animator anim;
    Transform target;
    bool isDamaged, movementPaused;
    Vector3 knockbackDirection;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        detectionRadius = GetComponent<CircleCollider2D>();
        hitbox = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        currentHealth = defaultHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
    }

    private void Update()
    {
        if (TutorialUI.isActive) { return; }
        if (currentHealth == 0) { return; }
        if (isDamaged)
        {
            rb.linearVelocity = knockbackDirection * Time.deltaTime * knockbackSpeed;
        }
        else if (target && !movementPaused)
        {
            rb.linearVelocity = (target.position - transform.position).normalized * Time.deltaTime * moveSpeed;
            if (rb.linearVelocityX == 0) { return; }
            Vector3 currentScale = transform.localScale;
            currentScale.x = (rb.linearVelocityX < 0) ? -1f : 1;
            transform.localScale = currentScale;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    public override void OnCollision(Player player)
    {
        base.OnCollision(player);
        player.SetState("Hurt");
        Debug.Log("Player is hurt");
        StartCoroutine(PauseMovement());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = null;
        }
    }

    public override void OnHurt(Player player)
    {
        currentHealth--;
        if (currentHealth < 0) { return; }
        else if (currentHealth == 0) {
            knockbackDirection = Vector3.zero;
            isDamaged = false;
            movementPaused = false;
            StopAllCoroutines();
            StartCoroutine(Die());
        }
        else
        {
            knockbackDirection = (transform.position - player.transform.position).normalized;
            movementPaused = false;
            StopAllCoroutines();
            StartCoroutine(TakeDamage());
        }
    }

    IEnumerator PauseMovement()
    {
        movementPaused = true;
        yield return new WaitForSeconds(1f);
        movementPaused = false;
    }

    IEnumerator TakeDamage()
    {
        isDamaged = true;
        anim.SetTrigger("toHurt");

        audioSource.Stop();
        audioSource.resource = hurtAudio;
        audioSource.pitch = Random.Range(.8f, 1.2f);
        audioSource.Play();

        yield return new WaitForSeconds(.66f);
        anim.SetTrigger("toIdle");
        isDamaged = false;
    }

    IEnumerator Die()
    {
        anim.SetTrigger("toDeath");

        audioSource.Stop();
        audioSource.resource = deathAudio;
        audioSource.pitch = Random.Range(.8f, 1.2f);
        audioSource.Play();
        hitbox.enabled = false;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(10f);

        currentHealth = defaultHealth;
        hitbox.enabled = true;
        anim.SetTrigger("toIdle");

        if (target)
        {
            if ((target.transform.position - transform.position).magnitude > 3)  target = null; 
        }
    }
}
