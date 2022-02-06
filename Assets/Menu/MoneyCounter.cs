using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    } 

    void Start()
    {
        text.text = RecourceManager.instance.money + "";
    }

    void Update()
    {
        text.text = RecourceManager.instance.money + "";
        if(Input.GetKeyDown(KeyCode.O))
        {
            RecourceManager.instance.money += 100;
            RecourceManager.instance.Save();
            text.text = RecourceManager.instance.money + "";
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            RecourceManager.instance.money -= 100;
            RecourceManager.instance.Save();
            text.text = RecourceManager.instance.money + "";
        }
    }
}
