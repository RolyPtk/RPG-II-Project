using UnityEngine;
using UnityEngine.Audio;

public class TranzitieAudio : MonoBehaviour
{
    [Tooltip("Trage aici snapshot-ul cand esti INAUNTRU")]
    public AudioMixerSnapshot stareInauntru;

    [Tooltip("Trage aici snapshot-ul cand esti AFARA")]
    public AudioMixerSnapshot stareAfara;

    [Tooltip("In cate secunde sa se faca trecerea treptata?")]
    public float timpTranzitie = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stareInauntru.TransitionTo(timpTranzitie);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stareAfara.TransitionTo(timpTranzitie);
        }
    }
}