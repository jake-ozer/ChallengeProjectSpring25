using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class DefaultBossAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private NavMeshAgent agent;

    [SerializeField] private float fastspeed = 20f;
    [SerializeField] private float slowspeed = 0f;
    [SerializeField] private float normalspeed = 5f;
    [SerializeField] private float triggerDistance = 5f;
    private float slowtimer;
    private float fasttimer;
    private float cooldown = 5f;
    private bool isSlow;
    private bool isFast;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    private void Update()
    {
        if (!agent || !agent.enabled) return;

        agent.SetDestination(player.transform.position);

        if (isSlow)
        {
            agent.transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(player.transform.position - agent.transform.position), agent.transform.rotation, slowtimer/cooldown);

            slowtimer -= Time.deltaTime;
            if (slowtimer < 0f)
            {
                isSlow = false;
                isFast = true;
                fasttimer = cooldown;
                agent.speed = fastspeed;
            }
        }

        else if (isFast)
        {
            fasttimer -= Time.deltaTime;
            if (fasttimer < 0f)
            {
                isFast = false;
                agent.speed = normalspeed;
            }
        }
        
        else if (Vector3.Distance(player.transform.position, agent.transform.position) > triggerDistance)
        {
            isSlow = true;
            slowtimer = cooldown;
            agent.speed = slowspeed;
        }
    }
       
}
