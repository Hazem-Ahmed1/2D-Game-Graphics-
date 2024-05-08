using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    private int score = 0;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }


    private void Update()
    {
        textMeshProUGUI.text = score.ToString();
    }
    public void IncrementScore(int amount)
    {
        score += amount;
    }
}

