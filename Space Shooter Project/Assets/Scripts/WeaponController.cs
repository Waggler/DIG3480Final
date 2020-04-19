using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform shotSpawn;
    public GameObject shot;
    //private Transform playerTransform;
    public float fireRate;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        InvokeRepeating("Fire", delay, fireRate);
    }

    void Update()
    {
        /*if (playerTransform == null)
        {
            CancelInvoke();
        }*/
    
    }

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
