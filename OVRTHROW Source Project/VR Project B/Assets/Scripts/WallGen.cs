using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGen : MonoBehaviour
{

    public int length;
    public Vector3 off;
    public List<GameObject> GenObjs;
    public Transform genRoot;
    Wall tWall;

    // Start is called before the first frame update
    void Start()
    {
        tWall = GetComponent<Wall>();
        off = transform.TransformDirection(off);

        for (int i = 1
            ; i < length; i++)
        {
            GameObject w = Instantiate(GenObjs[Random.Range(0,GenObjs.Count)], transform.position + off*i, transform.rotation);
            if (tWall.Friendly) w.GetComponent<Wall>().Friendly = true;


            try
            {
                Destroy(w.GetComponent<WallGen>());
                w.transform.parent = genRoot;
            }
            catch { }
        }
    }

}
