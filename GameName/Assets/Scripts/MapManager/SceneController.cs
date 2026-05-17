using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next < SceneManager.sceneCountInBuildSettings)
            StartCoroutine(TransitionToScene(next));
        else
            Debug.Log("Ai terminat jocul!");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(TransitionToSceneByName(sceneName));
    }

    private IEnumerator TransitionToScene(int sceneIndex)
    {
        yield return StartCoroutine(Fade(0f, 1f));
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneIndex);
        load.allowSceneActivation = false;
        while (load.progress < 0.9f)
            yield return null;
        load.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator TransitionToSceneByName(string sceneName)
    {
        yield return StartCoroutine(Fade(0f, 1f));
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
        load.allowSceneActivation = false;
        while (load.progress < 0.9f)
            yield return null;
        load.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float from, float to)
    {
        if (fadeImage == null) yield break;
        float elapsed = 0f;
        Color c = fadeImage.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, elapsed / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
        c.a = to;
        fadeImage.color = c;
    }
}