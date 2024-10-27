using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerSpotLeft : MonoBehaviour
{
    public TextMeshProUGUI towerplacementText;
    private string currentMessage = "";

    private void Start()
    {
        if (towerplacementText != null)
        {
            towerplacementText.text = "";
        }
    }

    public void UpdateTowerMessage(string message)
    {
        currentMessage = message;
        towerplacementText.text = currentMessage;
    }
}
