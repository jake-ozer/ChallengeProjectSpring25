using UnityEngine;
using UnityEngine.AI;

public class DefaultBossAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agent != null && agent.enabled)
        {
            agent.SetDestination(player.transform.position);
        }
    }
       
}
