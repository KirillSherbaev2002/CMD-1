using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDataOnLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] characterSkins;

    void Awake()
    {
        ChooseSkinModel(RecourceManager.instance.currentSkin);
    }

    void ChooseSkinModel(int index)
    {
        Instantiate(characterSkins[index], transform.position, Quaternion.identity, transform);
    }
}
