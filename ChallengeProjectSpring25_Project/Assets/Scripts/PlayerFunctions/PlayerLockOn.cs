using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLockOn : MonoBehaviour
{
    public LayerMask lockOnLayer;
    public float lockOnSpeed;
    public PlayerInput input;
    private PlayerCamera playerCam;
    private Quaternion lastRotation;
    private Quaternion lastPlayerRotation;
    public bool lockedOn = false;
    public Transform target { get; private set; }
    private bool resetXRot = false;

    private void Start()
    {
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
                resetXRot = true;
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
            Vector3 dir = Vector3.zero;
            if (target != null)
            {
               dir = target.position - transform.position;
            }
             
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            

            Quaternion playerTargetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

            playerCam.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lockOnSpeed);
            transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, playerTargetRotation, Time.deltaTime * lockOnSpeed);

            lastRotation = playerCam.gameObject.transform.rotation;
            lastPlayerRotation = transform.parent.rotation;
            //Debug.Log(lastRotation.eulerAngles);
        }
        else //remove lockon logic
        {
            playerCam.enabled = true;
            ResetXRot();
            transform.parent.localEulerAngles = new Vector3(0, transform.rotation.eulerAngles.y, 0);
        }

        //==================================================================================


        //visualization
        //Debug.DrawRay(transform.position, transform.forward * 10000f, Color.cyan);
    }

    private void ResetXRot()
    {
        if (resetXRot)
        {
            playerCam.SetXRot(lastRotation.eulerAngles.x);
            playerCam.SetYRot(lastPlayerRotation.eulerAngles.y);
            resetXRot = false;
        }
    }
}
