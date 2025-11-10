using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 120f;
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.Set(0, 0);
        movement.x += Input.GetKey(KeyCode.A) ? -1 : 0;
        movement.x += Input.GetKey(KeyCode.D) ? 1 : 0;
        movement.y += Input.GetKey(KeyCode.S) ? -1 : 0;
        movement.y += Input.GetKey(KeyCode.W) ? 1 : 0;

        rb.linearVelocity = movement * moveSpeed * Time.deltaTime;
    }
}
