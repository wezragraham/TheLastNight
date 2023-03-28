using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float timeElapsed;
    bool gameOver;

    public bool playerSurvived;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= 1800)
        {
            playerSurvived = true;
            gameOver = true;
        }

        if (gameOver)
        {
            SceneManager.LoadScene(2);
        }

    }
}
