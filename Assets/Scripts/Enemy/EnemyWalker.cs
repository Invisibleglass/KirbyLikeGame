using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWalker : Enemy
{
    AudioSourceManager asm;
    Rigidbody2D rb;

    public AudioClip enemyDeathSound;

    public float speed;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        {
            rb = GetComponent<Rigidbody2D>();
            asm = GetComponent<AudioSourceManager>();
        }

        if (speed <= 0)
            speed = 1.5f;

    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curClips[0].clip.name == "Idle")
        {
            if(!sr.flipX)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }
        if (curClips[0].clip.name == "Death")
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Barrier"))
            sr.flipX = !sr.flipX;
        if (collision.CompareTag("beam"))
        {
            Death();
            GameManager.instance.playerInstance.GetComponent<AudioSourceManager>().PlayOneShot(enemyDeathSound, false);
        }
    }


    public void DestroyMyself()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
