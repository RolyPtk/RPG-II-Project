using UnityEngine;
using UnityEngine.Tilemaps;

public class RoofFade : MonoBehaviour
{
    [SerializeField] private Tilemap roof;
    public bool isPlayerUpstairs = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerUpstairs)
        {
            roof.color = new Color(1, 1, 1, 0.3f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerUpstairs)
        {
            roof.color = new Color(1, 1, 1, 0.3f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Cand iese complet din zona, redevine opac
        if (other.CompareTag("Player"))
        {
            ForceOpaque();
        }
    }

    public void ForceOpaque()
    {
        roof.color = new Color(1, 1, 1, 1f);
    }
}