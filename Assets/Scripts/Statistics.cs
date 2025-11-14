using UnityEngine;

[CreateAssetMenu(fileName = "Statistics", menuName = "Scriptable Objects/Statistics")]
public class Statistics : ScriptableObject
{
    [System.NonSerialized] public int collectedCrops = 0;
}
