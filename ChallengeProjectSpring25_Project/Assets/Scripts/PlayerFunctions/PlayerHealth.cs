using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float health;
    public AudioClip takeDamageSFX;
    public float healthRegenTime;
    private float regenTimeStart;
    private float maxHealth;
    public float regenSpeed;
    public AudioClip healSound;
    private bool dead = false;
    public CardDrawingController cdc;

    private void Awake()
    {
        maxHealth = health;
        healthBar.SetMaxHealth(maxHealth);
        regenTimeStart = healthRegenTime;
    }

    public bool IsAtMaxHealth()
    {
        return (health == maxHealth) ? true : false;
    }

    public void TakeDamage(int damage)
    {
        GetComponent<AudioSource>().PlayOneShot(takeDamageSFX);

        health -= damage;
        healthBar.SetHealth(health);
        healthRegenTime = regenTimeStart;

        //player death
        if (health <= 0 && !dead)
        {
            Debug.Log("player is dead ;(");
            PlayerDie();
            dead = true;
            //Destroy(gameObject);
        }
    }

    private void PlayerDie()
    {
        if (FindFirstObjectByType<GameLoopController>() != null)
        {
            //count loss and reload scene
            FindFirstObjectByType<GameLoopController>().CountALoss();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //Debug.Log("player is immobile ;(");
        //Debug.Log(FindFirstObjectByType<DeathManager>());
        //Debug.Log(FindFirstObjectByType<CardDrawingController>().GetCardList());
        FindFirstObjectByType<DeathManager>().DisplayDeathMenu(cdc.GetCardList());
    }


    public void HealDamage(int healing)
    {
        GetComponent<AudioSource>().PlayOneShot(healSound);
        health = Mathf.Clamp(health + healing, 0, maxHealth);
        healthBar.SetHealth(health);
    }

    private void Update()
    {
        //player health regen disabled for now, but can be brought back
/*        if (health < maxHealth)
        {
            healthRegenTime -= Time.deltaTime;

            if (healthRegenTime <= 0)
            {
                RegenHealth();
            }
        }

        if (health == maxHealth)
        {
            healthRegenTime = regenTimeStart;
        }*/
    }

    private void RegenHealth()
    {
        health += Time.deltaTime * regenSpeed;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthBar.SetHealth(health);
    }
}
