using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuUI;

    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = !isOpen;

            menuUI.SetActive(isOpen);

            Time.timeScale = isOpen ? 0f : 1f;

            Debug.Log(Time.timeScale);
        }
    }

    public void Resume()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {

        Player player = FindFirstObjectByType<Player>();

        DatabaseManager.Instance.SavePlayer(
            player.ToData()
        );

        GameStateData gameState = new GameStateData();

        gameState.PlayerPosX = player.transform.position.x;
        gameState.PlayerPosY = player.transform.position.y;

        DatabaseManager.Instance.SaveGameState(gameState);

        Debug.Log("Game Saved!");
    }
}