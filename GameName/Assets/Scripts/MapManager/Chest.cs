using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Sprite closedSpriteBottom;
    [SerializeField] private Sprite openSpriteBottom;
    [SerializeField] private Sprite closedSpriteTop;
    [SerializeField] private Sprite openSpriteTop;

    [SerializeField] private SpriteRenderer rendererBottom;
    [SerializeField] private SpriteRenderer rendererTop;
    [SerializeField] private GameObject interactPrompt; // un TextMeshPro GameObject
    private bool _isOpen = false;
    private bool _playerNearby = false;

    void Start()
    {
        rendererBottom.sprite = closedSpriteBottom;
        rendererTop.sprite = closedSpriteTop;
    }

    void OpenChest()
    {
        _isOpen = true;
        rendererBottom.sprite = openSpriteBottom;
        rendererTop.sprite = openSpriteTop;
        Debug.Log("Chest deschis!");
    }

    void Update()
    {
        if (_playerNearby && Input.GetKeyDown(KeyCode.E) && !_isOpen)
        {
            OpenChest();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNearby = true;
            if (interactPrompt != null) interactPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNearby = false;
            if (interactPrompt != null) interactPrompt.SetActive(false);
        }
    }
}