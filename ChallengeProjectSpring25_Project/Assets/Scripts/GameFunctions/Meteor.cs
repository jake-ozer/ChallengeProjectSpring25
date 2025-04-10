using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float timeTillFall;
    public int damage;

    private void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        Invoke("StartFalling", timeTillFall);
    }

    private void StartFalling()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
