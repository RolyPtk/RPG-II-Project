using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public KeyCode interactKey = KeyCode.E;
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveLeft = KeyCode.A;

    public bool IsPressed(KeyCode key) => Input.GetKeyDown(key);
    public bool IsHeld(KeyCode key) => Input.GetKey(key);
    public bool IsReleased(KeyCode key) => Input.GetKeyUp(key);

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}