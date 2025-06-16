using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class heartSystem : MonoBehaviour
{
    //playerEncounters health = collision.GetComponent<playerEncounters>();
    public Image[] cloversImages;
    public Sprite clover;
    public playerEncounters health;

    // Start is called before the first frame update
    void Start()
    {
        if (health == null)
        {
            health = FindObjectOfType<playerEncounters>();
        }

        // Initialize with 3 clovers
        if (health != null)
        {
            health.playerMaxHealth = 3;
        }

        CloverUI();
    }

    // Update is called once per frame
    void Update()
    {
        CloverUI();
    }

    void CloverUI()
    {
        for (int i = 0; i < cloversImages.Length; i++)
        {
            if (cloversImages[i] != null && clover != null)
            {
                cloversImages[i].sprite = clover;
                cloversImages[i].enabled = (i < health.playerCurrentHealth);
            }
        }
    }
}
