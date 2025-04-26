using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BossMelee : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDuration;
    [SerializeField] private float waitBeforeAttack;
    [SerializeField] private GameObject attackColliderObj;
    [SerializeField] private Animator anim;

    private Transform playerTransform;
    private Vector3 dirToPlayer;
    private float cooldownTimer;

    public AudioClip windupSound;
    public AudioClip attackSound;
    public BossButtCheckAttack buttCheck;

    private bool bossLockedOn;
    public float lockOnSpeed;

    private void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerMovement>().transform;
        cooldownTimer = 0;
        attackColliderObj.GetComponent<MeshRenderer>().enabled = false;
        if(attackColliderObj.transform.GetChild(0) != null) { attackColliderObj.transform.GetChild(0).gameObject.SetActive(false); }
    }

    private void Update()
    {
        dirToPlayer = playerTransform.position - transform.position;
        cooldownTimer -= Time.deltaTime;

        if (dirToPlayer.magnitude <= attackRange && cooldownTimer <= 0)
        {
            StopAllCoroutines();
            StartCoroutine("MeleeAttackSequence");
            cooldownTimer = attackCooldown;
        }

        if (bossLockedOn)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dirToPlayer);
            Vector3 targetEuler = targetRotation.eulerAngles;
            targetEuler.x = 0;
            targetEuler.z = 0;
            targetRotation = Quaternion.Euler(targetEuler);
           // Debug.Log(targetRotation.eulerAngles);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * lockOnSpeed);
        }
    }

    private IEnumerator MeleeAttackSequence()
    {
        bossLockedOn = true;

        anim.SetBool("CurrentlyInAttack", true);
        anim.SetTrigger("Windup");
        GetComponent<AudioSource>().PlayOneShot(windupSound);
        GetComponent<NavMeshAgent>().enabled = false;

        // Rotate toward player during windup phase
      

        // Attack
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDuration);

        // Cleanup
        anim.SetTrigger("RTI");
        attackColliderObj.GetComponent<MeshRenderer>().enabled = false;
        attackColliderObj.GetComponent<BossMeleeCollider>().attacking = false;
        attackColliderObj.transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<NavMeshAgent>().enabled = true;
        anim.SetBool("CurrentlyInAttack", false);
        attackColliderObj.GetComponent<BossMeleeCollider>().active = true;
        bossLockedOn = false;
        if(buttCheck != null)
        {
            buttCheck.ButtViewCheck();
        }
    }

    public void PlayAttackSound()
    {
        GetComponent<AudioSource>().PlayOneShot(attackSound);
        attackColliderObj.GetComponent<BossMeleeCollider>().attacking = true;
        attackColliderObj.transform.GetChild(0).gameObject.SetActive(true);
    }
}
