using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ActivaLumaPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Light2D luminaTorta = collision.GetComponentInChildren<Light2D>(true);

            if (luminaTorta != null)
            {
                luminaTorta.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Light2D luminaTorta = collision.GetComponentInChildren<Light2D>();

            if (luminaTorta != null)
            {
                luminaTorta.enabled = false;
            }
        }
    }
}