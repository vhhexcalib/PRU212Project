using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    public bool isOccupied = false;
    public TowerPlacement towerPlacement;

    private void OnMouseDown()
    {
        if (!isOccupied)
        {
            towerPlacement.PlaceTowerAtSpot(this);
        }
    }
}
