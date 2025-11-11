using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    [SerializeField] private Tilemap waterTilemap;
    void Awake()
    {
        Debug.Assert(waterTilemap != null, "Must set a water tilemap.");
        SetWorldBounds();
    }

    void SetWorldBounds()
    {
        BoundsInt intBounds = waterTilemap.cellBounds;
        Globals.worldBounds = new((Vector3)intBounds.center, (Vector3)intBounds.size);
    }

}
