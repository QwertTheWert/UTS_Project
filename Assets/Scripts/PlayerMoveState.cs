using UnityEngine;

public class PlayerMoveState : PlayerState
{
    [SerializeField] private float moveSpeed = 120f;
    private Vector2 movement;

    public override void UpdateState()
    {
        if (CheckActionInput()) { return; }

        movement.Set(0, 0);
        movement.x = -CheckKeyPress(KeyCode.A) + CheckKeyPress(KeyCode.D);
        movement.y = -CheckKeyPress(KeyCode.S) + CheckKeyPress(KeyCode.W);

        player.rb.linearVelocity = moveSpeed * Time.deltaTime * movement.normalized;

        OrientSprite();
        SetAnimatorState();
    }
    
    public override void ExitState()
    {
        player.lastMovement = movement;
    }

    bool CheckActionInput()
    {
        if (CheckKeyPress(KeyCode.Mouse0) == 1) {
            player.SetState("Action", 0);
            return true; }
        if (CheckKeyPress(KeyCode.Mouse1) == 1) { 
            player.SetState("Action", 1);
            return true; }
        return false;
    }

    int CheckKeyPress(KeyCode key)
    {
        return Input.GetKey(key) ? 1 : 0;
    }

    void OrientSprite()
    {
        if (movement.x == 0) { return; };
        Vector3 currentScale = player.transform.localScale;
        currentScale.x = (movement.x < 0) ? -1f : 1;
        player.transform.localScale = currentScale;


    }

    void SetAnimatorState()
    {
        player.anim.SetBool("isMoving", movement != Vector2.zero);
    }
}
