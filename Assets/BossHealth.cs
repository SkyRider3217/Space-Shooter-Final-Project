using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int health;
    public Slider healthBar;

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue1;
    public int scoreValue2;

    private GameController gameController;

    void Update()
    {
        healthBar.value = health;
        UpdateHealth();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.tag == "PlayerWeapon")
        {
            Destroy(other.gameObject);
            UpdateHealth();

            if (health == 0)
            {
                gameController.AddScore(scoreValue2);
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

    }

    void UpdateHealth()
    {
        health -= 1;
    }

}
