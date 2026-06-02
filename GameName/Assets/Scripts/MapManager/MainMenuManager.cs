using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string primaScena = "Level1";
    public void ApasaNewGame()
    {
        //PlayerPrefs.DeleteKey("UltimulNivelSalvat");

        SceneManager.LoadScene(primaScena);
    }

    public void ApasaContinue()
    {
        //string scenaSalvata = PlayerPrefs.GetString("UltimulNivelSalvat", primaScena);

        //SceneManager.LoadScene(scenaSalvata);
    }

    public void ApasaLoadGame()
    {
        Debug.Log("Aici vom deschide panoul cu mai multe salvari!");
    }

    public void ApasaQuit()
    {
        Application.Quit();
        Debug.Log("Jocul s-a inchis (merge doar in Build-ul final, nu in editor)");
    }
}