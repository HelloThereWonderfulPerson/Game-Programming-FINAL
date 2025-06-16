using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuSong : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public AudioClip mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = mainMenu;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
