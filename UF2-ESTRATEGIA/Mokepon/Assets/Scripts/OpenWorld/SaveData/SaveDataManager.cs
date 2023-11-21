using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SaveData;

public class SaveDataManager : MonoBehaviour
{
    private const string saveFileName = "savegame.json";

    public void SaveData()
    {
        ISaveableObject[] saveableObjects= FindObjectsByType<JugadorController>(FindObjectsSortMode.None);
        SaveData data = new SaveData();
        data.PopulateData(saveableObjects);
        string jsonData =JsonUtility.ToJson(data);

        try
        {
            Debug.Log("Saving: ");
            Debug.Log(jsonData);
            File.WriteAllText(saveFileName, jsonData);

        }catch (Exception e)
        {
            Debug.LogError("Error");
        }
    }

    public void LoadData()
    {
        try
        {

            string jsonData = File.ReadAllText(saveFileName);
            SaveData data = new SaveData();
            JsonUtility.FromJsonOverwrite(jsonData, data);
            GetComponent<PlayerSpawner>().FromSaveData(data);

        }
        catch (Exception e)
        {
        Debug.LogError($"Error while trying to load {Path.Combine(Application.persistentDataPath, saveFileName)} with exception {e}");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            SaveData();

        if (Input.GetKeyDown(KeyCode.C))
            LoadData();
    }




}
