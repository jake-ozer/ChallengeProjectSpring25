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
    [SerializeField] private float rotateSpeed = 5f; // NEW: rotation speed
    [SerializeField] private GameObject attackColliderObj;
    [SerializeField] private Animator anim;

    private Transform playerTransform;
    private Vector3 dirToPlayer;
    private float cooldownTimer;

    public AudioClip windupSound;
    public AudioClip attackSound;
    public BossButtCheckAttack buttCheck;

    private void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerMovement>().transform;
        cooldownTimer = 0;
        attackColliderObj.GetComponent<MeshRenderer>().enabled = false;
        attackColliderObj.transform.GetChild(0).gameObject.SetActive(false);
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
    }

    private IEnumerator MeleeAttackSequence()
    {
        anim.SetBool("CurrentlyInAttack", true);
        anim.SetTrigger("Windup");
        GetComponent<AudioSource>().PlayOneShot(windupSound);
        GetComponent<NavMeshAgent>().enabled = false;

        // Rotate toward player during windup phase
        float timer = 0f;
        while (timer < waitBeforeAttack)
        {
            Vector3 targetDir = (playerTransform.position - transform.position).normalized;
            targetDir.y = 0; // prevent vertical tilting
            if (targetDir != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Attack
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDuration);

        // Cleanup
        anim.SetTrigger("Idle");
        attackColliderObj.GetComponent<MeshRenderer>().enabled = false;
        attackColliderObj.GetComponent<BossMeleeCollider>().attacking = false;
        attackColliderObj.transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<NavMeshAgent>().enabled = true;
        anim.SetBool("CurrentlyInAttack", false);

        buttCheck.ButtViewCheck();
    }

    public void PlayAttackSound()
    {
        GetComponent<AudioSource>().PlayOneShot(attackSound);
        attackColliderObj.GetComponent<BossMeleeCollider>().attacking = true;
        attackColliderObj.transform.GetChild(0).gameObject.SetActive(true);
    }
}
