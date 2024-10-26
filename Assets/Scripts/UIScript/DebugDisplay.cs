using TMPro;
using UnityEngine;

public class DebugDisplay : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    private string currentMessage = "";

    private void Start()
    {
        if (debugText != null)
        {
            debugText.text = "";
        }
    }

    public void UpdateDebugMessage(string message)
    {
        currentMessage = message;
        debugText.text = currentMessage;
    }
}
