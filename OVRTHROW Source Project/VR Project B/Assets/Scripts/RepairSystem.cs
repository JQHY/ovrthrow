using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSystem : MonoBehaviour
{

    public Transform head;
    public Transform Player;
    public Transform castle;

    public GameObject brickPart;
    public int Points;
    ScoreSystem scoreSys;
    List<Wall> walls;

    List<Wall> rWalls;

    float playerHeight;
    bool HeightSaved = false;

    float SquatValue;
    public float DefaultHeight = 1.75f;
    public float SquatLevel; //= 0.4f; 

    Vector3 startP;

    private void Awake()
    {
        walls = new List<Wall>();
        rWalls = new List<Wall>();
        playerHeight = DefaultHeight;//head.position.y;
        scoreSys = transform.parent.GetComponentInChildren<ScoreSystem>();

        if (PlayerPrefs.HasKey("PlayerHeight"))
        {
            HeightSaved = true;
            playerHeight = PlayerPrefs.GetFloat("PlayerHeight");
        }
        //Debug.Log("playerHeight " + playerHeight.ToString());
        SquatValue = playerHeight * SquatLevel;//playerHeight - (playerHeight * SquatLevel); // TO standing height FROM squatting height
        startP = Player.position;
        //Debug.Log($"SquatVal: {SquatValue}, startp: {startP}");

    }

    public void PopulateWalls()
    {
        walls.AddRange(castle.GetComponentsInChildren<Wall>());
        Debug.Log("Wall Count: " + walls.Count.ToString());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    GameObject parts;
    Wall lockWall;

    public void EngageR(bool b)
    {
        EngagingR = b;
        if (!HeightSaved) { playerHeight = head.localPosition.y; }
        if (EngagingL)
        {
            lockWall = PickRepairTarget();
        }
    }

    public void EngageL(bool b)
    {
        EngagingL = b;
        if (!HeightSaved) { playerHeight = head.localPosition.y; }
        if (EngagingR)
        {
            lockWall = PickRepairTarget();
        }

    }



    bool EngagingR = false;
    bool EngagingL = false;
    bool Resetting = false;

    float headHeight = 0f;

    Wall PickRepairTarget() // Choose the damaged wall most directly in front of the player.
    {
        if (lockWall) lockWall.RepairSelect(false);
        float minAng = 360f;
        Wall pickW = rWalls[0];
        for (int i = 0; i < rWalls.Count; i++)
        {
            Wall rw = rWalls[i];
            if (rw.GetDamage() < 1)
            {
                continue;
            }
            float wAng = Vector3.Angle(head.forward, rw.transform.position);
            if (wAng < minAng)
            {
                minAng = wAng;
                pickW = rw;
            }
        }
        pickW.RepairSelect(true);
        if (parts) Destroy(parts);
        parts = Instantiate(brickPart, pickW.transform.position + Vector3.up * 10, Quaternion.identity);
        return pickW;
    }


    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (EngagingR && EngagingL)
        {
            headHeight = head.localPosition.y;//head.position.y - startP.y;//(startP.y+playerHeight)-head.position.y; // Diff TO standing height FROM squatting height
            //Debug.Log($"headHeight: {headHeight}");
            if (!Resetting)
            {
                if (headHeight < SquatValue) // If head is lowered a sufficient percentage of the height (indicating a squat)
                {
                    //Debug.Log("repair squat complete!");
                    scoreSys.AddScore(Points);
                    Resetting = true;
                    lockWall.Repair();
                    Destroy(parts);
                    
                }
                else
                {
                    if (parts)
                        parts.transform.position = (lockWall.transform.position) + (Vector3.up * (15 - 15 * ((playerHeight - headHeight) / SquatValue)));
                }
            }
            else
            {
                if (headHeight > playerHeight - 0.1f)
                {
                    //Debug.Log("Reset");
                    Resetting = false;
                }
            }
        }
    }

    public void SetRepair(bool b)
    {
        if (!b)
        {
            try
            {
                Destroy(parts.gameObject);
            }
            catch
            {

            }
        }
        rWalls = new List<Wall>();
        foreach (Wall w in walls)
        {
            if (b)
            {
                if (w.GetDamage() > 0)
                {
                    rWalls.Add(w); // Add to list of walls that are damaged (and can therefore be repaired)
                    w.RepairMode(true);
                }
            }
            else
            {
                w.RepairMode(false);
                if (w.GetDamage() > 0)
                {
                    scoreSys.AddScore(-5);
                }
            }
        }
        
    }
}
