using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject GamePanel;
    public GameObject MapPanel;
    public Text CountText;
    public Transform BoatGO;
    public RectTransform BoatUI;

    public void OpenMap()
    {
        CountText.text = string.Format("{0} / 15", CatLocationchecker.CatsPlaced);
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

    private void Update()
    {
        if (BoatGO != null)
        {
            Vector3 pos = BoatGO.position * 10;
            BoatUI.anchoredPosition = pos;
        }
    }
}
