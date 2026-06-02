using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PreviousSceneController : MonoBehaviour
{
   
    public string numeScenaDestinatie;
    public static PreviousSceneController Instance;
    [SerializeField] Animator transitionAnim;
    public Image fadeImage;
    public float fadeDuration = 1f;

    private bool playerEsteLaUsa = false;

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
    private void Update()
    {
        if (playerEsteLaUsa == true && Input.GetKeyDown(KeyCode.E))
        {
            LoadScene(numeScenaDestinatie);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEsteLaUsa = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEsteLaUsa = false;
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        transitionAnim.SetTrigger("Start");
    }
}