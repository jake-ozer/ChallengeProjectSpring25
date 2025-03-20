using UnityEngine;

public class ProjLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            //Debug.Log(other.gameObject.tag);
            Destroy(gameObject);
        }
    }
}
