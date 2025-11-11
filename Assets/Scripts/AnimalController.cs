using UnityEngine;

public class AnimalController : NPCController
{
    public string animalNoise;
    public override void OnCollision(PlayerMoveController player)
    {
        Debug.Log(animalNoise);
    }
}
