using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyModes : MonoBehaviour
{
    public Button EasyButton;
    public Button MediumButton;
    public Button HardButton;

    Text text;

    // This should run every time the main menu is re-opened?
    void Start()
    {
          PlayerPrefs.SetInt("Difficulty", 2);
          MediumButton.Select();
          text = GameObject.FindWithTag("MediumModeText").GetComponent<Text>();
          text.text = "• Medium •";
          // 1 = Easy, 2 = Medium, 3 = Hard

          EasyButton.onClick.AddListener(() => ButtonPress(1));
          MediumButton.onClick.AddListener(() => ButtonPress(2));
          HardButton.onClick.AddListener(() => ButtonPress(3));
    }

    void ButtonPress(int mode)
    {
        // check if that difficulty mode is already the playerpref
        // else change it
        if (mode != PlayerPrefs.GetInt("Difficulty"))
        {
          if (mode == 1)
          {
            PlayerPrefs.SetInt("Difficulty", 1);
            print("easy mode on");

            text = GameObject.FindWithTag("EasyModeText").GetComponent<Text>();
            text.text = "• Easy •";
            text = GameObject.FindWithTag("MediumModeText").GetComponent<Text>();
            text.text = "Medium";
            text = GameObject.FindWithTag("HardModeText").GetComponent<Text>();
            text.text = "Hard";
          }
          if (mode == 2)
          {
            PlayerPrefs.SetInt("Difficulty", 2);
            print("medium mode on");

            text = GameObject.FindWithTag("MediumModeText").GetComponent<Text>();
            text.text = "• Medium •";
            text = GameObject.FindWithTag("EasyModeText").GetComponent<Text>();
            text.text = "Easy";
            text = GameObject.FindWithTag("HardModeText").GetComponent<Text>();
            text.text = "Hard";
          }
          if (mode == 3)
          {
            PlayerPrefs.SetInt("Difficulty", 3);
            print("hard mode on");

            text = GameObject.FindWithTag("HardModeText").GetComponent<Text>();
            text.text = "• Hard •";
            text = GameObject.FindWithTag("EasyModeText").GetComponent<Text>();
            text.text = "Easy";
            text = GameObject.FindWithTag("MediumModeText").GetComponent<Text>();
            text.text = "Medium";
          }
        }
    }

}
