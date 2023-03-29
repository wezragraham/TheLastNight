using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    [SerializeField]
    AudioClip ring, call;

    AudioSource myAudio;

    public bool isRinging;

    public bool answered;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = this.gameObject.GetComponent<AudioSource>();
        myAudio.clip = ring;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ring()
    {
        myAudio.Play();
        isRinging = true;
    }

    public void PickUp()
    {
        myAudio.clip = call;
        myAudio.Play();
        answered = true;
        isRinging = false;
    }
}
