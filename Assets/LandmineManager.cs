using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineManager : MonoBehaviour
{
    public GameObject landminePrefabs;
    public void PlaceMineAtSpot(MineSpot spot)
    {
        if (landminePrefabs != null && !spot.isOccupied && GameManager.instance.CanPlaceMine())
        {
            Instantiate(landminePrefabs, spot.transform.position, Quaternion.identity);
            spot.isOccupied = true;
            GameManager.instance.UseMine();
            Debug.Log("Placed a mine at: " + spot.name);
        }
        else
        {
            Debug.LogWarning("Cannot place mine here. Either spot is occupied or no mines available.");
        }
    }
}
