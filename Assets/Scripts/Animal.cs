using UnityEngine;

public class Animal : NPC
{
    public string animalNoise;
    public override void OnCollision(Player player)
    {
        base.OnCollision(player);
        Debug.Log(animalNoise);
    }
}
