using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] GameObject boomParticle;
    [SerializeField] GameObject playerController;
    [SerializeField] GameObject playerMesh;
    [SerializeField] AudioSource boomSound;


    public SplineFollower splineFollower;
    public SplineComputer splineComputer;
    public SplineProjector splineProjector;

    [Header("Material")]
    public Material[] materials;
    public GameObject tiger;

    [Header("Level")]
    public Text scoreOnWay;
    public int sceneToLoad;
    public int segmentToLoad;

    [SerializeField] private float[] rotationForYOnStage;
    private float nextPercent;
    private float currentPercent;
    private int currentPointIndex = 0;
    private bool isTheLastSegment = false;

    [Header("GoDown")]
    [SerializeField] private bool isGoDownNeeded;
    [SerializeField] private float percentGoDown;

    [SerializeField] private float cameraGoDown;
    [SerializeField] private float characterGoDown;

    private void Start()
    {
        int currentIndex = PlayerPrefs.GetInt("material");
        tiger.GetComponent<SkinnedMeshRenderer>().material = materials[currentIndex];
    }

    private void OnCollisionEnter(Collision other)
    {
        var particles = Instantiate(boomParticle, other.transform.position, Quaternion.identity);

        if (other.gameObject.CompareTag("Obsticle"))
        {

            StartCoroutine(Death());
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        boomSound.Play();
        playerMesh.GetComponent<AudioSource>().volume = 0;
        splineFollower.enabled = false;
        playerMesh.GetComponent<Animator>().SetTrigger("Dead");
        yield return new WaitForSeconds(1.5f);
        playerMesh.GetComponent<AudioSource>().volume = 0.06f;
        splineFollower.enabled = true;
        splineFollower.SetPercent(0);
        SetBack();
        print("Death");
        yield break;
    }

    private void OnPlayerSwitchTrajectory()
    {
        if (currentPercent < splineComputer.GetPointPercent(splineComputer.pointCount - 2)) isTheLastSegment = false;
        if (currentPercent < nextPercent || isTheLastSegment) return;
        splineFollower.transform.rotation = Quaternion.Euler(0,rotationForYOnStage[currentPointIndex],0);
        nextPercent = (float) splineComputer.GetPointPercent(currentPointIndex + 1);
        currentPointIndex++;
        CheckLastSegment();
    }

    private void CameraAngleAndCheck()
    {
        if (!isGoDownNeeded) return;
        percentGoDown = (float)splineComputer.GetPointPercent(currentPointIndex + 1);

    }

    private void CheckLastSegment()
    {
        if (currentPercent > splineComputer.GetPointPercent(splineComputer.pointCount - 2))
        {
            print(currentPointIndex);
            SetBack();
            isTheLastSegment = true;
        }
    }

    private void SetBack()
    {
        currentPointIndex = 0;
        nextPercent = 0;
        Quaternion.Euler(0, rotationForYOnStage[currentPointIndex], 0);
        currentPercent = (float)splineProjector.result.percent;

        ScoreScript score = FindObjectOfType<ScoreScript>();
        score.SetToZero();
    }

    private void FixedUpdate()
    {
        currentPercent = (float) splineProjector.result.percent;
        OnPlayerSwitchTrajectory();
        scoreOnWay.text = "Line "+ currentPointIndex.ToString();
        CheckOnEnd();
    }

    public void SetToFastMode()
    {
        splineFollower.followSpeed = 50f;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void SetNormalMode()
    {
        splineFollower.followSpeed = 16f;
        GetComponent<BoxCollider>().isTrigger = false;
    }

    private void CheckOnEnd()
    {
        if(currentPointIndex == segmentToLoad)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
