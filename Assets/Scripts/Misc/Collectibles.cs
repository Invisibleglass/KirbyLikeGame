using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public enum Collectibletype
    {
        wisp = 0,
        beam = 1,
    }

    public Collectibletype currentPickup;
    public AudioClip powerupSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (currentPickup)
            {
                case Collectibletype.beam:
                    break;
                case Collectibletype.wisp:

                    break;
            }

            if (powerupSound)
                collision.gameObject.GetComponent<AudioSourceManager>().PlayOneShot(powerupSound, false);

            Destroy(gameObject);
        }
    }
}