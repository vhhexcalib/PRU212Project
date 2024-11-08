using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    public GameObject wizardTowerPrefab;
    public Transform spawnPosition; 
    public void CreateTower()
    {
        GameObject newTower = Instantiate(wizardTowerPrefab, spawnPosition.position, Quaternion.identity);
        StartCoroutine(DestroyTowerAfterTime(newTower, 20f));
    }
    private IEnumerator DestroyTowerAfterTime(GameObject tower, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(tower);
    }
}
