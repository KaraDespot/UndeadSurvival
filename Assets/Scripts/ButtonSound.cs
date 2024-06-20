using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource clickSound; 

    void Awake()
    {
        clickSound = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            clickSound.Play(); 
        }
    }
}