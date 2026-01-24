using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CropData", menuName = "Scriptable Objects/Crop Data")]
public class CropData : ScriptableObject
{
    public UnityEvent cropGathered;
    public Sprite cropPlantImage;
    public Sprite cropPickupImage;
}
