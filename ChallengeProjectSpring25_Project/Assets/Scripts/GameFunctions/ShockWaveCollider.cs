using UnityEngine;

public class ShockWaveCollider : MonoBehaviour
{
    public int damage;
    public float heightClearance;
    private bool active = true;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("triggered");
        if (active && other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            if(other.gameObject.transform.position.y < heightClearance)
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
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

    private void OnEnable()
    {
        active = true;
    }
}
