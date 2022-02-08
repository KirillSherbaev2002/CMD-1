using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotToSpawn : MonoBehaviour
{
    public GameObject objectToSpawn;
    private bool isSpawnPossible = true;

    void Start()
    {
        if (isSpawnPossible)
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
