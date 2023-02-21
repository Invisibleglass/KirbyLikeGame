using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swingbeam : MonoBehaviour
{
    SpriteRenderer sr;

    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public Beam beamPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
        if (!spawnPointLeft || !spawnPointRight || !beamPrefab)
            Debug.Log("Please setup default values on " + gameObject.name);
    }

    public void Fire()
    {
        if (!sr.flipX)
        {
            Beam curProjectile = Instantiate(beamPrefab, spawnPointRight.position, spawnPointRight.rotation);
        }
        else
        {
            Beam curProjectile = Instantiate(beamPrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
