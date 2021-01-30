using UnityEngine;
using UnityEngine.SceneManagement;

public class CatLocationchecker
{
    private static float AnchorDistance = 5;
    private static GameObject[] CatLocations;
    private static int CatsPlaced = 0;

    public static bool CheckLocation(Vector3 position)
    {
        if (CatLocations == null)
        {
            CatLocations = GameObject.FindGameObjectsWithTag("CatLocation");
        }

        for (int i = 0; i < CatLocations.Length; ++i)
        {
            if (Vector3.Distance(position, CatLocations[i].transform.position) < AnchorDistance)
            {
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
