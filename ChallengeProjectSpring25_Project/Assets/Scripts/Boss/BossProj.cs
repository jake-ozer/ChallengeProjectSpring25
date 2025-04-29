using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossProj : MonoBehaviour
{
    private Transform player; 
    private float projTime;
    [SerializeField] public float timer;
    [SerializeField] public GameObject projectile;
    [SerializeField] public Transform projSpawn;
    [SerializeField] public float projSpeed;
    private UnityEngine.AI.NavMeshAgent bossMove;
    public Animator anim;
    private bool lookAtPlayer;
    [SerializeField] private float attackDuration;
    [SerializeField] private float waitBeforeAttack;
    public AudioClip shootSound;
    public int numProjectiles;
    public float timeBetweenProj;
    public float projLifetime;
    private bool shootLock;

    void Start()
    {
        projTime = timer;
        player = FindFirstObjectByType<PlayerHealth>().gameObject.transform;
        bossMove = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
    }
    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {       
        //Need logic handling, for making sure to fire.
        //If navmeshagent is not active, then we can fire, otherwise it is in shockwave move.
        if (bossMove.enabled == true)
        {
            projTime -= Time.deltaTime;
            if (projTime > 0) { return; }
            lookAtPlayer = true;
            StartCoroutine("ShootRoutine");
            projTime = timer;


           
            
        }

        if (lookAtPlayer == true)
        {
            Vector3 playerPos = player.transform.Find("PlayerCamera").position;
            playerPos.y += -2;
            //transform.LookAt(playerPos);

            Vector3 direction = playerPos - transform.position;
            direction.y = 0; // eliminate vertical difference
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;
            }
        }

    }

    private IEnumerator ShootRoutine()
    {

        //signal to player that attack is coming
        anim.SetBool("CurrentlyInAttack", true);
        //anim.SetTrigger("Windup");
        GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(waitBeforeAttack);
        //attack
        anim.SetTrigger("Magic");
        yield return new WaitUntil(() => shootLock == true);

        //Since player position may be a little high, set it -1 in y axis.
        Vector3 playerPos = player.transform.Find("PlayerCamera").position;
        playerPos.y += -0.25f;
        transform.LookAt(playerPos);

        for (int i = 0; i < numProjectiles; i++)
        {
            Vector3 dirToPlayer = (playerPos - projSpawn.transform.position);
            GameObject bossProjectile = Instantiate(projectile, projSpawn.transform.position, Quaternion.LookRotation(dirToPlayer.normalized)) as GameObject;
            bossProjectile.SetActive(true);
            //Rigidbody bossProjRigid = bossProjectile.GetComponent<Rigidbody>();
            //bossProjectile.GetComponent<ProjLogic>().dir = dirToPlayer;
            bossProjectile.GetComponent<ProjLogic>().speed = projSpeed;
            //bossProjRigid.AddForce(dirToPlayer * projSpeed, ForceMode.Impulse);
            GetComponent<AudioSource>().PlayOneShot(shootSound);

            Destroy(bossProjectile, projLifetime);

            yield return new WaitForSeconds(timeBetweenProj);
        }


        shootLock = false;

        //attackColliderObj.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        //clean up
        anim.SetTrigger("RTI");
        //Debug.Log("idle called");

        //attackColliderObj.SetActive(false);
        GetComponent<NavMeshAgent>().enabled = true;
        anim.SetBool("CurrentlyInAttack", false);
    }

    public void CueShoot()
    {
        shootLock = true;
    }
}
