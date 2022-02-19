using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadlab : MonoBehaviour
{
    private void OnEnable()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
    }
}
