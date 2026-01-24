using UnityEngine;
using UnityEngine.UI;

public class CropCounter : MonoBehaviour
{
    public CropData cropData;
    [SerializeField] Image graphic;
    [SerializeField] Text label;

    private void Start()
    {
        graphic.sprite = cropData.cropPickupImage;
        switch (cropData.name)
        {
            case "Carrot":
                label.text = SaveDataManager.saveData.carrot.ToString();
                break;
            case "Beetroot":
                label.text = SaveDataManager.saveData.beet.ToString();
                break;
            case "Radish":
                label.text = SaveDataManager.saveData.radish.ToString();
                break;
            case "Sunflower":
                label.text = SaveDataManager.saveData.sunflower.ToString();
                break;
            case "Wheat":
                label.text = SaveDataManager.saveData.wheat.ToString();
                break;
            case "Pumpkin":
                label.text = SaveDataManager.saveData.pumpkin.ToString();
                break;
        }
        cropData.cropGathered.AddListener(IncrementCounter);
    }
    public void IncrementCounter()
    {
        label.text = (int.Parse(label.text) + 1).ToString();
    }
}
