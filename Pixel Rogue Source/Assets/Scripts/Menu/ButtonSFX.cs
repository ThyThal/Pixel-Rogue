using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioClip hoverAudio;
    [SerializeField] private AudioClip clickAudio;
    [SerializeField] public bool hasClicked;

    public void HoverSound() // Play Sound on Hover.
    {
        if (!hasClicked)
        {
            buttonSound.PlayOneShot(hoverAudio);
        }
    }

    public void ClickSound() // Play Sound on Click.
    {
        if (!hasClicked)
        {
            buttonSound.PlayOneShot(clickAudio);
        }
    }
}
