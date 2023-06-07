using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    public InputField ifPlayerName;
    public Color selectedColor;

    public string playerName;
    public string highScorePlayerName;
    public string pathScores;
    public int highScore;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
        pathScores = Application.persistentDataPath + "/highScores.json";
    }

    public void OnStartPressed()
    {
        if (playerName != "")
            SceneManager.LoadScene(1);
    }

    public void GetPlayerName()
    {
        playerName = ifPlayerName.text;
    }

    public void SaveData()
    {
        HighScoreData highScoreData = new HighScoreData();
        highScoreData.playerName = playerName;
        highScoreData.score = highScore;

        string json = JsonUtility.ToJson(highScoreData);
        File.WriteAllText(pathScores, json);
    }

    public void SaveData(object obj)
    {
        string savePath = Application.persistentDataPath + "/" + obj.GetType().Name + ".json";
        string json = JsonUtility.ToJson(obj);
        File.WriteAllText(savePath, json);
    }

    public void LoadData()
    {
        if (File.Exists(pathScores))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/highScores.json");
            HighScoreData highScoreData = JsonUtility.FromJson<HighScoreData>(json);

            highScore = highScoreData.score;
            highScorePlayerName = highScoreData.playerName;
        }
    }

    public void LoadData(object obj)
    {
        string loadingPath = Application.persistentDataPath + "/" + obj.GetType().Name + ".json";
        if (File.Exists(loadingPath))
        {
            string json = File.ReadAllText(loadingPath);
            var data = JsonUtility.FromJson<object>(json);
        }
    }

    [System.Serializable]
    public class HighScoreData
    {
        public string playerName;
        public int score;
    }

    
}

[System.Serializable]
public class ColorData
{
    public Color color;
}
