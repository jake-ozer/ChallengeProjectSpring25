using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerGroundCheck : MonoBehaviour
{
    public LayerMask floorLayer;
    public PlayerMovement pm;
    public float groundCheckLength;

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hitinfo, groundCheckLength, floorLayer))
        {
            pm.grounded = true;
        }
        else
        {
            pm.grounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckLength);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if((floorLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            //pm.grounded = true;
            Debug.Log("touching ground");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((floorLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            //pm.grounded = false;
            Debug.Log("LEFT ground");
        }
    }*/
}
