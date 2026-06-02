using UnityEngine;

public class StairTransition : MonoBehaviour
{
    [Header("Setari Afisare (Vizual)")]
    public int targetOrderInLayer;

    [Header("Setari Fizica (Coliziuni)")]
    [Tooltip("Scrie exact: PlayerSus sau PlayerJos")]
    public string targetPhysicsLayerName;

    [Header("Setari Etaj")]
    public bool isGoingUp;
    public RoofFade[] elementeAcoperis;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpriteRenderer playerSprite = collision.GetComponent<SpriteRenderer>();
            if (playerSprite != null)
            {
                playerSprite.sortingOrder = targetOrderInLayer;
            }

            int targetLayer = LayerMask.NameToLayer(targetPhysicsLayerName);
            if (targetLayer != -1)
            {
                collision.gameObject.layer = targetLayer;
            }

            foreach (RoofFade fadeScript in elementeAcoperis)
            {
                if (isGoingUp)
                {
                    fadeScript.isPlayerUpstairs = true;
                    fadeScript.ForceOpaque();
                }
                else
                {
                    fadeScript.isPlayerUpstairs = false;
                }
            }
        }
    }
}