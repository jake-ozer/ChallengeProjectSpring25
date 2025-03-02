using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float health;
    public AudioClip takeDamageSFX;
    private float maxHealth;

    private void Awake()
    {
        maxHealth = health;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        //GetComponent<AudioSource>().PlayOneShot(takeDamageSFX);

        health -= damage;
        healthBar.SetHealth(health);

        //player death
        if (health <= 0)
        {
            Debug.Log("boss is dead :)");
            FindFirstObjectByType<PlayerLockOn>().lockedOn = false;
            Destroy(gameObject);
            //additional logic will go here when player dies
        }
    }
}
