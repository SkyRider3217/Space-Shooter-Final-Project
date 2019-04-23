using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject damageExplosion;
    public int scoreValueDamage;
    public int scoreValueDeath;
    public GameObject shot;
    public Transform shotSpawnC;
    public Transform shotSpawnR1;
    public Transform shotSpawnR2;
    public Transform shotSpawnL1;
    public Transform shotSpawnL2;

    public float maxHealth;
    public float currentHealth;
    public float damage;
    public float fireRate1;
    public float fireRate2;
    public float fireRate3;
    public float delay1;
    public float delay2;
    public float delay3;

    private AudioSource audioSource;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire1", delay1, fireRate1);
        InvokeRepeating("Fire2", delay2, fireRate2);
        InvokeRepeating("Fire3", delay3, fireRate3);

        currentHealth = maxHealth;
    }

    void Fire1()
    {
        Instantiate(shot, shotSpawnC.position, shotSpawnC.rotation);
        audioSource.Play();
    }

    void Fire2()
    {
        Instantiate(shot, shotSpawnR1.position, shotSpawnR1.rotation);
        Instantiate(shot, shotSpawnL1.position, shotSpawnL1.rotation);
        audioSource.Play();
    }

    void Fire3()
    {
        Instantiate(shot, shotSpawnR2.position, shotSpawnR2.rotation);
        Instantiate(shot, shotSpawnL2.position, shotSpawnL2.rotation);
        audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(damageExplosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            other.gameObject.SetActive(false);
            Instantiate(damageExplosion, transform.position, transform.rotation);
            gameController.AddScore(scoreValueDamage);
            DamageBoss();
        }
    }

    void DamageBoss()
    {
        currentHealth -= damage;

        if (currentHealth == 0)
        {
            gameController.AddScore(scoreValueDeath);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
