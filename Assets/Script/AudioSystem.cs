using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioSystem : MonoBehaviour {

    static AudioSource audioManager;
    public AudioClip gameButton;
    public AudioClip menuButton;
    public AudioClip victory;

    public Button mute;
    public Sprite muteSprite, unmuteSprite;

    bool isMuted = false;

    void Start()
    {
        audioManager = this.GetComponent<AudioSource>();
    }

    public void MuteorUnmute()
    {
        isMuted = !isMuted;

        if (isMuted) mute.image.sprite = muteSprite;
        if (!isMuted) mute.image.sprite = unmuteSprite;
    }

    public void PlayButtonPress()
    {
        audioManager.pitch = 0.6f;
        if (!isMuted) audioManager.PlayOneShot(gameButton);
    }

    public void PlayRedoButton()
    {
        audioManager.pitch = 0.3f;
        if (!isMuted) audioManager.PlayOneShot(menuButton);
    }

    public void PlayNormalButton()
    {
        audioManager.pitch = 1f;
        if (!isMuted) audioManager.PlayOneShot(menuButton);
    }

    public void PlayWonGame()
    {
        if (!isMuted) audioManager.PlayOneShot(victory);
    }
}