using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLockOn : MonoBehaviour
{
    public LayerMask lockOnLayer;
    public float lockOnSpeed;
    private PlayerInput input;
    private PlayerCamera playerCam;
    private Quaternion lastRotation;
    public bool lockedOn { get; private set; } = false;
    public Transform target { get; private set; }

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        playerCam = GetComponent<PlayerCamera>();
    }

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, lockOnLayer))
        {
            if (input.actions["LockOn"].triggered && !lockedOn)
            {
                //start lockon
                lockedOn = true;
                target = hit.transform;
            }
            else if (input.actions["LockOn"].triggered && lockedOn)
            {
                //end lockon
                lastRotation = transform.rotation;
                lockedOn = false;
            }
        }

        //==================================================================================

        //lockon logic
        if (lockedOn)
        {
            playerCam.enabled = false;
            Vector3 dir = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            playerCam.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lockOnSpeed);
            transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, targetRotation, Time.deltaTime * lockOnSpeed);
        }
        else //remove lockon logic
        {
            playerCam.enabled = true;
            transform.rotation = lastRotation;
        }

        //==================================================================================


        //visualization
        Debug.DrawRay(transform.position, transform.forward * 10000f, Color.cyan);
    }


}
