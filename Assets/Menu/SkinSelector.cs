using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinSelector : MonoBehaviour
{
    [Header("Selection Buttons")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    
    [Header("Buying Buttons")]
    [SerializeField] private Button saveSkin;
    [SerializeField] private Button buy;
    [SerializeField] private TextMeshProUGUI priceText;

    [Header("Atributes")]
    [SerializeField] private int[] skinPrices;

    [Header("Misc")]
    [SerializeField] private GameObject buyParticles;

    private int currentSkin;

    void Start()
    {
        currentSkin = RecourceManager.instance.currentSkin;
        if(RecourceManager.instance.unlockedSkins[currentSkin])
            SelectSkin(currentSkin);
        else
            SelectSkin(0);
    }

    private void SelectSkin(int index)
    {
        previousButton.interactable = (index != 0);
        nextButton.interactable = (index != transform.childCount - 1); 

        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        if(RecourceManager.instance.unlockedSkins[currentSkin])
        {
            saveSkin.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
        }
        else
        {
            saveSkin.gameObject.SetActive(false);
            buy.gameObject.SetActive(true);
            priceText.text = "Купить за " + skinPrices[currentSkin] + "$";
        }
    }

    void Update()
    {
        if(buy.gameObject.activeInHierarchy)
            buy.interactable = (RecourceManager.instance.money >= skinPrices[currentSkin]);
    }

    public void ChangeSkin(int change)
    {
        currentSkin += change;

        RecourceManager.instance.currentSkin = currentSkin;
        RecourceManager.instance.Save();

        SelectSkin(currentSkin);
    }

    public void BuySkin()
    {
        RecourceManager.instance.money -= skinPrices[currentSkin];
        RecourceManager.instance.unlockedSkins[currentSkin] = true;
        RecourceManager.instance.Save();
        Instantiate(buyParticles);
        UpdateUI();
    }
}
