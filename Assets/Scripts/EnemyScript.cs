using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyScript : MonoBehaviour
{
    public AudioSource audioSource;
    private GameObject PastProbirka;
    public GameObject EffectCloud;
    private bool isPossible;

    private void OnMouseDown()
    {
        CatchController catchObject = FindObjectOfType<CatchController>();

        if (Vector3.Distance(catchObject.gameObject.transform.position, gameObject.transform.position) < catchObject.probirkaDistance)
        {
            if (catchObject.countProbirka <= 0)
            {
                return;
            }
            catchObject.OnProbirkaMinus();
            GameObject probirka = Instantiate(catchObject.Probirka, catchObject.gameObject.transform.position, catchObject.gameObject.transform.rotation);
            Vector3 targetDirection = transform.position - catchObject.gameObject.transform.position;
            probirka.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().AddForce(targetDirection * catchObject.forceToAd);
        }
        isPossible = false;
        StartCoroutine(Reload(catchObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Probirka")
        {
            audioSource.Play();
            Instantiate(EffectCloud, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), transform.rotation);
            gameObject.SetActive(false);
            var probirki = GameObject.FindGameObjectsWithTag("Probirka");
            foreach (GameObject probirka in probirki)
            {
                print("done");
                Destroy(probirka);
            }
        }
    }

    private IEnumerator Reload(CatchController catchObject)
    {
        yield return new WaitForSeconds(catchObject.timeToWait);
        isPossible = true;
    }
}
