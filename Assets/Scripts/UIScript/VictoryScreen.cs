using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class NextLevelScreen : MonoBehaviour
{
    public GameObject nextLevelScreen;
    public TextMeshProUGUI victoryMessage;
    public Button nextLevelButton;

    private void Start()
    {
        nextLevelScreen.SetActive(false);
        nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
    }

    public void ShowNextLevelScreen(string message)
    {
        nextLevelScreen.SetActive(true);
        victoryMessage.text = message;
    }

    private void OnNextLevelButtonClick()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels to load. Game complete!");
        }
    }
}
