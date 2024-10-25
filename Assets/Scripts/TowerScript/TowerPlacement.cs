using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject selectedTower;
    private bool isPlacingTower = false;
    private TowerSpot selectedSpot;

    void Update()
    {
        if (isPlacingTower)
        {
            if (selectedSpot == null) return;

            Vector3 placementPosition = selectedSpot.transform.position;
            selectedTower.transform.position = placementPosition;

            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
    }

    public void StartPlacingTower()
    {
        if (towerPrefab != null)
        {
            selectedTower = Instantiate(towerPrefab);
            isPlacingTower = true;
        }
    }

    void PlaceTower()
    {
        if (selectedSpot != null)
        {
            isPlacingTower = false;
            selectedSpot.isOccupied = true;
            selectedSpot = null;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TowerSpot"))
        {
            TowerSpot spot = other.GetComponent<TowerSpot>();
            if (spot != null && !spot.isOccupied)
            {
                selectedSpot = spot;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TowerSpot"))
        {
            TowerSpot spot = other.GetComponent<TowerSpot>();
            if (spot == selectedSpot)
            {
                selectedSpot = null;
            }
        }
    }
}
