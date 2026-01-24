using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    static public SaveData saveData;
    static public SaveDataManager active;
    static string saveFilePath;

    private void Awake()
    {
        active = this;
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");

        if (File.Exists(saveFilePath)) {
            string saveJson = File.ReadAllText(saveFilePath);
            saveData = JsonUtility.FromJson<SaveData>(saveJson);
        }
        else
        {
            saveData = new SaveData();
            File.WriteAllText(saveFilePath, JsonUtility.ToJson(saveData));
        }
    }

    public static void CollectCrop(CropData cropData)
    {
        switch (cropData.name)
        {
            case "Carrot":
                saveData.carrot += 1;
                break;
            case "Beetroot":
                saveData.beet += 1;
                break;
            case "Radish":
                saveData.radish += 1;
                break;
            case "Sunflower":
                saveData.sunflower += 1;
                break;
            case "Wheat":
                saveData.wheat += 1;
                break;
            case "Pumpkin":
                saveData.pumpkin += 1;
                break;
        }
        cropData.cropGathered.Invoke();
        File.WriteAllText(saveFilePath, JsonUtility.ToJson(saveData));
    }

}
