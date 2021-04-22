// ************************************************ Flocking Behavior Project built for bachelor graduation ***************************//
//***************************************************** Main Authur for this Scripts is Mahdi Bentaleb ***************************
//****************************************************** Co-workers Tina Abdelaziz and Zendagui Rafik **************************
//************************************************************ supervisor Dr. Mebarek Bouceta **************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{

    [SerializeField]
    private FlockAgent flockAgentPrefab;
    [SerializeField]
    private int agentCount;
    [SerializeField]
    private List<FlockAgent> agents;
    [SerializeField]
    private GameObject goalObject;
    [SerializeField]
    private EnemyMove enemyObject;
    [SerializeField]
    [Range(1f, 50f)]
    private float alignmentWeight = 1;
    [SerializeField]
    [Range(1f, 50f)]
    private float cohesionWeight = 2;
    [SerializeField]
    [Range(1f, 50f)]
    private float avoidanceWeight = 6;
    [SerializeField]
    private float rotationSpeed = 3f;
    [SerializeField]
    private float fishSpeed;
    [SerializeField]
    private float hitRange;
    [SerializeField]
    private float neighberRadius;
    private float avoidenceRadius;
    [SerializeField]
    public Vector3 swimLimits = new Vector3(20, 10, 20);

    public FlockAgent FlockAgentPrefab { set => flockAgentPrefab = value; }
    public GameObject GoalObject { set => goalObject = value; }
    public EnemyMove EnemyObject { set => enemyObject = value; }

    private void Awake()
    {
        agentCount = PlayerPrefs.GetInt("nFish");
        fishSpeed = PlayerPrefs.GetFloat("fishSpeed");
        neighberRadius = PlayerPrefs.GetFloat("neighbourraduis");
        hitRange = PlayerPrefs.GetFloat("maxHit");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireCube(transform.position, swimLimits * 2);

    }
    // Start is called before the first frame update
    void Start()
    {
        InitManager();
    }


    private void InitManager()
    {
        Vector3 position = transform.position + new Vector3(
               Random.Range(-swimLimits.x + 1, swimLimits.x - 1),
               Random.Range(-swimLimits.y + 1, swimLimits.y - 1),
               Random.Range(-swimLimits.z + 1, swimLimits.z - 1)
               );


        avoidenceRadius = 0.5f * neighberRadius;

        if (flockAgentPrefab == null || agentCount < 1)
        {
            Debug.LogError("Agent is null or count is zero");
            return;
        }

        agents = new List<FlockAgent>(agentCount);

        for (int i = 0; i < agentCount; i++)
        {
            Vector3 randomPos = transform.position + new Vector3(
                Random.Range(-swimLimits.x + 1, swimLimits.x - 1),
                Random.Range(-swimLimits.y + 1, swimLimits.y - 1),
                Random.Range(-swimLimits.z + 1, swimLimits.z - 1)
                );

            FlockAgent tempAgent = Instantiate(
                flockAgentPrefab,
                randomPos,
                Quaternion.Euler(0, Random.Range(-180, 180), 0),
                transform
                );

            tempAgent.name = "Agent" + i;
            agents.Add(tempAgent);

        }

        foreach (FlockAgent agent in agents)
        {
            agent.InitAgent(
                this,
                agents,
                null,
                null,
                alignmentWeight,
                cohesionWeight,
                avoidanceWeight,
                rotationSpeed,
                fishSpeed,
                hitRange,
                neighberRadius,
                avoidenceRadius,
                swimLimits
                );
        }

    }

  // public void ChangeGoalPosition()
  // {
  //     Vector3 position = transform.position + new Vector3(
  //            Random.Range(-swimLimits.x + 1, swimLimits.x - 1),
  //            Random.Range(-swimLimits.y + 1, swimLimits.y - 1),
  //            Random.Range(-swimLimits.z + 1, swimLimits.z - 1)
  //            );
  //     goalObject.transform.position = position;
  // }
    public void UpdateEnemy(EnemyMove enemy)
    {
        enemyObject = enemy;
        foreach (FlockAgent agent in agents)
        {
            agent.EnemyObject = enemy;
        }
    }

    public void UpdateGoalObject()
    {
        GameObject leader = Instantiate(goalObject,transform);
        leader.GetComponent<LeaderMove>().manager = this;
        foreach (FlockAgent agent in agents)
        {
            agent.GoalObject = leader;
        }
    }
}
