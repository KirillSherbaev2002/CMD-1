using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] private float valueToAdd;
    private float value;
    //public Text valueText;
    public bool isCount;

    TextMeshProUGUI txt;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }


    private void FixedUpdate()
    {
        if (isCount == false) return;
        value += valueToAdd;
        txt.text = Mathf.Round(value).ToString();
        //valueText.text = Mathf.Round(value).ToString();
    }

    public void SetToZero()
    {
        value = 0;
    }
}
