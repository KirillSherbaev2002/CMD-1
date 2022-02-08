using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorSpawner : MonoBehaviour
{
    public EnemyBoss enemyBoss;
    public float valueToGoDown;
    private bool isDeactivatedFalling;
    public bool isMouseButtonDown = false;

    [Header("Catching")]
    private bool isPossible;
    [SerializeField] private float forceToAdd;
    private GameObject virusToThrow;
    public GameObject[] virusesToThrow;
    public GameObject bossCenter;
    [SerializeField] private int lives;

    public void Start()
    {
        enemyBoss = FindObjectOfType<EnemyBoss>();
    }

    public void SetSpawn()
    {
        enemyBoss.SpawnTry();
    }

    public void GoBossDown()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(GetComponent<Animator>());
    }

    public void Update()
    {
        if(transform.localPosition.y <= valueToGoDown && !isDeactivatedFalling)
        {
            isMouseButtonDown = true;
            isDeactivatedFalling = true;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent.transform.parent = transform.parent.transform.parent.transform.parent.transform;
        }
    }

    private void OnMouseDown()
    {
        if (isMouseButtonDown == false) ThrowVirus();
        else ThrowProbirka();
    }

    //private void ThrowVirus()
    //{
    //    virusToThrow = virusesToThrow[Random.Range(0, virusesToThrow.Length - 1)];
    //    CatchController catchObject = FindObjectOfType<CatchController>();
    //    GameObject virus = Instantiate(virusToThrow, catchObject.gameObject.transform.position, catchObject.gameObject.transform.rotation);

    //    Vector3 positionOfTarget = bossCenter.transform.position;
    //    positionOfTarget = new Vector3(positionOfTarget.x, positionOfTarget.y - 7f, positionOfTarget.z);

    //    Vector3 targetDirection = positionOfTarget - catchObject.transform.GetChild(0).transform.GetChild(0).transform.position;
    //    virus.gameObject.GetComponent<Rigidbody>().AddForce(targetDirection * forceToAdd);
    //}

    private void ThrowVirus()
    {
        virusToThrow = virusesToThrow[Random.Range(0, virusesToThrow.Length - 1)];
        CatchController catchObject = FindObjectOfType<CatchController>();
        GameObject virus = Instantiate(virusToThrow, catchObject.gameObject.transform.position, catchObject.gameObject.transform.rotation);
        Vector3 bossCenterPosition = new Vector3(bossCenter.transform.position.x - 10f, bossCenter.transform.position.y, bossCenter.transform.position.z);
        Vector3 targetDirection = bossCenterPosition - catchObject.gameObject.transform.position;
        virus.GetComponent<Rigidbody>().AddForce(targetDirection * forceToAdd);
    }

    private void ThrowProbirka()
    {
        CatchController catchObject = FindObjectOfType<CatchController>();
        if (catchObject.countProbirka <= 0)
        {
            return;
        }
        GameObject probirka = Instantiate(catchObject.Probirka, catchObject.gameObject.transform.position, catchObject.gameObject.transform.rotation);
        Vector3 targetDirection = transform.position - catchObject.gameObject.transform.position;
        probirka.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().AddForce(targetDirection * catchObject.forceToAd);
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Probirka")
        {
            EnemyScript enemyScript = FindObjectOfType<EnemyScript>();
            GameObject EffectCloud = enemyScript.EffectCloud;
            Instantiate(EffectCloud, transform.GetChild(0).position, transform.rotation);
            gameObject.transform.parent.gameObject.SetActive(false);
            var probirki = GameObject.FindGameObjectsWithTag("Probirka");
            foreach (GameObject probirka in probirki)
            {
                print("done");
                Destroy(probirka);
            }
        }
        if (other.gameObject.tag == "VirusThrown")
        {
            lives--;
            CheckLives();
        }
    }

    private void CheckLives()
    {
        if(lives <= 0)
        {
            GoBossDown();
        }
    }
}
