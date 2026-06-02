using System;

public static class GameEvents{
    public static event Action<string> OnEnemyKilled;
    public static event Action<string> OnItemGathered;
    public static event Action<string> OnNPCTalkedTo;

    public static void EnemyKilled(string enemyType) => OnEnemyKilled?.Invoke(enemyType);
    public static void ItemGathered(string itemType) => OnItemGathered?.Invoke(itemType);
    public static void NPCTalkedTo(string npcId) => OnNPCTalkedTo?.Invoke(npcId);
}