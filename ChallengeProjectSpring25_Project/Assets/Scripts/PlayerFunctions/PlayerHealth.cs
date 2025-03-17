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

    private void Awake()
    {
        maxHealth = health;
        healthBar.SetMaxHealth(maxHealth);
        regenTimeStart = healthRegenTime;
    }

    public void TakeDamage(int damage)
    {
        //GetComponent<AudioSource>().PlayOneShot(takeDamageSFX);

        health -= damage;
        healthBar.SetHealth(health);
        healthRegenTime = regenTimeStart;

        //player death
        if (health <= 0)
        {
            Debug.Log("player is dead ;(");
            //additional logic will go here when player dies
        }
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
