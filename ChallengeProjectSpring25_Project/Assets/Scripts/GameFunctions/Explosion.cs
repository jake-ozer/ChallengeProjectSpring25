using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage = 5;
    public float duration = 5f;
    
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0f)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

    }
}
