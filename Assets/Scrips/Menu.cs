using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject GamePanel;
    public GameObject MapPanel;

    public void OpenMap()
    {
        GamePanel.SetActive(false);
        MapPanel.SetActive(true);
    }

    public void CloseMap()
    {
        MapPanel.SetActive(false);
        GamePanel.SetActive(true);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
