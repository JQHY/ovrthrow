using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolSliders : MonoBehaviour
{
    public Toggle vol1;
    public Toggle vol2;
    public Toggle vol3;
    public Toggle vol4;
    public Toggle vol5;

    public Button vol1Collider;
    public Button vol2Collider;
    public Button vol3Collider;
    public Button vol4Collider;
    public Button vol5Collider;
    // Start is called before the first frame update

    void vol1_active()
    {
      PlayerPrefs.SetInt("MusicVolume", 1);
      vol1.GetComponent<Toggle>().isOn = true;
      vol2.GetComponent<Toggle>().isOn = false;
      vol3.GetComponent<Toggle>().isOn = false;
      vol4.GetComponent<Toggle>().isOn = false;
      vol5.GetComponent<Toggle>().isOn = false;
    }

    void vol2_active()
    {
      PlayerPrefs.SetInt("MusicVolume", 2);
      vol1.GetComponent<Toggle>().isOn = true;
      vol2.GetComponent<Toggle>().isOn = true;
      vol3.GetComponent<Toggle>().isOn = false;
      vol4.GetComponent<Toggle>().isOn = false;
      vol5.GetComponent<Toggle>().isOn = false;
    }

    void vol3_active()
    {
      PlayerPrefs.SetInt("MusicVolume", 3);
      vol1.GetComponent<Toggle>().isOn = true;
      vol2.GetComponent<Toggle>().isOn = true;
      vol3.GetComponent<Toggle>().isOn = true;
      vol4.GetComponent<Toggle>().isOn = false;
      vol5.GetComponent<Toggle>().isOn = false;
    }

    void vol4_active()
    {
      PlayerPrefs.SetInt("MusicVolume", 4);
      vol1.GetComponent<Toggle>().isOn = true;
      vol2.GetComponent<Toggle>().isOn = true;
      vol3.GetComponent<Toggle>().isOn = true;
      vol4.GetComponent<Toggle>().isOn = true;
      vol5.GetComponent<Toggle>().isOn = false;
    }

    void vol5_active()
    {
      PlayerPrefs.SetInt("MusicVolume", 5);
      vol1.GetComponent<Toggle>().isOn = true;
      vol2.GetComponent<Toggle>().isOn = true;
      vol3.GetComponent<Toggle>().isOn = true;
      vol4.GetComponent<Toggle>().isOn = true;
      vol5.GetComponent<Toggle>().isOn = true;
    }

    void Start()
    {
      PlayerPrefs.SetInt("MusicVolume", 3);
      vol3_active();

      vol1Collider.onClick.AddListener(vol1_active);
      vol2Collider.onClick.AddListener(vol2_active);
      vol3Collider.onClick.AddListener(vol3_active);
      vol4Collider.onClick.AddListener(vol4_active);
      vol5Collider.onClick.AddListener(vol5_active);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
