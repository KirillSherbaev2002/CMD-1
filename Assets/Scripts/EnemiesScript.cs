using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesScript : MonoBehaviour
{
    private GameObject[] Enemies;

    void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void ActivationEnemyBack()
    {
        foreach(GameObject enemy in Enemies)
        {
            enemy.SetActive(true);
        }
    }
}
