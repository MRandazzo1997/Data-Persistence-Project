using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [SerializeField] private InputField ifPlayerName;

    public void OnStartPressed()
    {
        if (SaveManager.Instance.playerName != "")
            SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene(2);
    }

    public void GetPlayerName()
    {
        SaveManager.Instance.playerName = ifPlayerName.text;
    }

    public void OpenHighScores()
    {
        SceneManager.LoadScene(3);
    }
}
