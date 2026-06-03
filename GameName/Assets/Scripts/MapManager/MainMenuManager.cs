using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string primaScena = "Level1";
    public void ApasaNewGame()
    {
        //PlayerPrefs.DeleteKey("UltimulNivelSalvat");

        DatabaseManager.Instance.DeleteSave();

        SceneManager.LoadScene(primaScena);
    }

    public void ApasaLoadGame()
    {
        GameStateData gameState =
       DatabaseManager.Instance.LoadGameState();

        if (gameState == null)
        {
            Debug.Log("Nu exista salvare!");
            return;
        }

        PlayerPrefs.SetString(
            "SavedScene",
            gameState.SceneName
        );

        SceneManager.LoadScene(
            gameState.SceneName
        );
    }

    public void ApasaQuit()
    {
        Application.Quit();
        Debug.Log("Jocul s-a inchis (merge doar in Build-ul final, nu in editor)");
    }
}