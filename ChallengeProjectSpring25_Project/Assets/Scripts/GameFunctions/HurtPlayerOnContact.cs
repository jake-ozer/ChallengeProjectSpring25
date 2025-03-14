using UnityEngine;

public class HurtPlayerOnContact : MonoBehaviour
{
    //you can put this script onto any trigger game object that needs to hurt the player
    public int damage;
    private bool active = true;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("triggered");
        if(active && other.gameObject.GetComponent<PlayerHealth>() != null)
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
