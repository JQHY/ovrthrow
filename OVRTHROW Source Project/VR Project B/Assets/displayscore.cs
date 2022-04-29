using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayscore : MonoBehaviour
{
    public Text highscore;

    void Start()
    {
        highscore.text = string.Format("HIGH SCORE: {0}", PlayerPrefs.GetInt("highscore"));
    }
}
