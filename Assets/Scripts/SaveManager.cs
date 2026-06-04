using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public GameState gameState = new GameState();

    private string path;

    private void Awake()
    {
        path = Application.persistentDataPath + "/save.json";
    }
    public void Save()
    {
        SaveData data = new SaveData();

        data.flags = new List<string>(gameState.activeFlags);

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);

        Debug.Log("Game Saved!");
    }
    public void Load()
    {
        if (!File.Exists(path))
        {
            Debug.Log("No save file");
            return;
        }

        string json = File.ReadAllText(path);

        SaveData data = JsonUtility.FromJson<SaveData>(json);

        gameState.activeFlags = new HashSet<string>(data.flags);

        Debug.Log("Game Loaded!");
    }
    public void DeleteSaveData()
    {
        File.Delete(path);
    }
}