using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInstatntiator : MonoBehaviour
{
    public EnemyBoss enemyBoss;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enemyBoss.ActivateBoss();
        }
    }
}
