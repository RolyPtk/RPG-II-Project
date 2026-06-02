using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private List<Quest> activeQuests = new List<Quest>();

    void Awake(){
        if (Instance != null && Instance != this) { 
            Destroy(gameObject); 
            return; 
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddQuest(Quest quest){
        if (!HasQuest(quest.id)){
            activeQuests.Add(quest);
            Debug.Log($"Quest added: {quest.id}");
        }
    }

    public void RemoveQuest(string id){
        Quest q = GetQuest(id);

        if (q == null) return;
        
        foreach (QuestRequirement r in q.requirements)
            r.Unsubscribe();
        activeQuests.Remove(q);
    }

    public bool HasQuest(string id) => GetQuest(id) != null;

    public bool IsCompleted(string id){
        Quest q = GetQuest(id);
        return q != null && q.isCompleted;
    }

    public Quest GetQuest(string id) => activeQuests.Find(q => q.id == id);

    public bool IsQuestActive(string questId){
        foreach(Quest q in activeQuests)
            if (q.id == questId)
                return true;
        return false; 
    }
}