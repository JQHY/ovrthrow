using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obj;
    public float SpawnCool= 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) SpawnObject();
    }

    void SpawnObject()
    {
        Instantiate(obj,transform.position,Quaternion.identity);
    }

    bool CanSpawn = true;

    IEnumerator DelaySpawn()
    {
        CanSpawn = false;
        yield return new WaitForSeconds(SpawnCool);
        SpawnObject();
        yield return new WaitForFixedUpdate();
        CanSpawn = true;
    }

    public void EnableSpawner()
    {
        CanSpawn = true;
        SpawnObject();
    }

    public void DisableSpawner()
    {
        StopAllCoroutines();
        CanSpawn = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Projectile>() && CanSpawn)
        {
            StartCoroutine(DelaySpawn());
        }
    }

}
