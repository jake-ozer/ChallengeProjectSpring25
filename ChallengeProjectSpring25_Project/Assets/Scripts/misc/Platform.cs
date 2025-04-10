using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            //Debug.Log("player is on me");
            FindFirstObjectByType<PlatformController>().SetPlatformChange(transform.root.gameObject, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("player is off me");
        FindFirstObjectByType<PlatformController>().SetPlatformChange(transform.root.gameObject, -1);
    }
}
