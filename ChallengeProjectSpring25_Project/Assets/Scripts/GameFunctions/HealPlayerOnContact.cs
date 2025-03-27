using UnityEngine;

public class HealPlayerOnContact : MonoBehaviour
{
    //you can put this script onto any trigger game object that needs to heal the player
    public int healing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null && !other.gameObject.GetComponent<PlayerHealth>().IsAtMaxHealth())
        {
            other.gameObject.GetComponent<PlayerHealth>().HealDamage(healing);
            Destroy(gameObject);
        }
    }
}
