using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float health;
    public AudioClip takeDamageSFX;
    private float maxHealth;
    private bool isInvincible = false;

    private void Awake()
    {
        maxHealth = health;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        //GetComponent<AudioSource>().PlayOneShot(takeDamageSFX);

        //health -= damage;
        //healthBar.SetHealth(health);

        if (isInvincible == true)
        {
            damage = 0;
        }
        else if (isInvincible == false)
        {
            health -= damage;
            healthBar.SetHealth(health);
        }
        //boss death
        if (health <= 0)
        {
            Debug.Log("boss is dead :)");
            FindFirstObjectByType<PlayerLockOn>().lockedOn = false;
            Destroy(gameObject);
            BossDie();
        }
    }

    private void BossDie()
    {
        if (FindFirstObjectByType<GameLoopController>() != null)
        {
            //count loss and reload scene
            FindFirstObjectByType<GameLoopController>().CountAWin();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
