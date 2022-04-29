using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEntity : MonoBehaviour
{

    TowerHealth tHealth;
    public float MaxHealth;
    public float currHealth;
    public float dmg;
    public List<GameObject> DamageObjs;

    // Start is called before the first frame update
    void Start()
    {
        tHealth = GetComponentInParent<TowerHealth>();
        currHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "projectile") // Collision has been triggered by a valid projectile object
        {
            tHealth.Damage(dmg);
            currHealth -= dmg;
            CheckDamage();
        }
    }

    void CheckDamage()
    {
        float step = MaxHealth / DamageObjs.Count;
        int index = Mathf.FloorToInt(currHealth / step);
    }
}
