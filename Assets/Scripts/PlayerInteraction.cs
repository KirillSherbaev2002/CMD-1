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
    public SplineFollower splineFollowerBoss;
    public SplineComputer splineComputer;
    public SplineProjector splineProjector;
    public HealthController healthController;
    public CatchController catchController;

    [Header("Material")]
    public Material[] materials;
    public GameObject tiger;

    [Header("Level")]
    public int sceneToLoad;
    public int segmentToLoad;

    public float[] rotationForYOnStage;
    public float nextPercent;
    public float currentPercent;
    public int currentPointIndex = 0;
    private bool isTheLastSegment = false;

    [Header("GoDown")]
    [SerializeField] private bool isGoDownNeeded;
    [SerializeField] private float percentGoDown;

    [SerializeField] private float cameraGoDown;
    [SerializeField] private float characterGoDown;

    public Action collision;

    private void Start()
    {
        int currentIndex = PlayerPrefs.GetInt("material");
        tiger.GetComponent<SkinnedMeshRenderer>().material = materials[currentIndex];
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BossTrigger"))
        {
            return;
        }
        var particles = Instantiate(boomParticle, other.transform.position, Quaternion.identity);

        if (other.gameObject.CompareTag("Obsticle"))
        {
            collision();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            collision();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ProbirkaToCatch"))
        {
            Destroy(other.gameObject);
            catchController.OnProbirkaCatched();
        }
    }

    public IEnumerator Death()
    {
        boomSound.Play();
        playerMesh.GetComponent<AudioSource>().volume = 0;

        splineFollower.enabled = false;
        splineFollowerBoss.enabled = false;

        playerMesh.GetComponent<Animator>().SetTrigger("Dead");
        yield return new WaitForSeconds(1.5f);
        playerMesh.GetComponent<AudioSource>().volume = 0.06f;

        splineFollower.enabled = true;
        splineFollower.SetPercent(0);
        splineFollowerBoss.SetPercent(0);

        OnPlayerSwitchTrajectory();
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
        Quaternion.Euler(0, rotationForYOnStage[currentPointIndex], 0);
    }

    private void FixedUpdate()
    {
        currentPercent = (float) splineProjector.result.percent;
        OnPlayerSwitchTrajectory();
        CheckOnEnd();
    }

    public void SetToFastMode()
    {
        splineFollower.followSpeed = 50f;
        splineFollowerBoss.followSpeed = 50f;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void SetNormalMode()
    {
        splineFollower.followSpeed = 18f;
        splineFollowerBoss.followSpeed = 18f;
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
