using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject miniEnemy;
    [SerializeField] GameObject spawnLoc;
    [SerializeField] int spawnCount;
    [SerializeField] float spawnTime;
    public float time;
    private float radius = 5f;
    public AudioClip spawnSound;
    public Animator anim;
    public SpawnAllChildParticles spacp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 offseft = new Vector3(Random.Range(-radius, radius), Random.Range(-radius,radius), Random.Range(-radius,radius));
        time += Time.deltaTime;//timer for spawning of the magicians

        if (time >= spawnTime)
        {
            StartCoroutine("SpawnSequence");
            time = 0;
        }
    }

    private IEnumerator SpawnSequence()
    {
        anim.SetTrigger("SpawnMinion");
        GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(1.2f);
        GetComponent<NavMeshAgent>().enabled = true;
        anim.SetTrigger("RTI");
    }

    public void SpawnMinions()
    {
        GetComponent<AudioSource>().PlayOneShot(spawnSound);

        //spawn a particle effect above the boss for cool effect
        spacp.Spawn();

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 offset = new Vector3(Random.insideUnitCircle.y * radius, .10f);//found that calling insideUnitSphere is supposed to spawn a cirlce radius
            offset.z = .5f;
            Vector3 spawn = spawnLoc.transform.position + offset;
            //Debug.Log("offset: " + offset);
            //Debug.Log("boss pos: " + theBoss.transform.position);
            //Debug.Log("spawn: " + spawn);
            Vector3 testOff = new Vector3(5, .1f, 5);
            //Debug.Log(Random.insideUnitSphere);
            var miniEnemy = Instantiate(this.miniEnemy, spawn, Quaternion.identity);
            miniEnemy.transform.parent = spawnLoc.transform;
        }
    }
}
