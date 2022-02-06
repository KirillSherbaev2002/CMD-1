using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProbirkaScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DestroyItself());
    }

    private IEnumerator DestroyItself()
    {
        yield return new WaitForSeconds(0.2f);
        var probirki = GameObject.FindGameObjectsWithTag("Probirka");
        foreach(GameObject probirka in probirki)
        {
            print("done");
            Destroy(probirka);
        }
    }
}
