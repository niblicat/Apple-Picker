using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadScene(string sceneName) {
        if (ControlsScript.isPaused || ControlsScript.gameOverFlag) {
            Time.timeScale = 1.0f;
            ControlsScript.gameOverFlag = false;
            ControlsScript.isPaused = false;
            if (SceneManager.GetActiveScene().name == "AppleGame") {
                ControlsScript.ToggleVisualBlur(false);
            }
        }
        SceneManager.LoadScene(sceneName);
    }
    public static void QuitGame() {
        Application.Quit();
    }
}
