using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Info")]
    public string npcId;
    public string npcName = "Villager";

    [Header("Dialogue")]
    public string[] defaultLines = { "Hello traveller!" };
    public string[] questLines = { "I need your help!", "Will you accept?" };
    public string[] questActiveLines = { "Have you finished yet?" };
    public string[] questCompleteLines = { "Thank you so much!" };

    [Header("Quest")]
    public bool hasQuest = false;
    public string questId;
    public bool slimeQuest = false;
    public bool flowerQuest = false;
    public bool yappingQuest = false;

    [Header("Quest Target")]
    public bool isQuestTarget = false;
    public string targetForQuestId; // The ID of the quest he is waiting for
    
    private bool hasDeliveredTargetLines = false;

    private Quest quest;
    private bool playerNearby = false;
    private bool questGiven = false;

    // requirement counters
    private int slimesKilled = 0;
    private int flowersGathered = 0;
    private bool npcTalkedTo = false;

    void Start(){
        if(hasQuest)
            BuildQuest();
    }

    void BuildQuest(){
        quest = new Quest(questId);

        if(slimeQuest)
            quest.AddRequirement(new QuestRequirement(
                description: "Kill 1 slime",
                checkCompletion: () => slimesKilled >= 1,
                subscribe: () => GameEvents.OnEnemyKilled += OnEnemyKilled,
                unsubscribe: () => GameEvents.OnEnemyKilled -= OnEnemyKilled,
                onComplete: () => Debug.Log("Slimes done!")
            ));

        if(flowerQuest)
            quest.AddRequirement(new QuestRequirement(
                description: "Gather 3 flowers",
                checkCompletion: () => flowersGathered >= 3,
                subscribe: () => GameEvents.OnItemGathered += OnItemGathered,
                unsubscribe: () => GameEvents.OnItemGathered -= OnItemGathered,
                onComplete: () => Debug.Log("Flowers done!")
            ));

        if(yappingQuest)
            quest.AddRequirement(new QuestRequirement(
                description: "Talk to Grandpa",
                checkCompletion: () => npcTalkedTo,
                subscribe: () => GameEvents.OnNPCTalkedTo += OnNPCTalkedTo,
                unsubscribe: () => GameEvents.OnNPCTalkedTo -= OnNPCTalkedTo,
                onComplete: () => Debug.Log("Grandpa talked to!")
            ));
        if(npcId == "Grandpa"){
            questGiven = true;
            hasQuest = false;
        }
    }

    void OnEnemyKilled(string enemyType){
        if(enemyType == "slime"){ 
            slimesKilled++;
            quest?.CheckRequirements();
        }
    }

    void OnItemGathered(string itemType){
        if(itemType == "flower"){
            flowersGathered++;
            quest?.CheckRequirements();
        }
    }

    void OnNPCTalkedTo(string talkedToId){
        if(talkedToId == "Grandpa"){
            npcTalkedTo = true;
            quest?.CheckRequirements();
        }
    }

    void Update(){
        if(playerNearby && !DialogueManager.Instance.IsActive && InputManager.Instance.IsPressed(InputManager.Instance.interactKey))
            TalkToPlayer();
    }

    void TalkToPlayer(){
        GameEvents.NPCTalkedTo(npcId);

        if(hasQuest){
            if(!questGiven)
                DialogueManager.Instance.StartDialogue(npcName, questLines, OnQuestAccepted);
            else if(quest.isCompleted)
                DialogueManager.Instance.StartDialogue(npcName, questCompleteLines);
            else 
                DialogueManager.Instance.StartDialogue(npcName, questActiveLines);
        }
        else if(isQuestTarget){
            bool playerHasQuest = QuestManager.Instance.IsQuestActive(targetForQuestId);

            if (!playerHasQuest)
                DialogueManager.Instance.StartDialogue(npcName, defaultLines);
            
            else if (!hasDeliveredTargetLines) {
                hasDeliveredTargetLines = true;
                DialogueManager.Instance.StartDialogue(npcName, questActiveLines);
            }
            else DialogueManager.Instance.StartDialogue(npcName, questCompleteLines);
        }
        else DialogueManager.Instance.StartDialogue(npcName, defaultLines);
    }

    void OnQuestAccepted(){
        questGiven = true;
        QuestManager.Instance.AddQuest(quest);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player")){
            playerNearby = false;

            if (DialogueManager.Instance.IsActive)
                DialogueManager.Instance.EndDialogue();
        }
    }

    void OnDestroy(){
        if (quest != null)
            foreach (QuestRequirement r in quest.requirements)
                r.Unsubscribe();
    }
}