using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class HealthController : MonoBehaviour
{
    public BoxCollider PlayerInterractor;
    public PlayerInteraction playerInteraction;
    public EnemiesScript enemiesScript;
    public Animator AnimatorToShowDamage;
    public AudioSource audioSource;
    public EnemyBoss enemyBoss;

    [SerializeField] private int HealthPoints;
    private int startPoints;

    private void OnEnable()
    {
        PlayerInteraction playerInteraction = FindObjectOfType<PlayerInteraction>();
        playerInteraction.collision += MakeHpLower;
    }

    private void OnDisable()
    {
        playerInteraction.collision -= MakeHpLower;
    }

    private void Start()
    {
        startPoints = HealthPoints;
    }

    public void MakeHpLower()
    {
        HealthPoints--;
        audioSource.Play();
        if (HealthPoints <= 0)
        {
            enemyBoss.DeactivateBoss();
            StartCoroutine(playerInteraction.Death());
            HealthPoints = startPoints;
            enemiesScript.ActivationEnemyBack();
            return;
        }
        AnimatorToShowDamage.SetTrigger("Jump");
    }
}
