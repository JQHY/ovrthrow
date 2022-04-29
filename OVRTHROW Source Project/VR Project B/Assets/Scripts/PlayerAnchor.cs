using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnchor : MonoBehaviour
{

    public Transform rootT;
    public Vector3 PosOffset;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 rootR = rootT.forward;
        rootR.y = 0;

        transform.forward = rootR;
        transform.position = rootT.position+PosOffset;
    }
}
