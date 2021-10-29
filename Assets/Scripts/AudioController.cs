using UnityEngine;

public class AudioController : MonoBehaviour
{
    public void PlayMusic()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void StopMusic()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
}
