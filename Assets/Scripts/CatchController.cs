using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatchController : MonoBehaviour
{
    public GameObject Probirka;
    public AudioSource audioCatchProbirka;
    public AudioSource audioThrowProbirka;
    public float probirkaDistance;
    public float forceToAd;
    public float timeToWait;

    public TMP_Text probirkaCounter;
    public int countProbirka;

    private void Start()
    {
        probirkaCounter.text = countProbirka.ToString();
    }

    public void OnProbirkaCatched()
    {
        audioCatchProbirka.Play();
        countProbirka++;
        probirkaCounter.text = countProbirka.ToString();
    }

    public void OnProbirkaMinus()
    {
        audioThrowProbirka.Play();
        countProbirka--;
        probirkaCounter.text = countProbirka.ToString();
    }
}
