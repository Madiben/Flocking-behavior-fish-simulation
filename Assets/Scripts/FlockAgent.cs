// ************************************************ Flocking Behavior Project built for bachelor graduation ***************************//
//***************************************************** Main Authur for this Scripts is Mahdi Bentaleb ***************************
//****************************************************** Co-workers Tina Abdelaziz and Zendagui Rafik **************************
//************************************************************ supervisor Dr. Mebarek Bouceta **************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    private List<FlockAgent> agents;
    [SerializeField]
    private List<FlockAgent> neighbors;

    private FlockManager manager;
    [SerializeField]
    private GameObject goalObject;
    private EnemyMove enemyObject;
    private float alignmentWeight;
    private float cohesionWeight;
    private float avoidanceWeight;
    private float rotationSpeed;
    private float moveSpeed;
    private float hitRange;
    private float neighberRadius;
    private float avoidenceRadius;
    private Vector3 swimLimits;
    private bool isInit;

    public EnemyMove EnemyObject {  set => enemyObject = value; }
    public GameObject GoalObject {  set => goalObject = value; }

    private void Awake()
    {
        isInit = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * hitRange);
        Gizmos.color = new Color(0.5f, 1f, 0.5f, 0.4f);
        Gizmos.DrawSphere(transform.position, neighberRadius / 2);

    }

    // Update is called once per frame
    void Update()
    {
        if (isInit)
        {
            Bounds bounds = new Bounds(manager.transform.position, swimLimits * 2);
            if (!bounds.Contains(transform.position))
            {
                Vector3 direction = manager.transform.position - transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
            else
            {
                RaycastHit hit = new RaycastHit();
                bool isFriend = true;

                if (Physics.Raycast(transform.position, transform.forward, out hit, hitRange))
                {


                    if (hit.transform.TryGetComponent(out FlockAgent agent))
                    {
                        isFriend = agents.Contains(agent);

                    }

                    else
                    {
                        isFriend = false;

                    }

                }

                if (!isFriend)
                {

                    Vector3 direction = Vector3.Reflect(transform.forward, hit.normal);
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 4 * rotationSpeed * Time.deltaTime);

                }
                else
                {

                    if (Random.Range(0, 100) < 10)
                    {
                        if (neighbors == null)
                        {
                            neighbors = GetNeighbors();
                        }
                        else
                        {

                            neighbors.Clear();
                            neighbors = GetNeighbors();
                        }
                    }

                    ApplyRules(Combine(Alignment(agents), Cohesion(agents), Avoidance(agents)));


                }
            }
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }
    }

    public void InitAgent(
        FlockManager _manager,
        List<FlockAgent> _agents,
        GameObject _goalObject,
        EnemyMove _enemyObject,
        float _alignmentWeight,
        float _cohesionWeight,
        float _avoidanceWeight,
        float _rotationSpeed,
        float _moveSpeed,
        float _hitRange,
        float _neighberRadius,
        float _avoidenceRadius,
        Vector3 _swimLimits
        )
    {
        manager = _manager;
        agents = _agents;
        goalObject = _goalObject;
        enemyObject = _enemyObject;
        alignmentWeight = _alignmentWeight;
        cohesionWeight = _cohesionWeight;
        avoidanceWeight = _avoidanceWeight;
        rotationSpeed = _rotationSpeed;
        moveSpeed = _moveSpeed;
        hitRange = _hitRange;
        neighberRadius = _neighberRadius;
        avoidenceRadius = _avoidenceRadius;
        swimLimits = _swimLimits;

 

        isInit = true;

    }

    private List<FlockAgent> GetNeighbors()
    {
        List<FlockAgent> _neighbors = new List<FlockAgent>();

        foreach (FlockAgent agent in agents)
        {
            if (agent != this)
            {
                if (DistanceFrom(agent) <= neighberRadius)
                {
                    if (!_neighbors.Contains(agent)) _neighbors.Add(agent);
                }
            }
        }
        return _neighbors;
    }
    private Vector3 Alignment(List<FlockAgent> _neighbors)
    {

        if (_neighbors.Count < 1)
        {
            return Vector3.zero;
        }

        Vector3 totalForward = Vector3.zero;

        foreach (FlockAgent neighbor in _neighbors)
        {
            totalForward += neighbor.transform.forward;
        }

        totalForward /= _neighbors.Count;

        totalForward.Normalize();

        return totalForward;
    }

    private Vector3 Cohesion(List<FlockAgent> _neighbors)
    {

        if (_neighbors.Count < 1)
        {
            return Vector3.zero;
        }

        Vector3 totalPosition = Vector3.zero;

        foreach (FlockAgent neighbor in _neighbors)
        {
            totalPosition += neighbor.transform.position;
        }

        totalPosition /= _neighbors.Count;

        totalPosition -= transform.position;

        totalPosition.Normalize();

        return totalPosition;
    }

    private Vector3 Avoidance(List<FlockAgent> _neighbors)
    {
        if (_neighbors.Count < 1)
        {
            return Vector3.zero;
        }

        Vector3 totalAvoid = Vector3.zero;

        int avoidCount = 0;

        foreach (FlockAgent neighbor in _neighbors)
        {
            if (DistanceFrom(neighbor) < avoidenceRadius)
            {
                avoidCount++;
                totalAvoid += (neighbor.transform.position - transform.position);
            }
        }

        totalAvoid *= -1;

        if (avoidCount > 0)
            totalAvoid /= avoidCount;

        totalAvoid.Normalize();

        return totalAvoid;
    }

    private Vector3 Combine(Vector3 alignment, Vector3 cohesion, Vector3 avoidance)
    {
        Vector3 combained = Vector3.zero;
        Vector3[] vectors = new Vector3[] { alignment, cohesion, avoidance };
        float[] weights = new float[] { alignmentWeight, cohesionWeight, avoidanceWeight };

        for (int i = 0; i < 3; i++)
        {
            combained += vectors[i] * weights[i];
        }


        return combained;
    }

    private void ApplyRules(Vector3 direction)
    {
        bool isEnemy = false;
        float enemyDist = 10000000;

        if (enemyObject != null) enemyDist = Vector3.Distance(transform.position, enemyObject.transform.position);
        
        if (enemyObject != null && enemyDist < neighberRadius * 2)
        {
            isEnemy = true;
            direction -= enemyObject.transform.position;
        }
        else
        {
            float goalDist = 0;
            if(goalObject !=null) goalDist = Vector3.Distance(transform.position, goalObject.transform.position);

            if (goalObject != null && goalDist > neighberRadius * 0.5)
            {
                direction += goalObject.transform.position;
            }
        }


        if (direction != Vector3.zero)
        {
            direction -= transform.position;
            
            if(isEnemy) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 3 * rotationSpeed * Time.deltaTime);
            else transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        }

    }


    private float DistanceFrom(FlockAgent agent)
    {
        return Vector3.Distance(transform.position, agent.transform.position);
    }

}
