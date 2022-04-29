using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public List<GameObject> damageObjs;
    public GameObject repairObj;
    int damage = 0;
    public int maxHealth = 3;
    public Color colour;
    public Material repairIdle;
    public Material repairSelect;

    bool Repairing = false;
    public bool Friendly = false;

    // Start is called before the first frame update
    void Start()
    {
        /*foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = colour;
        }*/
    }

    void Damage()
    {
           
        damage += 1;
        damage = Mathf.Clamp(damage, 0, maxHealth);
        if (damage < maxHealth)
        {
            //Debug.Log("damage at "+ damage.ToString() + " switching");
            SwitchObj(damage);
        }
        else
        {
            if (Friendly)
            {
                damageObjs[2].SetActive(false); // If enemy ball hits friendly wall, disable its final wall piece (pseudo-destroyed)
                lastdI = 2;
            }
            else 
            {
                FindObjectOfType<ScoreSystem>().AddScore(15); // If friendly ball hits enemy wall, add sore and destroy wall completely.
                Destroy(gameObject);
            }
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    int lastdI = 0;

    void SwitchObj(int i)
    {
        if (i < maxHealth) damageObjs[lastdI].SetActive(!damageObjs[lastdI].activeInHierarchy); // Disable last damage state object
        float frac;
        int dI;
        if (i == 0)
        {
            frac = 0;
            dI = 0;
        }
        else
        {
            frac = (float)i / maxHealth;
            dI = Mathf.Clamp(Mathf.FloorToInt(frac * (damageObjs.Count - 1)) + 1, 0, maxHealth - 1);
        }
        //Debug.Log("frac "+frac.ToString());
        
        //Debug.Log("DI "+dI.ToString());
        //Debug.Log(damageObjs[dI]);
        damageObjs[dI].SetActive(!damageObjs[dI].activeInHierarchy);
        lastdI = dI;
    }

    public void Repair()
    {
        damage -= 1;
        if (damage == 0)
        {
            //Debug.Log("REPAIRED:" + damage.ToString() + "/" + maxHealth.ToString());
            RepairMode(false);
        }
        if (damage == maxHealth - 1)
        {
            damageObjs[2].SetActive(true);
            lastdI = 2;
        }
        else
        {
            SwitchObj(damage);
        }

    }

    public void RepairMode(bool b)
    {
        Repairing = b;
        repairObj.SetActive(b);
    }

    public void RepairSelect(bool b)
    {
        if (b)
            repairObj.GetComponent<Renderer>().material = repairSelect;
        else
            repairObj.GetComponent<Renderer>().material = repairIdle;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Repairing)
        {

        }
        else
        {
            if (collision.gameObject.tag == "projectile")
            {
                //Debug.Log("Wall damaged");
                Damage();
            }
        }
    }

}
