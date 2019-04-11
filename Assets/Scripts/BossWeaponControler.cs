using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponControler : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawnR1;
    public Transform shotSpawnR2;
    public Transform shotSpawnR3;
    public Transform shotSpawnL1;
    public Transform shotSpawnL2;
    public Transform shotSpawnL3;
    public float spawnWait;
    public float fireRate1;
    public float fireRate2;
    public float delay1;
    public float delay2;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire1", delay1, fireRate1);
        InvokeRepeating("Fire2", delay2, fireRate2);
    }

    void Fire1()
    {
        Instantiate(shot, shotSpawnR1.position, shotSpawnR1.rotation);
        Instantiate(shot, shotSpawnR2.position, shotSpawnR2.rotation);
        Instantiate(shot, shotSpawnR3.position, shotSpawnR3.rotation);
        audioSource.Play();
    }

    void Fire2()
    {
        Instantiate(shot, shotSpawnL1.position, shotSpawnL1.rotation);
        Instantiate(shot, shotSpawnL2.position, shotSpawnL2.rotation);
        Instantiate(shot, shotSpawnL3.position, shotSpawnL3.rotation);
        audioSource.Play();
    }
    
}
