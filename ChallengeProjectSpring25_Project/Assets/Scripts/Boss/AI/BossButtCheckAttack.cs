using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossButtCheckAttack : MonoBehaviour
{
    public float angle;
    public float radius;
    public LayerMask targetMask;

    private bool doButtCheck = true;

    [SerializeField] private float attackDuration;
    [SerializeField] private float waitBeforeAttack;

    public GameObject buttAttackCollider;
    public NavMeshAgent navAgent;
    public Animator anim;


    private void Start()
    {
        buttAttackCollider.SetActive(false);
    }

    private void Update()
    {
        if (doButtCheck)
        {
            //ButtViewCheck();
        }
    }

    public void ButtViewCheck()
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
                StartCoroutine("WhirlAttack");
            }
        }
    }

    private IEnumerator WhirlAttack()
    {
        //signal to player that attack is coming
        //anim.SetBool("CurrentlyInAttack", true);
        //anim.SetTrigger("Windup");
        //GetComponent<AudioSource>().PlayOneShot(windupSound);
        //GetComponent<NavMeshAgent>().enabled = false;
        navAgent.enabled = false;
        anim.SetTrigger("WhirlWindup");

        yield return new WaitForSeconds(waitBeforeAttack);
        //attack
        
        anim.SetTrigger("WhirlAttack");
        //anim.SetTrigger("Attack");
        //attackColliderObj.GetComponent<MeshRenderer>().enabled = true;
        //attackColliderObj.GetComponent<BossMeleeCollider>().attacking = true;

        //attackColliderObj.SetActive(true);
        yield return new WaitForSeconds(attackDuration);

        //clean up
        buttAttackCollider.SetActive(false);
        navAgent.enabled = true;
        anim.SetTrigger("RTI");
        //anim.SetTrigger("Idle");
        //anim.SetTrigger("Idle");
        //Debug.Log("idle called");
        //attackColliderObj.GetComponent<MeshRenderer>().enabled = false;
        //attackColliderObj.GetComponent<BossMeleeCollider>().attacking = false;
        //attackColliderObj.SetActive(false);
        //GetComponent<NavMeshAgent>().enabled = true;
        //anim.SetBool("CurrentlyInAttack", false);
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

    //called by anim event to initiate attack happens
    public void CueAttack()
    {
        buttAttackCollider.SetActive(true);
    }

}
