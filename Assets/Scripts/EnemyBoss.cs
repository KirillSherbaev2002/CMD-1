using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] private float finalXValue;
    [SerializeField] private float startXValue;

    [SerializeField] private float speedOfMovingX;
    [SerializeField] private float speedOfLookingAtPlayer;

    [Header("Spawning")]
    public Action SpawnTry;
    public GameObject Player;
    public GameObject[] VirusToSpawn;
    public GameObject[] SpawnersObjectsColliders;

    [Header("EnemyBossGoesDown")]
    private int spawns;
    private bool bossActivated;
    [SerializeField] private int spawnToStop;
    public BossAnimatorSpawner bossAnimatorSpawner;


    public void ActivateBoss()
    {
        transform.localPosition = new Vector3(startXValue, transform.localPosition.y, transform.localPosition.z);
        bossAnimatorSpawner = FindObjectOfType<BossAnimatorSpawner>();
        StartCoroutine(SetToFinalX());
        bossActivated = true;
    }

    public void DeactivateBoss()
    {
        transform.localPosition = new Vector3(startXValue, transform.localPosition.y, transform.localPosition.z);
        bossActivated = false;
    }

    private void OnEnable()
    {
        SpawnTry += SpawnByAnimation;
    }

    private void OnDisable()
    {
        SpawnTry -= SpawnByAnimation;
    }

    public IEnumerator SetToFinalX()
    {
        var currentValue = startXValue;
        while (currentValue > finalXValue)
        {
            transform.localPosition -= new Vector3(speedOfMovingX * Time.deltaTime, 0, 0);
            currentValue -= speedOfMovingX * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void FixedUpdate()
    {
        if (!bossActivated) return;
        Vector3 targetDirection = Player.transform.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speedOfLookingAtPlayer, 0.0f);

        if (bossAnimatorSpawner.isMouseButtonDown)
        {
            return;
        }
        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void SpawnByAnimation()
    {
        if (!bossActivated) return;
        spawns += 1;
        CheckSpawnsToGoDown();

        if (spawns >= spawnToStop)
        {
            return;
        }
        SpawnVirus();
    }

    private void CheckSpawnsToGoDown()
    {
        if (!bossActivated) return;
        if (spawns >= spawnToStop)
        {
            transform.GetChild(0).GetComponent<BossAnimatorSpawner>().GoBossDown();
        }
    }

    public void SpawnVirus()
    {
        if (!bossActivated) return;
        if (spawns >= spawnToStop)
        {
            return;
        }
        foreach (GameObject spawnPlace in SpawnersObjectsColliders)
        {
            if(spawnPlace.GetComponent<ColliderToSpawnScript>().isCollides == false)
            {
                int indexVirus = UnityEngine.Random.Range(0, VirusToSpawn.Length - 1);
                Instantiate(VirusToSpawn[indexVirus], spawnPlace.transform.position, VirusToSpawn[indexVirus].transform.rotation);
            }
        }
    }
}
