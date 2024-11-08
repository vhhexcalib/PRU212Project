using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerControl : MonoBehaviour
{
    public GameObject wizardTowerPrefab;
    public Transform spawnPosition;
    public Button createTowerButton; 

    private bool canCreateTower = true;

    public void CreateTower()
    {
        if (canCreateTower)
        {
            GameObject newTower = Instantiate(wizardTowerPrefab, spawnPosition.position, Quaternion.identity);
            StartCoroutine(DestroyTowerAfterTime(newTower, 20f));
            canCreateTower = false;
            if (createTowerButton != null)
            {
                createTowerButton.interactable = false;
            }
        }
    }

    private IEnumerator DestroyTowerAfterTime(GameObject tower, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(tower);
    }

    public void ResetTowerCreation()
    {
        canCreateTower = true;
        if (createTowerButton != null)
        {
            createTowerButton.interactable = true; 
        }
    }
}
