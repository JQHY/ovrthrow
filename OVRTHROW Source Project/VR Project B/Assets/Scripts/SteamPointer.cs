using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SteamPointer : SteamVR_LaserPointer
{

    EventSystem ESys;

    public UnityEvent UEvs;

    // Start is called before the first frame update
    void Start()
    {
        ESys = GetComponent<EventSystem>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
