using UnityEngine;

public class IndicatorSimplu : MonoBehaviour
{
    [Tooltip("Trage aici obiectul TextMesaj din Hierarchy")]
    public GameObject textulDeAfisat;

    void Start()
    {
        if (textulDeAfisat != null)
        {
            textulDeAfisat.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textulDeAfisat.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textulDeAfisat.SetActive(false);
        }
    }
}