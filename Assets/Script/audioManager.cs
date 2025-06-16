using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip jump;
    public AudioClip wallJump;
    public AudioClip death;
    public AudioClip takeDamage;
    public AudioClip cureWhale;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.volume = 0.5f;

        musicSource.clip = background;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
