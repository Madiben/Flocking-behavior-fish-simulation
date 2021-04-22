// ************************************************ Flocking Behavior Project built for bachelor graduation ***************************//
//***************************************************** Main Authur for this Scripts is Mahdi Bentaleb ***************************
//****************************************************** Co-workers Tina Abdelaziz and Zendagui Rafik **************************
//************************************************************ supervisor Dr. Mebarek Bouceta **************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FullManager : MonoBehaviour
{
    int numFlockManagers;
    int NumFish;
    public EnemyManager Em1;
    public FlockManager Fm1;
    public EnemyMove Enemy;
    public FlockManager[] allFlockManagers;
    public FlockAgent[] allFishPrefabs = new FlockAgent[10];
    public LeaderMove[] allLeader = new LeaderMove[10];
 
    private void Awake()
    {
        numFlockManagers = PlayerPrefs.GetInt("nflock");
        NumFish = PlayerPrefs.GetInt("nFish");
    }
    // Start is called before the first frame update
    void Start()
    {
        Fm1.transform.position = new Vector3(0, 0, 0);

        allFlockManagers = new FlockManager[numFlockManagers];

        for (int i = 0; i < numFlockManagers; i++)
        {
            Vector3 R = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            Fm1.FlockAgentPrefab = allFishPrefabs[i];
            Fm1.GoalObject = allLeader[i].gameObject;
            Fm1.EnemyObject = null;
            Fm1.transform.position = Fm1.transform.position + R ;
            allFlockManagers[i] = Instantiate(Fm1, transform);
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

       
        if (Input.GetKeyDown(KeyCode.P))
        {
            EnemyMove[] enemyList = FindObjectsOfType<EnemyMove>(); 
            if (enemyList.Length == 0)
            {
                
                EnemyMove ene = Em1.InitEnemy(Enemy);
                foreach (FlockManager manager in allFlockManagers)
                {
                    manager.UpdateEnemy(ene);
                }
            }
            else
            {
                foreach (EnemyMove enemy in enemyList) Destroy(enemy.gameObject);
            }
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            print("Leader");
            LeaderMove[] LeaderList = FindObjectsOfType<LeaderMove>();
            if (LeaderList.Length == 0)
            {
                foreach(FlockManager manager in allFlockManagers)
                {
                    manager.UpdateGoalObject();
                }
            }
            else
            {
                print(" no Leader");
                foreach (LeaderMove leader in LeaderList) Destroy(leader.gameObject);
            }
        }

    }
}
