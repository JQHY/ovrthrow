using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject explosion;
    public bool Scoring = false;
    public int Points = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 7)
        {
            GameObject e = Instantiate(explosion, collision.contacts[0].point, Quaternion.identity);
            if (Scoring & collision.gameObject.layer == 6)
            {
                if (!collision.gameObject.GetComponent<Wall>().Friendly) FindObjectOfType<ScoreSystem>().AddScore(Points);
            }
            if (transform.childCount > 0)
                GetComponentInChildren<KillParts>().RunKill();
            
            Destroy(gameObject);
        }
    }

}
