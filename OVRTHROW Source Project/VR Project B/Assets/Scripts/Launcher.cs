using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{

    public GameObject projectile;
    public Transform shootRoot;
    public Vector3 shootVel;
    public bool CanSteer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Fire();

        if (CanSteer)
        {
            transform.Rotate(0,Input.GetAxis("Horizontal")*0.01f,0);
            transform.Rotate(0, 0,Input.GetAxis("Vertical") * 0.01f);
        }
    }

    public void Fire()
    {
        GameObject p = Instantiate(projectile, shootRoot.position, shootRoot.rotation);
        Rigidbody r = p.GetComponent<Rigidbody>();
        r.isKinematic = false;
        r.AddForce(shootRoot.TransformDirection(shootVel),ForceMode.Impulse);
    }

    
}
