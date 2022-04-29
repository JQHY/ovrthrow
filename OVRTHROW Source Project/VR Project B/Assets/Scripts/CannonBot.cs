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
    Vector3 aimCentre;

    // Start is called before the first frame update
    void Start()
    {
        aimCentre = GetAimCentre();
        targetPos = RandomTargetPos();
        shootForce = Vector3.forward* Mathf.Sqrt((10f*(transform.position-aimCentre).magnitude)/(2*(Mathf.Cos(Mathf.PI/4)*Mathf.Sin(Mathf.PI/4))));
    }

    Vector3 GetAimCentre()
    {
        Vector3 c = aimVolume[0] + 0.5f * aimVolume[1];
        return c;
    }

    public void RecalculateForce()
    {
        shootForce = Vector3.forward * Mathf.Sqrt((10f * transform.position.magnitude) / (2 * (Mathf.Cos(Mathf.PI / 4) * Mathf.Sin(Mathf.PI / 4))));
    }

    Vector3 targetDiff;
    Vector3 posDiff;
    Quaternion targetRot;

    bool CannonActive = true;

    // Update is called once per frame
    void Update()
    {
        if (CannonActive)
        {
            targetT.position = Vector3.MoveTowards(targetT.position, targetPos, .5f);
            targetDiff = targetT.position - ShootPoint.position;
            posDiff = targetPos - ShootPoint.position; // vector3 between desired aiming point and barrel of cannon
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
                    Cannon.localRotation = Quaternion.Euler(0, 0, Vector3.SignedAngle(transform.forward, CanScale, transform.right));
                    Cannon.Rotate(0, 0, 90);
                    //Debug.Log(Vector3.SignedAngle(Vector3.forward, CanScale, Vector3.right));
                }
                else
                {
                    Debug.Log("Reached");
                    Aiming = false;
                    Shoot();
                    StopAllCoroutines();
                    StartCoroutine(Cooldown());

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) Shoot();
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        StartCoroutine(ShootTimeout());
        targetPos = RandomTargetPos();
        Aiming = true;
    }

    IEnumerator ShootTimeout()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("timeout");
        Aiming = false;
        if (CannonActive)
        {
            Shoot();
            StartCoroutine(Cooldown());
        }
    }

    Vector3 RandomTargetPos()
    {
        Vector3 newPos = new Vector3(Random.Range(aimVolume[0].x, aimVolume[1].x), Random.Range(aimVolume[0].y, aimVolume[1].y)+transform.position.magnitude, Random.Range(aimVolume[0].z, aimVolume[1].z));
        if (Vector3.Distance(targetPos, newPos) < 0.05f)
        { // If new aiming position is too close to previous aiming position
            Debug.Log("recurse");
            return RandomTargetPos();
        }
        else
        {
            return newPos;
        }
    }

    public void SetCannonActive(bool b)
    {
        if (b)
        {
            CannonActive = true;
            Aiming = true;
            targetPos = RandomTargetPos();
            StartCoroutine(ShootTimeout());
        }
        else
        {
            CannonActive = false;
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

    public void SetAiming(bool v)
    {
        Aiming = v;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetT.position, 0.3f);
        Gizmos.DrawLine(ShootPoint.position, ShootPoint.position + targetDiff);
        Gizmos.color = Color.green;
        Vector3 centr = aimVolume[0] + (aimVolume[1] - aimVolume[0]) * 0.5f;
        Gizmos.DrawWireCube(centr,(aimVolume[1]-aimVolume[0]));
    }
}
