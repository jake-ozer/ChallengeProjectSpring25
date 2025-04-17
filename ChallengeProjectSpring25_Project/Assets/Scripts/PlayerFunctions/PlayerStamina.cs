using System.Collections;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public StaminaBar stamBar;
    public float totalStamina;
    public float regenSpeed;
    private float curStam;
    public float stamRegenTime;
    private float regenTimeStart;
    public AudioClip noStaminaSound;
    public GameObject noStamIcon;

    private void Awake()
    {
        noStamIcon.SetActive(false);
        curStam = totalStamina;
        stamBar.SetMaxStamina(curStam);
        regenTimeStart = stamRegenTime;
    }

/*    public bool IsAtMaxHealth()
    {
        return (health == maxHealth) ? true : false;
    }*/

    public bool HasEnoughStamina(float amt)
    {
        if(curStam - amt < 0)
        {
            GetComponent<AudioSource>().PlayOneShot(noStaminaSound);
            StopAllCoroutines();
            StartCoroutine("FlashNoStamIcon");
        }
        return (curStam - amt >= 0);
    }

    public void ConsumeStam(float amt)
    {
        stamRegenTime = regenTimeStart;
        curStam -= amt;
        curStam = Mathf.Clamp(curStam, 0, totalStamina);
        stamBar.SetStamina(curStam);

       /* health -= damage;
        healthBar.SetHealth(health);
        healthRegenTime = regenTimeStart;

        //player death
        if (health <= 0)
        {
            Debug.Log("player is dead ;(");
            Destroy(gameObject);
        }*/
    }

   /* public void HealDamage(int healing)
    {
        health = Mathf.Clamp(health + healing, 0, maxHealth);
        healthBar.SetHealth(health);
    }*/

    private void Update()
    {
       
        if (curStam < totalStamina)
        {
            stamRegenTime -= Time.deltaTime;

            if (stamRegenTime <= 0)
            {
                RegenStamina();
            }
        }

        if (curStam == totalStamina)
        {
            stamRegenTime = regenTimeStart;
        }
    }

    private void RegenStamina()
    {
        curStam += Time.deltaTime * regenSpeed;
        curStam = Mathf.Clamp(curStam, 0, totalStamina);

        stamBar.SetStamina(curStam);
    }

    private IEnumerator FlashNoStamIcon()
    {
        noStamIcon.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        noStamIcon.SetActive(false);
    }
}
