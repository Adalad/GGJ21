using UnityEngine;
using UnityEngine.SceneManagement;

public class CatLocationchecker
{
    public static int CatsPlaced = 0;
    private static float AnchorDistance = 5;
    private static GameObject[] CatLocations;

    public static bool CheckLocation(Vector3 position)
    {
        if (CatLocations == null)
        {
            CatLocations = GameObject.FindGameObjectsWithTag("CatLocation");
        }

        for (int i = 0; i < CatLocations.Length; ++i)
        {
            if (CatLocations[i].activeSelf && (Vector3.Distance(position, CatLocations[i].transform.position) < AnchorDistance))
            {
                CatLocations[i].SetActive(false);
                CatsPlaced++;
                if (CatsPlaced == 15)
                {
                    SceneManager.LoadScene("EndScene");
                }

                return true;
            }
        }

        return false;
    }
}
