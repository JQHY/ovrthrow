using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEntity : MonoBehaviour
{

    public shieldScript sCr;
    public GameObject shieldObj;
    public GameObject ReadyText;
    public float Cooldown;

    bool canTrigger;

    // Start is called before the first frame update
    void Start()
    {
        canTrigger = false;
        ReadyText.SetActive(false);
        StartCoroutine(RandomStart());
    }

    IEnumerator RandomStart()
    {
        yield return new WaitForSeconds(Random.Range(1, 4));
        canTrigger = true;
        ReadyText.SetActive(true);
    }


    IEnumerator Cool()
    {
        Debug.Log("power off, cooling...");
        yield return new WaitForSeconds(Cooldown);
        canTrigger = true;
        ReadyText.SetActive(true);
    }


    bool lastV = false;
    // Update is called once per frame
    void Update()
    {
        if (sCr.raiseShields != lastV)
        {
            if (sCr.raiseShields)
            {
                if (canTrigger)
                {
                    shieldObj.SetActive(true);
                    canTrigger = false;
                    ReadyText.SetActive(false);
                }
            }
            else
            {
                shieldObj.SetActive(false);
                StartCoroutine(Cool());
            }
        }
        

        lastV = sCr.raiseShields;
    }
}
