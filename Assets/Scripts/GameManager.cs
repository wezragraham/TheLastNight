using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance = null;

    public float timeElapsed;

    float damageTimer;

    public bool gameOver, phoneRang, lightsOff;

    public bool playerHasFlashlight, playerHasKnife;

    public GameObject killer, phone, player;

    [SerializeField]
    GameObject[] lights;

    ParticleSystem fire;

    public bool playerLived;

    private void Awake()
    {

        if (gmInstance == null)
        {
            gmInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (gmInstance != this)
        {
            Destroy(this.gameObject);
        }

        killer = GameObject.FindGameObjectWithTag("Killer");
        killer.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");

        phone = GameObject.FindGameObjectWithTag("Phone");
        fire = GameObject.Find("Rooms").GetComponent<ParticleSystem>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!gameOver)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 20 && phoneRang == false)
            {
                PhoneRing();
            }

            if (timeElapsed >= 40 && playerHasFlashlight == false && phone.GetComponent<Phone>().answered == true && lightsOff == false)
            {
                TurnOutLights();
            }

            if (timeElapsed >= 60 && killer.activeSelf == false)
            {
                if (phone.GetComponent<Phone>().answered == false)
                {
                    killer.transform.position = new Vector3(-14, killer.transform.position.y, -8);
                }

                if(playerHasKnife == true)
                {
                    killer.transform.position = new Vector3(-10, killer.transform.position.y, 0);
                    fire.Play();

                }



                killer.SetActive(true);
            }


            if (fire.isPlaying)
            {
                damageTimer += Time.deltaTime;
                if (damageTimer > 3)
                {
                    player.GetComponent<Health>().TakeDamage(8);
                    damageTimer = 0;
                }

            }

            if (timeElapsed >= 1800)
            {
                EndGame(true);
            }
        }





    }

    public void EndGame(bool playerSurvival)
    {
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            StartCoroutine(WaitThenEnd(playerSurvival));
        }


    }

    void PhoneRing()
    {
        phoneRang = true;
        phone.GetComponent<Phone>().Ring();
    }

    void TurnOutLights()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
        lightsOff = true;
    }

    IEnumerator WaitThenEnd(bool playerSurvival)
    {
        yield return new WaitForSeconds(5);
        gameOver = true;
        timeElapsed = 0;
        playerLived = playerSurvival;
        SceneManager.LoadScene(2);
    }
}
