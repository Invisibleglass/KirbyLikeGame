using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swingbeam : MonoBehaviour
{
    SpriteRenderer sr;

    public GameObject beam;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;
    public float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (!spawnPointLeft || !spawnPointRight || !beam)
            Debug.Log("Please setup default values on " + gameObject.name);

    }

    public void Swing()
    {
        if (!sr.flipX)
        {
            beam.transform.position = spawnPointRight.position;
            beam.transform.rotation = spawnPointRight.rotation;
            beam.SetActive(true);
            beam.GetComponent<Animator>().Play("SwipeRight");
        }
        else
        {
            beam.transform.position = spawnPointLeft.position;
            beam.transform.rotation = spawnPointLeft.rotation;
            beam.SetActive(true);
            beam.GetComponent<Animator>().Play("SwipeLeft");
        }
    }
    public void StopSwing()
    {
        beam.SetActive(false);
    }
}
