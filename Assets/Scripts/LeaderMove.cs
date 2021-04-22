// ************************************************ Flocking Behavior Project built for bachelor graduation ***************************//
//***************************************************** Main Authur for this Scripts is Mahdi Bentaleb ***************************
//****************************************************** Co-workers Tina Abdelaziz and Zendagui Rafik **************************
//************************************************************ supervisor Dr. Mebarek Bouceta **************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderMove : MonoBehaviour
{
    public FlockManager manager;
    public float speed;
    Vector3 RanPos = Vector3.zero;

    private void Start()
    {

        RanPos = new Vector3(Random.Range(-manager.swimLimits.x + 4, manager.swimLimits.x - 4),
                                                Random.Range(-manager.swimLimits.y + 4, manager.swimLimits.y - 4),
                                                Random.Range(-manager.swimLimits.z + 4, manager.swimLimits.z - 4));
    }
    private void Awake()
    {
        speed = PlayerPrefs.GetFloat("leaderSpeed");
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction;
        Bounds bounds = new Bounds(manager.transform.position, manager.swimLimits * 2);
        if (!bounds.Contains(transform.position))
        {
            direction = manager.transform.position - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed / 2 * Time.deltaTime);
        }
        else
        {
            RaycastHit hit = new RaycastHit();
            

            if (Physics.Raycast(transform.position, transform.forward, out hit, 4f))
            {

                direction = Vector3.Reflect(transform.forward, hit.normal);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 4 * speed * Time.deltaTime);


            }

            if (Random.Range(0, 20) < 10)
            {
                float distance = Vector3.Distance(transform.position, RanPos);
                if (distance < 1)
                {
                    RanPos = new Vector3(Random.Range(-manager.swimLimits.x + 1, manager.swimLimits.x - 1),
                                                            Random.Range(-manager.swimLimits.y + 1, manager.swimLimits.y - 1),
                                                            Random.Range(-manager.swimLimits.z + 1, manager.swimLimits.z - 1));
                }


                direction = RanPos - transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed * Time.deltaTime);
            }

               
            
        }

        transform.Translate(0, 0, Time.deltaTime * speed);

    }
}