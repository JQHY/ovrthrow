using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillParts : MonoBehaviour
{
    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(3.0f);
        Debug.Log("Destroying");
        Destroy(gameObject);
    }

    public void RunKill()
    {
        StartCoroutine(WaitDestroy());
    }
}
