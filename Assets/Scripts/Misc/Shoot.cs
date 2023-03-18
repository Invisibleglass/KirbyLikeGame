using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;
    AudioSourceManager asm;

    public float projectileSpeed;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public Projectile projectilePrefab;

    public AudioClip spitSound;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        asm = GetComponent<AudioSourceManager>();

        if (projectileSpeed <= 0)
            projectileSpeed = 7.0f;
        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.Log("Please setup default values on " + gameObject.name);
    }

    public void Fire()
    {
        if (!sr.flipX)
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.speed = projectileSpeed;
        }
        else
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.speed = -projectileSpeed;
            curProjectile.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (asm)
            asm.PlayOneShot(spitSound, false);
    }
}
