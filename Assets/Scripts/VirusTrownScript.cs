using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusTrownScript : MonoBehaviour
{
    public GameObject Smoke;
    [SerializeField] private float timeToMoveOutOfPlayer;
    [SerializeField] private float timeBlow;
    private bool isOutOfPlayer;

    private void OnEnable()
    {
        StartCoroutine(ExitDetection());
        StartCoroutine(BlowDetection());
    }

    private IEnumerator ExitDetection()
    {
        yield return new WaitForSeconds(timeToMoveOutOfPlayer);
        isOutOfPlayer = true;
    }

    private IEnumerator BlowDetection()
    {
        yield return new WaitForSeconds(timeBlow);
        Instantiate(Smoke, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("collision");
        if (isOutOfPlayer == false) return;
        Instantiate(Smoke, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("collision");
        if (isOutOfPlayer == false) return;
        Instantiate(Smoke, collision.transform.position, collision.transform.rotation);
        Destroy(gameObject);
    }
}
