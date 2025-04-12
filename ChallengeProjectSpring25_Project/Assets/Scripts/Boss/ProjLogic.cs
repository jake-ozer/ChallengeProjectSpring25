using UnityEngine;

public class ProjLogic : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 2f; 

    private Transform player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform; 
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;

            if (directionToPlayer != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            Destroy(gameObject);
        }
    }
}
