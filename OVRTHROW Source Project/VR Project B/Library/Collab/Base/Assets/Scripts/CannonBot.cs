using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBot : MonoBehaviour
{

    public Transform Base;
    public Transform Cannon;
    public Transform ShootPoint;
    
    public GameObject projectile;
    public GameObject ShootEffect;

    public Vector3 targetPos;
    public Transform targetT;
    public float turnSpeed;
    public float angleSpeed;
    public float cooldown;
    public Vector3 shootForce;
    public List<Vector3> aimVolume; // Define the front-lower-left and top-upper-right corners of the aim volume
    bool Aiming = true;
    

    // Start is called before the first frame update
    void Start()
    {
        targetPos = RandomTargetPos();
    }

    Vector3 targetDiff;
    Vector3 posDiff;
    Quaternion targetRot;

    // Update is called once per frame
    void Update()
    {
        targetT.position = Vector3.MoveTowards(targetT.position, targetPos, .1f);
        targetDiff = targetT.position - ShootPoint.position;
        posDiff = targetPos - ShootPoint.position;
        //targetDiff = targetDiff.normalized;
        targetRot = Quaternion.LookRotation(targetDiff);
        if (Aiming)
        {
            if (Vector3.Angle(ShootPoint.forward, posDiff) > 0.4f)//(ShootPoint.forward != targetDiff)////
            {
                Vector3 BaseScale = targetDiff;//targetDiff.Scale(new Vector3(1, 0, 1));
                BaseScale.y = 0;
                Vector3 CanScale = targetDiff;
                CanScale.x = 0;
                Base.right = Vector3.RotateTowards(Base.right, BaseScale, .1f, turnSpeed);
                Cannon.localRotation = Quaternion.Euler(0, 0, Vector3.SignedAngle(Vector3.forward, CanScale, Vector3.right));
            }
            else
            {
                Debug.Log("Reached");
                Aiming = false;
                Shoot();
                StartCoroutine(Cooldown());

            }
        }
        if (Input.GetKeyDown(KeyCode.F)) Shoot();
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        targetPos = RandomTargetPos();
        Aiming = true;
    }

    Vector3 RandomTargetPos()
    {
        Vector3 newPos = new Vector3(Random.Range(aimVolume[0].x, aimVolume[1].x), Random.Range(aimVolume[0].y, aimVolume[1].y), Random.Range(aimVolume[0].z, aimVolume[1].z));
        if (Vector3.Distance(targetPos, newPos) < 0.5f)
        { // If new aiming position is too close to previous aiming position
            Debug.Log("recurse");
            return RandomTargetPos();
        }
        else
        {
            return newPos;
        }
    }

    void Shoot()
    {
        GameObject p = Instantiate(projectile, ShootPoint.position, ShootPoint.rotation);
        Rigidbody r = p.GetComponent<Rigidbody>();
        r.isKinematic = false;
        r.AddForce(ShootPoint.TransformDirection(shootForce), ForceMode.Impulse);
        Instantiate(ShootEffect, ShootPoint.position, ShootPoint.rotation);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetT.position, 0.3f);
        Gizmos.DrawLine(ShootPoint.position, ShootPoint.position + targetDiff);
        Gizmos.color = Color.green;
        Vector3 centr = aimVolume[0] + (aimVolume[1] - aimVolume[0]) * 0.5f;
        Gizmos.DrawWireCube(centr,(aimVolume[1]-aimVolume[0]));
    }
}
