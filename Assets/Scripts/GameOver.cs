using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.gmInstance.playerLived == false)
        {
            playerStatus.text = "YOU DIED";
        }
        else if (GameManager.gmInstance.playerLived == true)
        {
            playerStatus.text = "YOU LIVED";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
