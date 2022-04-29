using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldScript : MonoBehaviour
{
    public GameObject STT;
    private newNewSTT _theSpeechToText;
    private Renderer _renderer;
    private string finalTranscript;


    private string[] keywordStrings;
    private string theActiveKeyword;

    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        _theSpeechToText = STT.GetComponent<newNewSTT>();
        _renderer = this.GetComponent<Renderer>();


        // keywordStrings = new string[3] { "blue", "yellow", "red" };
        keywordStrings = new string[1] { "raise the shields" };
        _theSpeechToText.SetKeywords(keywordStrings);
    }

    IEnumerator ShieldTrigger()
    {
        yield return new WaitForSeconds(cooldown);
        raiseShields = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckColour();
    }

    

    public bool raiseShields;
    int keynum = -1;
    public void CheckColour()
    {
        theActiveKeyword = _theSpeechToText.GetCurrentKeyword();
        

        if (!raiseShields)
        {
            if (string.Equals(theActiveKeyword, keywordStrings[0]))
            {
                string ts = _theSpeechToText.GetFinalTranscript();
                int keyCount = (ts.Length - ts.Replace(keywordStrings[0], "").Length) / keywordStrings[0].Length;
                if (keyCount > keynum)
                {
                    raiseShields = true;
                    //_renderer.material.SetColor("_Color", Color.blue);
                    StartCoroutine(ShieldTrigger());
                }
                keynum = keyCount;
            }
            else
            {
                raiseShields = false;
            }
        }

        // if (string.Equals(theActiveKeyword, keywordStrings[1]))
        // {
        //     _renderer.material.SetColor("_Color", Color.yellow);
        // }

        // if (string.Equals(theActiveKeyword, keywordStrings[2]))
        // {
        //     _renderer.material.SetColor("_Color", Color.red);
        // }
    }

    //public void CheckColour()
    //{
    //    finalTranscript = _theSpeechToText.GetFinalTranscript();

    //    if (string.Equals(finalTranscript, "blue "))
    //    {
    //        _renderer.material.SetColor("_Color", Color.blue);
    //    }

    //    if (string.Equals(finalTranscript, "yellow "))
    //    {
    //        _renderer.material.SetColor("_Color", Color.yellow);
    //    }

    //    if (string.Equals(finalTranscript, "red "))
    //    {
    //        _renderer.material.SetColor("_Color", Color.red);
    //    }

    //}
}