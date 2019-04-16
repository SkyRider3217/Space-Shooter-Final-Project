using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyBoss : MonoBehaviour
{
    public GameObject explosionDamage;
    public GameObject bossExplosion;
    public GameObject playerExplosion;
    public int scoreValueDamage;
    public int scoreValueDeath;

    public int health;
    public Slider healthBar;

    private GameController gameController;
    private bool bossDeath;

    void Start()
    {
        bossDeath = false;
        healthBar.value = health;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        gameController.AddScore(scoreValueDamage);
        Instantiate(explosionDamage, other.transform.position, other.transform.rotation);
        Destroy(other.gameObject);

        if (health == 0)
        {
            bossDeath = true;
            Instantiate(bossExplosion, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
            gameController.AddScore(scoreValueDeath);
        }
        
    }

}
