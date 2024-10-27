using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public GameObject towerPrefab;

    public void PlaceTowerAtSpot(TowerSpot spot)
    {
        if (towerPrefab != null && !spot.isOccupied && GameManager.instance.CanPlaceTower())
        {
            Instantiate(towerPrefab, spot.transform.position, Quaternion.identity);
            spot.isOccupied = true;
            GameManager.instance.UseTower();
            Debug.Log("Placed a tower at: " + spot.name);
        }
        else
        {
            Debug.LogWarning("Cannot place tower here. Either spot is occupied or no towers available.");
        }
    }
}
