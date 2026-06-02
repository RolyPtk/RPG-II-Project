using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    private string dbPath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        dbPath = "URI=file:" + Application.persistentDataPath + "/SaveGame.db";

        CreateTables();

        Debug.Log("Database: " + Application.persistentDataPath);
    }

    private void CreateTables()
    {
        using (IDbConnection dbConnection = new SqliteConnection(dbPath))
        {
            dbConnection.Open();

            using (IDbCommand cmd = dbConnection.CreateCommand())
            {
                cmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS Player
                (
                    Id INTEGER PRIMARY KEY,
                    Name TEXT,
                    Health INTEGER,
                    Experience INTEGER,
                    Gold INTEGER,
                    CurrentLevel INTEGER
                );";

                cmd.ExecuteNonQuery();

                cmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS GameState
                (
                    Id INTEGER PRIMARY KEY,
                    PlayerPosX REAL,
                    PlayerPosY REAL
                );";

                cmd.ExecuteNonQuery();
            }

            dbConnection.Close();
        }
    }

    public void SavePlayer(PlayerData player)
    {
        using (IDbConnection dbConnection = new SqliteConnection(dbPath))
        {
            dbConnection.Open();

            using (IDbCommand cmd = dbConnection.CreateCommand())
            {
                cmd.CommandText =
                @"INSERT OR REPLACE INTO Player
                (
                    Id,
                    Name,
                    Health,
                    Experience,
                    Gold,
                    CurrentLevel
                )
                VALUES
                (
                    1,
                    @Name,
                    @Health,
                    @Experience,
                    @Gold,
                    @CurrentLevel
                );";

                AddParameter(cmd, "@Name", player.Name);
                AddParameter(cmd, "@Health", player.Health);
                AddParameter(cmd, "@Experience", player.Experience);
                AddParameter(cmd, "@Gold", player.Gold);
                AddParameter(cmd, "@CurrentLevel", player.CurrentLevel);

                cmd.ExecuteNonQuery();
            }

            dbConnection.Close();
        }

        Debug.Log("Player saved!");
    }

    public void SaveGameState(GameStateData gameState)
    {
        using (IDbConnection dbConnection = new SqliteConnection(dbPath))
        {
            dbConnection.Open();

            using (IDbCommand cmd = dbConnection.CreateCommand())
            {
                cmd.CommandText =
                @"INSERT OR REPLACE INTO GameState
                (
                    Id,
                    PlayerPosX,
                    PlayerPosY
                )
                VALUES
                (
                    1,
                    @PlayerPosX,
                    @PlayerPosY
                );";

                AddParameter(cmd, "@PlayerPosX", gameState.PlayerPosX);
                AddParameter(cmd, "@PlayerPosY", gameState.PlayerPosY);

                cmd.ExecuteNonQuery();
            }

            dbConnection.Close();
        }

        Debug.Log("Game state saved!");
    }

    private void AddParameter(IDbCommand cmd, string name, object value)
    {
        IDataParameter param = cmd.CreateParameter();

        param.ParameterName = name;
        param.Value = value;

        cmd.Parameters.Add(param);
    }
}