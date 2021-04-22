// ************************************************ Flocking Behavior Project built for bachelor graduation ***************************//
//***************************************************** Main Authur for this Scripts is Mahdi Bentaleb ***************************
//****************************************************** Co-workers Tina Abdelaziz and Zendagui Rafik **************************
//************************************************************ supervisor Dr. Mebarek Bouceta **************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [HideInInspector]
    public EnemyManager enemyManager;
    public float speed = 3f;
    Vector3 RanPos = Vector3.zero;

    private void Start()
    {

        RanPos = new Vector3(Random.Range(-enemyManager.swimLimits.x + 1, enemyManager.swimLimits.x - 1),
                                                Random.Range(-enemyManager.swimLimits.y + 1, enemyManager.swimLimits.y - 1),
                                                Random.Range(-enemyManager.swimLimits.z + 1, enemyManager.swimLimits.z - 1));
    }
    private void Awake()
    {
        speed = PlayerPrefs.GetFloat("predatorspeed");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction;
        Bounds bounds = new Bounds(enemyManager.transform.position, enemyManager.swimLimits * 2);
        if (!bounds.Contains(transform.position))
        {
            direction = enemyManager.transform.position - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed / 2 * Time.deltaTime);
        }
        else
        {
            RaycastHit hit = new RaycastHit();


            if (false)
            {

                direction = Vector3.Reflect(transform.forward, hit.normal);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 4 * speed * Time.deltaTime);


            }
            else
            {
                float distance = Vector3.Distance(transform.position, RanPos);
                if (distance < 1)
                {
                    RanPos = new Vector3(Random.Range(-enemyManager.swimLimits.x + 1, enemyManager.swimLimits.x - 1),
                                                            Random.Range(-enemyManager.swimLimits.y + 1, enemyManager.swimLimits.y - 1),
                                                            Random.Range(-enemyManager.swimLimits.z + 1, enemyManager.swimLimits.z - 1));
                }


                direction = RanPos - transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed * Time.deltaTime);
            }

        }

        transform.Translate(0, 0, Time.deltaTime * speed);

    }
}

