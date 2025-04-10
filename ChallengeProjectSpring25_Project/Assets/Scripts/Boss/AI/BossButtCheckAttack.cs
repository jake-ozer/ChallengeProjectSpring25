using UnityEngine;

public class BossButtCheckAttack : MonoBehaviour
{
    public float angle;
    public float radius;
    public LayerMask targetMask;

    private bool doButtCheck = true;

    private void Update()
    {
        if (doButtCheck)
        {
            ButtViewCheck();
        }
    }

    private void ButtViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 targetDir = (target.position - transform.position).normalized;

            if (Vector3.Angle(-transform.forward, targetDir) > angle / 2)
            {
                //butt sees player, initiate attack
                Debug.Log("butt check attack");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);       

        Vector3 leftLimit = Quaternion.Euler(0, -angle / 2, 0) * -transform.forward;
        Vector3 rightLimit = Quaternion.Euler(0, angle / 2, 0) * -transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftLimit * radius);
        Gizmos.DrawLine(transform.position, transform.position + rightLimit * radius);
    }

}
