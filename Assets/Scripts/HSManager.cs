using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HSManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabScoreData;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var scoreData in SaveManager.Instance.scores.scoreDatas)
        {
            GameObject scoreDataRow = Instantiate(prefabScoreData, transform);
            scoreDataRow.transform.GetChild(0).gameObject.GetComponent<Text>().text = scoreData.playerName;
            scoreDataRow.transform.GetChild(1).gameObject.GetComponent<Text>().text = scoreData.score.ToString();
        }
    }

    public void BackHome()
    {
        SceneManager.LoadScene(0);
    }
}
