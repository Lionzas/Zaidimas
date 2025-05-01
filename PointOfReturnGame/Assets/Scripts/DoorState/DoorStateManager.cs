using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[System.Serializable]
public class DoorSaveData
{
    public List<string> unlockedDoors;
    public List<string> lockedDoors;
}

public class DoorStateManager : MonoBehaviour
{
    public static DoorStateManager instance;
    private Dictionary<string, bool> doorStates = new Dictionary<string, bool>();

    private string SavePath => Application.persistentDataPath + "/doors.json";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadDoors();  // Load on start
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        SaveDoors();
    }

    public bool IsDoorUnlocked(string id)
    {
        return doorStates.ContainsKey(id) && doorStates[id];
    }

    public void SetDoorState(string id, bool isUnlocked)
    {
        if (doorStates.ContainsKey(id))
        {
            doorStates[id] = isUnlocked;
        }
        else
        {
            doorStates.Add(id, isUnlocked);
        }
    }

    public Dictionary<string, bool> GetDoorStates()
    {
        return doorStates;
    }

    public void LoadStates(Dictionary<string, bool> savedStates)
    {
        doorStates = savedStates;
    }

    public void SaveDoors()
    {
        var data = new DoorSaveData
        {
            unlockedDoors = doorStates.Where(kvp => kvp.Value).Select(kvp => kvp.Key).ToList(),
            lockedDoors = doorStates.Where(kvp => !kvp.Value).Select(kvp => kvp.Key).ToList(),
        };

        // Write out the save data to the file
        File.WriteAllText(SavePath, JsonUtility.ToJson(data));
    }

    public void LoadDoors()
    {
        if (!File.Exists(SavePath)) return;

        string json = File.ReadAllText(SavePath);
        var data = JsonUtility.FromJson<DoorSaveData>(json);

        // Load unlocked doors
        var loaded = new Dictionary<string, bool>();
        foreach (var id in data.unlockedDoors)
        {
            loaded[id] = true;
        }
        foreach (var id in data.lockedDoors)
        {
            loaded[id] = false;
        }

        LoadStates(loaded);
    }
}