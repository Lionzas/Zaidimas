using System.IO;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    [SerializeField] TimeMachine timeMachine;
    public int portalState;
    public bool pipes;
    public bool fuel;

    public void SaveData()
    {
        MachineData machineData = new MachineData();
        machineData.portalState = timeMachine.portalState;
        machineData.pipes = timeMachine.pipes;
        machineData.fuel = timeMachine.fuel;

        string json = JsonUtility.ToJson(machineData);
        string path = Application.persistentDataPath + "/machineData.json";
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/machineData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MachineData loadedData = JsonUtility.FromJson<MachineData>(json);
            timeMachine.portalState = loadedData.portalState;
            timeMachine.pipes = loadedData.pipes;
            timeMachine.fuel = loadedData.fuel;
        }
        else
        {
            Debug.LogWarning("machineData.json not found");
        }
    }
}
