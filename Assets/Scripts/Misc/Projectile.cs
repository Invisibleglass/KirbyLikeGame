using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    
    public float lifetime;

    [HideInInspector]
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0)
            lifetime = 2.0f;

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTrigger2D(Collision2D collision)
    {
        if /*(other.gameObject.CompareTag("Powerup") ||*/ (collision.gameObject.CompareTag("Player")/*|| other.gameObject.CompareTag("Collectible")*/)
        {
            //Do nothing
        }
        else
            Destroy(gameObject);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
