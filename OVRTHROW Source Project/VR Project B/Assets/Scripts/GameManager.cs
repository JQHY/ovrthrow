using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int Difficulty;
    public float AttackTime = 60f;
    public float RepairTime = 30f;
    public float RestTime = 10f;
    public int Rounds;
    public float EnemyDist = 50f;

    public GameObject enemyPrefab;

    

    public List<CannonBot> bots;
    public RepairSystem RepairSys;
    Text RoundText;
    Text CountText;

    ScoreSystem ScoreSys;

    int currentRound = 1;
    public void StartGame()
    {
        SpawnEnemies();
        StartCoroutine(RoundTimers());
        currentRound = 1;
    }

    IEnumerator RoundTimers()
    {
        yield return new WaitForFixedUpdate();
        RepairSys.PopulateWalls();
        while (currentRound < Rounds)
        {
            //ATTACK PHASE
            yield return new WaitForFixedUpdate();
            ActivateAttackPhase();
            StartCoroutine(CountDown(AttackTime));
            yield return new WaitForSeconds(AttackTime);
            Debug.Log("Attack Phase ended, beginning repair phase");
            //REPAIR PHASE
            yield return new WaitForFixedUpdate();
            DisableAttackPhase();
            ActivateRepairPhase();
            yield return new WaitForFixedUpdate();
            StartCoroutine(CountDown(RepairTime));
            yield return new WaitForSeconds(RepairTime);
            Debug.Log("Repair Phase Ended, beginning rest phase");
            //REST PHASE
            yield return new WaitForFixedUpdate();
            DisableRepairPhase();
            RoundText.text = "REST!";
            StartCoroutine(CountDown(RestTime));
            yield return new WaitForSeconds(RestTime);
            currentRound++;
        }
        EnableEndPhase();
    }

    void CalculateHighscore()
    {
        bool newHigh = true;
        if (PlayerPrefs.HasKey("highscore"))
        {
            int high = PlayerPrefs.GetInt("highscore");
            if (ScoreSys.GetScore() > high)
            {
                PlayerPrefs.SetInt("highscore", ScoreSys.GetScore());
            }
            else
            {
                newHigh = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("highscore", ScoreSys.GetScore());
        }

        if (newHigh)
        {
            CountText.text = "New High Score!";
        }
    }

    IEnumerator CountDown(float count)
    {
        float dt = Time.time;
        //Debug.Log("DT: "+ dt.ToString());
        while (Time.time - dt < count)
        {
            CountText.text = string.Format("{0}", Mathf.Ceil(count -(Time.time - dt)));
            yield return new WaitForEndOfFrame();
        }
        CountText.text = "-";
        Debug.Log("Timer End");
    }

    void ActivateAttackPhase()
    {
        FindObjectOfType<Spawner>().EnableSpawner();
        RoundText.text = "ATTACK!";
        foreach (CannonBot c in bots)
        {
            c.SetCannonActive(true);
        }
    }
    void DisableAttackPhase()
    {
        FindObjectOfType<Spawner>().DisableSpawner();
        foreach (Projectile p in FindObjectsOfType<Projectile>())
        {
            Destroy(p.gameObject);
        }
        foreach(CannonBot c in bots)
        {
            c.SetCannonActive(false);
        }
    }

    void ActivateRepairPhase()
    {
        RoundText.text = "REPAIR!";
        RepairSys.SetRepair(true);
    }

    void DisableRepairPhase()
    {
        RepairSys.SetRepair(false);
    }

    void EnableEndPhase()
    {
        RoundText.text = "GAME COMPLETE";
        CalculateHighscore();
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(GameObject.Find("Player").gameObject);
        SceneManager.LoadScene("Start Menu");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        RepairSys = GetComponentInChildren<RepairSystem>();
        ScoreSys = GetComponentInChildren<ScoreSystem>();
        RoundText = GetComponentsInChildren<Text>()[0];
        CountText = GetComponentsInChildren<Text>()[1];
        StartGame(); // Test Start Game
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        int enemyCount = Mathf.Clamp(Mathf.FloorToInt(Difficulty*0.05f + 1),0,2); // Calculate # of enemies for Difficulty level;
        float radInt = ((2f / 3f) * Mathf.PI) / enemyCount;//(Mathf.PI * 2) / enemyCount; // Distributes enemies over a 120 degree arc in front of player
        float offset = ((4 / 3f) * Mathf.PI)*Mathf.Clamp((enemyCount-1),0,1);

        for (int i = 0; i <enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            
            Debug.Log(Mathf.Sin(radInt * i).ToString()+" "+ Mathf.Cos(radInt * i).ToString());
            Vector3 newPos = new Vector3((Mathf.Sin(radInt * i - offset) * EnemyDist), 0, (Mathf.Cos(radInt * i - offset) * EnemyDist));
            enemy.transform.position = newPos;
            enemy.transform.rotation = Quaternion.LookRotation(newPos,Vector3.up);//Quaternion.Euler(0, Mathf.Rad2Deg*radInt*-i, 0);
            enemy.transform.position += enemy.transform.forward * Random.Range(-2, 2);

            CannonBot cBot = enemy.GetComponentInChildren<CannonBot>(); // Set Cannon properties based on difficulty
            cBot.aimVolume[0] += enemy.transform.forward * 9;
            cBot.aimVolume[1] += enemy.transform.forward * 9;
            cBot.RecalculateForce();
            bots.Add(cBot);

        }
    }
}
