using System;
using Unity.VisualScripting;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float timeTillFall;
    public int damage;
    public LayerMask floorLayer;

    public GameObject explosion;
    
    private void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        Invoke("StartFalling", timeTillFall);

    }

    private void StartFalling()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // if(other.GetComponent<PlayerHealth>() != null)
        // {
        //     other.GetComponent<PlayerHealth>().TakeDamage(damage);
        //     Destroy(gameObject);
        // }

        if ((floorLayer & (1 << other.gameObject.layer)) != 0)
        {
            Instantiate(explosion, transform);
            Destroy(gameObject);
        }
    }

}
