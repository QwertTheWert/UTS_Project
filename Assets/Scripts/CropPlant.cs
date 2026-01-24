using System.Collections;
using UnityEngine;

public class CropPlant : MonoBehaviour
{
    [SerializeField] GameObject cropPickupPrefab;
    [SerializeField] CropData cropData;

    SpriteRenderer spriteRenderer;

    public void Awake()
    {   
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        spriteRenderer.sprite = cropData.cropPlantImage;
    }

    public void OnShoveled()
    {
        GameObject cropPickup = Instantiate(cropPickupPrefab);
        cropPickup.transform.position = transform.position;
        cropPickup.GetComponent<CropPickup>().SetCropData(cropData);
        Destroy(gameObject);
    }
}
