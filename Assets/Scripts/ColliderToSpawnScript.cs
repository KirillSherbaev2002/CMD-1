using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderToSpawnScript : MonoBehaviour
{
    [HideInInspector] public EnemyBoss enemyBoss;
    public bool isCollides;

    private void Start()
    {
        enemyBoss = FindObjectOfType<EnemyBoss>();
        isCollides = false;
    }

    public void OnCollisionExit(Collision collision)
    {
        isCollides = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        isCollides = true;
    }
}
