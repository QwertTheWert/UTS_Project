using UnityEngine;

public class Enemy : NPC
{
    public override void OnCollision(Player player)
    {
        base.OnCollision(player);
        player.SetState("Hurt");
        Debug.Log("Player is hurt");
    }

}
