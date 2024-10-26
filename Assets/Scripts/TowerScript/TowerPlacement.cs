using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject selectedTower;
    private bool isPlacingTower = false;
    private TowerSpot selectedSpot;
    public Camera mainCamera;
    public LayerMask placementLayerMask;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (isPlacingTower)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, placementLayerMask);

            if (hit.collider != null && hit.collider.CompareTag("TowerSpot"))
            {
                TowerSpot spot = hit.collider.GetComponent<TowerSpot>();
                if (spot != null && !spot.isOccupied)
                {
                    Debug.Log("Valid TowerSpot detected.");
                    selectedSpot = spot;
                    selectedTower.transform.position = selectedSpot.transform.position;

                    if (Input.GetMouseButtonDown(0))
                    {
                        PlaceTower();
                    }
                }
            }
            else
            {
                selectedSpot = null;
                selectedTower.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
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
            selectedTower = null;
            selectedSpot = null;
        }
    }
}
