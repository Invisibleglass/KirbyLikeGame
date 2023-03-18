using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    AudioSourceManager asm;

    public float lifetime;

    public AudioClip enemyHurtSound;

    [HideInInspector]
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        asm = GetComponent<AudioSourceManager>();

        if (lifetime <= 0)
            lifetime = 2.0f;

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        /*if (other.gameObject.CompareTag("Powerup") || (collision.gameObject.CompareTag("Player")|| other.gameObject.CompareTag("Collectible"))
        {
            //Do nothing
        }
        else
        {
            Destroy(gameObject);
        }*/
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
            GameManager.instance.playerInstance.GetComponent<AudioSourceManager>().PlayOneShot(enemyHurtSound, false);
            Destroy(gameObject);
        }
    }
}
