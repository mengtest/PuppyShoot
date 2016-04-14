using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour {

    public Text m_ScoreText;
    public int m_Score;

	void Awake()
    {
        this.AddObserver(Notifications.ADD_SCORE, OnAddScore);
    }

    void Start ()
    {
        m_Score = 0;
        m_ScoreText.text = "Score: " + m_Score.ToString(); 
    }

    void OnDestroy()
    {
        this.RemoveObserver(Notifications.ADD_SCORE);
    }

	void OnAddScore(object sender, EventArgs e)
    {
        InfoEventArgs<int> score = (InfoEventArgs<int>)e;
        m_Score += score.info;
        m_ScoreText.text = "Score: " + m_Score.ToString();
    }
}
