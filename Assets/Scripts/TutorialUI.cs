using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public static bool isActive = true;
    public  GameObject tutorialPanel;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetEnabled(true);
        }
    }

    public void SetEnabled(bool value)
    {
        isActive = value;
        tutorialPanel.SetActive(value);
    }
}
