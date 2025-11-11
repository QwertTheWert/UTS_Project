using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static readonly float offset = -10f;
    
    [SerializeField] private Transform target;

    private Camera mainCamera;
    private Bounds cameraBounds;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Start()
    {
        Debug.Assert(target != null, "Must have a target to follow (player).");
        
        float height = mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        float minX = Globals.worldBounds.min.x + width*2.25f;
        float maxX = Globals.worldBounds.extents.x - width*2;
        float minY = Globals.worldBounds.min.y + height*2.25f;
        float maxY = Globals.worldBounds.extents.y - height*2;

        cameraBounds = new();
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, offset),
            new Vector3(maxX, maxY, offset)
            );
    }

    private void LateUpdate()
    {
        Vector3 newPosition = GetClampedPosition(target.position);
        transform.position = newPosition;
    }

    Vector3 GetClampedPosition(Vector2 pos)
    {
        return new(
            Math.Clamp(pos.x, cameraBounds.min.x, cameraBounds.max.x),
            Math.Clamp(pos.y, cameraBounds.min.y, cameraBounds.max.y),
            offset
            );
    }
}
    