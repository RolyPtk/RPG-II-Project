public class Player : Entity
{
    public string playerName;
    public int experience = 0;
    public int gold = 0;
    public int currentLevel = 1;

    protected override void Start(){
        base.Start();
    }

    protected override void Die(){
        //Debug.Log("Player died!");
        base.Die();
    }

    public PlayerData ToData()
    {
        return new PlayerData
        {
            Name = playerName,
            Health = currentHealth, // sau cum se numește în Entity
            Experience = experience,
            Gold = gold,
            CurrentLevel = currentLevel
        };
    }
}