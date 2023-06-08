using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    public Color selectedColor = Color.black;
    public Scores scores;

    public string playerName;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        selectedColor = LoadData<ColorData>().color;
        scores = LoadData<Scores>();
        scores.Sort();

        if (scores.scoreDatas.Length <= 0)
            Debug.Log("No scores available");
    }

    public void SaveData(object obj)
    {
        string savePath = Application.persistentDataPath + "/" + obj.GetType().Name + ".json";
        string json = JsonUtility.ToJson(obj);
        File.WriteAllText(savePath, json);
    }

    public T LoadData<T>() where T : new()
    {
        T data = new T();
        string loadingPath = Application.persistentDataPath + "/" + data.GetType().Name + ".json";
        if (File.Exists(loadingPath))
        {
            string json = File.ReadAllText(loadingPath);
            data = JsonUtility.FromJson<T>(json);
        }
        return data;
    } 
    
    public void Reload(string playerName = "")
    {
        selectedColor = LoadData<ColorData>().color;
        scores = LoadData<Scores>();
        scores.Sort();
        this.playerName = playerName;
    }
}

[System.Serializable]
public class ColorData
{
    public Color color;
}

[System.Serializable]
public class HighScoreData
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class Scores
{
    public HighScoreData[] scoreDatas;

    public void Sort()
    {
        if (scoreDatas != null && scoreDatas.Length > 1)
            scoreDatas = scoreDatas.ToList().OrderByDescending(s => s.score).ToArray();
        else
        {
            scoreDatas = new HighScoreData[0];
        }
    }
}
