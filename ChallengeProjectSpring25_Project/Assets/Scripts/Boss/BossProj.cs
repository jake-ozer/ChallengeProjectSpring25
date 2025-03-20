using UnityEngine;

public class BossProj : MonoBehaviour
{
    private Transform player; 
    private float projTime;
    [SerializeField] public float timer;
    [SerializeField] public GameObject projectile;
    [SerializeField] public Transform projSpawn;
    [SerializeField] public float projSpeed;
    private UnityEngine.AI.NavMeshAgent bossMove;
    
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
            //Since player position may be a little high, set it -1 in y axis.
            Vector3 playerPos = player.transform.Find("PlayerCamera").position;
            playerPos.y += -2;
            transform.LookAt(playerPos);
            projTime = timer;
            GameObject bossProjectile = Instantiate(projectile, projSpawn.transform.position, projSpawn.transform.rotation) as GameObject;
            bossProjectile.SetActive(true);
            Rigidbody bossProjRigid = bossProjectile.GetComponent<Rigidbody>();
            bossProjRigid.AddForce(projSpawn.forward * projSpeed, ForceMode.Impulse);
            Destroy(bossProjectile, 5f);
        }
       
    }
}
