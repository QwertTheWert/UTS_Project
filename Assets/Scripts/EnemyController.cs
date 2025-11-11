using UnityEngine;

public class EnemyController : NPCController
{
    public override void OnCollision(PlayerMoveController player)
    {
        player.ToggleState();
        Debug.Log("Player is hurt");
    }

}
