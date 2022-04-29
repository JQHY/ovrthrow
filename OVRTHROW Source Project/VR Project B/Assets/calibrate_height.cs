using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class calibrate_height : MonoBehaviour
{
    public Button calibrate;

    public Transform cam;
    // Start is called before the first frame update

    void Callback()
    {
       PlayerPrefs.SetFloat("PlayerHeight", cam.localPosition.y);
       //Debug.Log(cam.localPosition.y);
    }

    void Start()
    {
      calibrate.onClick.AddListener(Callback);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
