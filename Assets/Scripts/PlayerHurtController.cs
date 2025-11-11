using System.Collections;
using UnityEngine;

public class PlayerHurtController : PlayerController
{
    [SerializeField] private float knockbackSpeed = 1000f;
    private PlayerMoveController moveController;
    private Vector2 direction;

    protected override void Awake()
    {
        base.Awake();
        moveController = GetComponent<PlayerMoveController>();
    }

    private void Update()
    {
        if (isActive != true) return;
        rb.linearVelocity = knockbackSpeed * Time.deltaTime * direction;
    }

    public override void OnEnterState()
    {
        StartCoroutine(PushBack());
    }

    public override void ToggleState()
    {
        base.ToggleState();
        moveController.isActive = true;
        moveController.OnEnterState();
    }

    IEnumerator PushBack()
    {
        anim.SetTrigger("startHurt");
        yield return new WaitForSeconds(.66f);
        anim.SetTrigger("endHurt");
        ToggleState();
    }


    public void SetDiection(Vector2 v2)
    {
        direction = -v2.normalized;
    }

}
