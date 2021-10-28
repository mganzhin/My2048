using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void StopMusic()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
}
