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

    private void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerMovement>().transform;
        cooldownTimer = 0;
        attackColliderObj.GetComponent<MeshRenderer>().enabled = false;
        attackColliderObj.transform.GetChild(0).gameObject.SetActive(false);
        //attackColliderObj.SetActive(false);
    }

    private void Update()
    {
        dirToPlayer = playerTransform.position - transform.position;
        cooldownTimer -= Time.deltaTime;
        //Debug.Log(dirToPlayer + " | " + dirToPlayer.magnitude);
        if(dirToPlayer.magnitude <= attackRange && cooldownTimer <= 0)
        {
            StopAllCoroutines();
            StartCoroutine("MeleeAttackSequence");
            cooldownTimer = attackCooldown;
        }
    }

    private IEnumerator MeleeAttackSequence()
    {
        //signal to player that attack is coming
        anim.SetBool("CurrentlyInAttack", true);
        anim.SetTrigger("Windup");
        GetComponent<AudioSource>().PlayOneShot(windupSound);
        GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(waitBeforeAttack);
        //attack
        anim.SetTrigger("Attack");
        GetComponent<AudioSource>().PlayOneShot(attackSound);
        //attackColliderObj.GetComponent<MeshRenderer>().enabled = true;
        attackColliderObj.GetComponent<BossMeleeCollider>().attacking = true;
        attackColliderObj.transform.GetChild(0).gameObject.SetActive(true);

        //attackColliderObj.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        
        //clean up
        anim.SetTrigger("Idle");
        //Debug.Log("idle called");
        attackColliderObj.GetComponent<MeshRenderer>().enabled = false;
        attackColliderObj.GetComponent<BossMeleeCollider>().attacking = false;
        attackColliderObj.transform.GetChild(0).gameObject.SetActive(false);
        //attackColliderObj.SetActive(false);
        GetComponent<NavMeshAgent>().enabled = true;
        anim.SetBool("CurrentlyInAttack", false);

        //check the butt in case of player (then attack on its own logic)
        buttCheck.ButtViewCheck();
    }

    public void PlayAttackSound()
    {
        //GetComponent<AudioSource>().PlayOneShot(attackSound);
    }
}
