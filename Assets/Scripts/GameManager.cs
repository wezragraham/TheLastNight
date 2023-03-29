using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance = null;

    float timeElapsed;

    bool gameOver, phoneRang;

    [SerializeField]
    GameObject killer, phone;

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


        if (timeElapsed >= 1800)
        {
            EndGame(true);
        }

        if (timeElapsed >= 60 && phoneRang == false)
        {
            PhoneRing();
        }



    }

    public void EndGame(bool playerSurvival)
    {
        gameOver = true;
        SceneManager.LoadScene(2);
    }

    void PhoneRing()
    {
        phoneRang = true;
        phone.GetComponent<Phone>().Ring();
    }
}
