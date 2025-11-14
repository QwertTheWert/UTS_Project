using UnityEngine;

public class Crop : NPC
{
    public override void OnCollision(Player player)
    {
        base.OnCollision(player);
        player.stats.collectedCrops++;
        Debug.Log(string.Format("Crops collected: {0}", player.stats.collectedCrops));
        Destroy(gameObject);
    }
}
