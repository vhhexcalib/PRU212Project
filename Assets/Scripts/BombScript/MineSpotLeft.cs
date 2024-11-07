using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MineSpotLeft : MonoBehaviour
{
    public TextMeshProUGUI mineplacementText;
    private string currentMessage = "";
    private void Start()
    {
        if (mineplacementText != null)
        {
            mineplacementText.text = "";
        }
    }

    public void UpdateMineMessage(string message)
    {
        currentMessage = message;
        mineplacementText.text = currentMessage;
    }
}
