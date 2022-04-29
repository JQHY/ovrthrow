using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{

    int score = 0;
    public Text scoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddScore(int s)
    {
        score += s;
        UpdateText();
    }

    public int GetScore()
    {
        return score;
    }


    void UpdateText()
    {
        scoreTxt.text = score.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
