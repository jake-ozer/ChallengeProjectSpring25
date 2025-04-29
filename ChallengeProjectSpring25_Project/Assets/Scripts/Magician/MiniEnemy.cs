using UnityEngine;

public class MiniEnemy : MonoBehaviour
{
    public int damage;
    public GameObject minionDeathEffect;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("triggered");
        if ( other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        //spawn effect just a little lower
        Vector3 spawn = new Vector3(transform.position.x, transform.position.y-0.5f, transform.position.z);
        GameObject effect = Instantiate(minionDeathEffect, spawn, Quaternion.identity);
        Destroy(effect, 5f);
    }
}
