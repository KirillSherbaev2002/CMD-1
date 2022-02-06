using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyScript : MonoBehaviour
{
    private GameObject PastProbirka;
    private bool isPossible;

    private void OnMouseDown()
    {
        CatchController catchObject = FindObjectOfType<CatchController>();

        if(Vector3.Distance(catchObject.gameObject.transform.position, gameObject.transform.position) < catchObject.probirkaDistance)
        {
            GameObject probirka =Instantiate(catchObject.Probirka, catchObject.gameObject.transform.position, catchObject.gameObject.transform.rotation);
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
