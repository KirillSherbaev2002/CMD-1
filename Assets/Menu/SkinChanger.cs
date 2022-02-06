using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private Material[] skins;

    [SerializeField] private Renderer _renderer;

    void Start()
    {
        SetSkin();
    }

    public void SetSkin()
    {
        _renderer.sharedMaterial = skins[RecourceManager.instance.currentSkin];
        PlayerPrefs.SetInt("material", RecourceManager.instance.currentSkin);
    }
}
