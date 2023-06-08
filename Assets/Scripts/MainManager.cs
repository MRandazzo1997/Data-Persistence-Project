using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text txtSavedData;
    public Text currentPlayerName;
    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private HighScoreData highScoreData;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        Ball.gameObject.GetComponent<MeshRenderer>().material.color = SaveManager.Instance.selectedColor;

        if (SaveManager.Instance.scores.scoreDatas.Length > 0)
            highScoreData = SaveManager.Instance.scores.scoreDatas[0];
        else
            highScoreData = new HighScoreData() { score = 0, playerName = "" };

        txtSavedData.text = "Best Score: " + highScoreData.score + " Name: " + highScoreData.playerName;
        currentPlayerName.text = "Name: " + SaveManager.Instance.playerName;
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SaveManager.Instance.Reload(highScoreData.playerName);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        if (m_Points > highScoreData.score)
            txtSavedData.text = "Best Score: " + m_Points + " Name: " + highScoreData.playerName;
    }

    public void GameOver()
    {
        if (m_Points > highScoreData.score)
        {
            List<HighScoreData> scoreDatas = SaveManager.Instance.scores.scoreDatas.ToList();
            scoreDatas.Add(new HighScoreData()
            {
                score = m_Points,
                playerName = SaveManager.Instance.playerName
            });
            SaveManager.Instance.scores.scoreDatas = scoreDatas.ToArray();
            SaveManager.Instance.SaveData(SaveManager.Instance.scores);
        }

        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
