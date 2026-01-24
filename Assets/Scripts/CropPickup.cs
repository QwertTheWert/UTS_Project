using System.Collections.Generic;
using UnityEngine;



public class CropPickup : MonoBehaviour
{
    SpriteRenderer graphic;
    CropData cropData;

    private void Awake()
    {
        graphic = GetComponent<SpriteRenderer>();
    }

    public void SetCropData(CropData data)
    {
        cropData = data;
        graphic.sprite = cropData.cropPickupImage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SaveDataManager.CollectCrop(cropData);
            Destroy(gameObject);

        }
    }
}
