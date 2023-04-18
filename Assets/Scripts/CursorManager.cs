using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public static CursorManager cmInstance = null;

    private void Awake()
    {
        if (cmInstance == null)
        {
            cmInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (cmInstance != this)
        {
            Destroy(this.gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && Cursor.visible == true)
        {
            Cursor.visible = false;
        }
        else if (SceneManager.GetActiveScene().buildIndex != 1 && Cursor.visible == false)
        {
            Cursor.visible = true;
        }
    }
}
