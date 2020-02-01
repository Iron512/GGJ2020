using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
}
