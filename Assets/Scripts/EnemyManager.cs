// ************************************************ Flocking Behavior Project built for bachelor graduation ***************************//
//***************************************************** Main Authur for this Scripts is Mahdi Bentaleb ***************************
//****************************************************** Co-workers Tina Abdelaziz and Zendagui Rafik **************************
//************************************************************ supervisor Dr. Mebarek Bouceta **************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Vector3 swimLimits = new Vector3(50, 10, 50);
    private Vector3 RanPos;

    public EnemyMove InitEnemy(EnemyMove _Enemy)
    {
        RanPos = new Vector3(Random.Range(-swimLimits.x + 1, swimLimits.x - 1),
                                                    Random.Range(-swimLimits.y + 1, swimLimits.y - 1),
                                                    Random.Range(-swimLimits.z + 1, swimLimits.z - 1));
        EnemyMove Enemy = Instantiate(
                _Enemy,
                RanPos,
                Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)),
                transform
                );

        Enemy.name = "Enemy";
        Enemy.enemyManager = this;
        return Enemy;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 1, 1);
        Gizmos.DrawWireCube(transform.position, swimLimits * 2);
    }
}

