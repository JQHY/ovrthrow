using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class KettleRelease : MonoBehaviour
{

    public Rigidbody proj;
    bool thrown = false;
    public Vector3 throwvel;


    private void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.X) && !thrown)
        {
            thrown = true;
            Vector3 vel = proj.velocity;
            proj.transform.parent = null;
            proj.isKinematic = false;
            proj.AddForce(throwvel, ForceMode.VelocityChange);
        }
    }
}
