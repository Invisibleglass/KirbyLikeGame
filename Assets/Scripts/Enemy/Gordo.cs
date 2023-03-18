using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gordo : MonoBehaviour
{
    AudioSourceManager asm;

    public AudioClip thudSound;
    
    void Start()
    {
        asm = GetComponent<AudioSourceManager>();
    }
    private void OnCollision2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
            asm.PlayOneShot(thudSound, false);

    }
}
