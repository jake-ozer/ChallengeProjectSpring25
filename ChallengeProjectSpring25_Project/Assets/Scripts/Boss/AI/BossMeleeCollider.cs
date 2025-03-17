using UnityEngine;

public class BossMeleeCollider : MonoBehaviour
{
    //you can put this script onto any trigger game object that needs to hurt the player
    public int damage;
    private bool active = true;
    public bool attacking = false;

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("eee");
        if (active && other.gameObject.GetComponent<PlayerHealth>() != null && attacking)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            active = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!active && other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            active = true;
        }
    }
}
