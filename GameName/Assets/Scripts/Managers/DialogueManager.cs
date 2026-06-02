using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("Panel")]
    public GameObject dialoguePanel;

    [Header("Text Elements")]
    public TMP_Text npcNameText;
    public TMP_Text dialogueText;
    public TMP_Text continuePrompt;

    [Header("Choice")]
    public GameObject choicePanel;
    public Button acceptButton;
    public Button declineButton;

    private string[] lines;
    private int currentLine;
    private Action onAccept;

    private int frameDialogueStarted;

    public bool IsActive => dialoguePanel.activeSelf;

    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(gameObject); 
            return; 
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        dialoguePanel.SetActive(false);
    }

    void Update(){
        if (IsActive && !choicePanel.activeSelf && InputManager.Instance.IsPressed(InputManager.Instance.interactKey))
            if (Time.frameCount > frameDialogueStarted) 
                ShowNextLine();
    }

    public void StartDialogue(string npcName, string[] lines, Action onAccept = null){
        this.lines = lines;
        this.currentLine = 0;
        this.onAccept = onAccept;

        frameDialogueStarted = Time.frameCount;
        
        npcNameText.text = npcName;
        dialogueText.text = lines[0];

        choicePanel.SetActive(false);
        continuePrompt.gameObject.SetActive(true);
        dialoguePanel.SetActive(true);
    }

    void ShowNextLine(){
        currentLine++;
        if (currentLine >= lines.Length){
            if (onAccept != null)
                ShowChoicePrompt();
            else
                EndDialogue();
            return;
        }
        dialogueText.text = lines[currentLine];
    }

    void ShowChoicePrompt(){
        continuePrompt.gameObject.SetActive(false);
        choicePanel.SetActive(true);

        acceptButton.onClick.RemoveAllListeners();
        declineButton.onClick.RemoveAllListeners();

        acceptButton.onClick.AddListener(() =>
        {
            onAccept?.Invoke();
            EndDialogue();
        });

        declineButton.onClick.AddListener(() =>
        {
            EndDialogue();
        });
    }

    public void EndDialogue(){
        choicePanel.SetActive(false);
        continuePrompt.gameObject.SetActive(true);
        dialoguePanel.SetActive(false);
        onAccept = null;
    }
}