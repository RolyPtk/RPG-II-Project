using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Quest
{
    public string id;
    public bool isCompleted => requirements.All(r => r.IsComplete);
    public List<QuestRequirement> requirements = new List<QuestRequirement>();

    public Quest(string id){
        this.id = id;
    }

    public void AddRequirement(QuestRequirement requirement){
        requirements.Add(requirement);
    }

    public void CheckRequirements(){
        foreach (var req in requirements)
        {
            req.Check();
        }
    }
}