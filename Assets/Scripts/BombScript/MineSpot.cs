using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpot : MonoBehaviour
{
    public bool isOccupied = false;
    public LandmineManager landmineManager;
    private void OnMouseDown()
    {
        if (!isOccupied)
        {
            landmineManager.PlaceMineAtSpot(this);
        }
    }
}
