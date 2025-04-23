using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class EnemyStateManager : MonoBehaviour
{
    public static EnemyStateManager Instance { get; private set; }

    private Dictionary<string, EnemyData> enemyStates = new();

    private string SavePath => Path.Combine(Application.persistentDataPath, "enemy_state_save.json");


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadEnemyData();
    }


    public EnemyData GetEnemyData(string enemyId, float maxHealth)
    {
        if (!enemyStates.ContainsKey(enemyId))
        {
            enemyStates[enemyId] = new EnemyData(enemyId, maxHealth);
        }
        return enemyStates[enemyId];
    }


    public void SetEnemyState(string enemyId)
    {
        if (enemyStates.ContainsKey(enemyId))
        {
            enemyStates[enemyId].isDead = true;
        }
    }
    

    public void SaveEnemyData()
    {
        EnemySaveData saveData = new EnemySaveData();

        foreach (var state in enemyStates)
        {
            saveData.allEnemies.Add(state.Value);
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
    }


    public void LoadEnemyData()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            EnemySaveData saveData = JsonUtility.FromJson<EnemySaveData>(json);

            enemyStates.Clear();
            foreach (var enemy in saveData.allEnemies)
            {
                enemyStates[enemy.enemyId] = enemy;
            }

            Debug.Log("Loaded enemy data.");
        }
        else
        {
            Debug.Log("No enemy save file found.");
        }
    }


    void OnApplicationQuit()
    {
        Instance.SaveEnemyData();
    }
}
