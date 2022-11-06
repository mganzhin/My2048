using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private AudioSource audioSource;
    
    public void ToggleMusic()
    {
        if (musicToggle != null)
        {
            if (musicToggle.isOn)
            {
                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }
            else
            {
                if (audioSource != null)
                {
                    audioSource.Stop();
                }
            }
        }
    }

    public void PlayMusic()
    {
        if (musicToggle != null)
        {
            if (musicToggle.isOn)
            {
                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }
        }
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
