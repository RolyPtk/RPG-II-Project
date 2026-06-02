using System;

public class QuestRequirement
{
    public string description;
    private Func<bool> checkCompletion;
    private Action onComplete;
    private Action unsubscribe;
    private bool completed = false;

    public bool IsComplete => completed;

    public QuestRequirement(string description, Func<bool> checkCompletion, Action subscribe, Action unsubscribe, Action onComplete = null){
        this.description = description;
        this.checkCompletion = checkCompletion;
        this.onComplete = onComplete;
        this.unsubscribe = unsubscribe;
        subscribe();
    }

    public void Check(){
        if (completed) return;
        
        if (checkCompletion()){
            completed = true;
            unsubscribe();
            onComplete?.Invoke();
        }
    }

    public void Unsubscribe() => 
    unsubscribe();
}