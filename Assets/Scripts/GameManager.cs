using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance = null;

    float timeElapsed;

    bool gameOver, phoneRang, lightsOff;

    public bool playerHasFlashlight;

    GameObject killer, phone;

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
        phone = GameObject.FindGameObjectWithTag("Phone");

    }

    // Start is called before the first frame update
    void Start()
    {
        killer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            timeElapsed += Time.deltaTime;
        }


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
            if (phone.GetComponent<Phone>().answered == true)
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

    public void EndGame(bool playerSurvival)
    {
        gameOver = true;
        playerLived = playerSurvival;
        SceneManager.LoadScene(2);
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
}
