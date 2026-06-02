using UnityEngine;

public class Player : Entity
{
    public string playerName;
    public int experience = 0;
    public int gold = 0;
    public int currentLevel = 1;

    protected override void Start(){
        base.Start();

        if (PlayerPrefs.HasKey("SavedScene"))
        {
            PlayerData playerData =
                DatabaseManager.Instance.LoadPlayer();

            GameStateData gameState =
                DatabaseManager.Instance.LoadGameState();

            if (playerData != null)
            {
                LoadData(playerData);
            }

            if (gameState != null)
            {
                transform.position =
                    new Vector3(
                        gameState.PlayerPosX,
                        gameState.PlayerPosY,
                        0f
                    );
            }

            PlayerPrefs.DeleteKey("SavedScene");
        }
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

    public void LoadData(PlayerData data)
    {
        playerName = data.Name;

        SetCurrentHealth(data.Health);

        experience = data.Experience;

        gold = data.Gold;

        currentLevel = data.CurrentLevel;
    }
}