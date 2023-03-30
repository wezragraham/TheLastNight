using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance = null;

    float timeElapsed;

    public bool gameOver, phoneRang, lightsOff;

    public bool playerHasFlashlight;

    public GameObject killer, phone;

    [SerializeField]
    GameObject[] lights;

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
        phone = GameObject.FindGameObjectWithTag("Phone");

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= 30 && phoneRang == false)
            {
                PhoneRing();
            }

            if (timeElapsed >= 50 && playerHasFlashlight == false && phone.GetComponent<Phone>().answered == true && lightsOff == false)
            {
                TurnOutLights();
            }

            if (timeElapsed >= 60 && killer.activeSelf == false)
            {
                if (phone.GetComponent<Phone>().answered == false)
                {
                    killer.transform.position = new Vector3(-14, killer.transform.position.y, -8);
                }

                killer.SetActive(true);
            }

            if (timeElapsed >= 1800)
            {
                EndGame(true);
            }
        }
    }

    public void EndGame(bool playerSurvival)
    {
        StartCoroutine(WaitThenEnd(playerSurvival));

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
