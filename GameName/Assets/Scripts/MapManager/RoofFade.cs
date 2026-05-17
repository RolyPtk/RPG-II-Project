using UnityEngine;
using UnityEngine.Tilemaps;

public class RoofFade : MonoBehaviour
{
    [SerializeField] private Tilemap roof;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Trigger hit: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player intrat - fade roof");
            roof.color = new Color(1, 1, 1, 0.3f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            roof.color = new Color(1, 1, 1, 1f);
    }
}