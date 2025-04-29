using UnityEngine;
using UnityEngine.AI;

public class KingBuffHandler : MonoBehaviour, IBoss
{
    private HurtPlayerOnContact hitbox;
    private Spawn minionSpawner;

    private NavMeshAgent agent;
    private NavMeshAgent miniAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitbox = transform.parent.Find("BossDamageBox").GetComponent<HurtPlayerOnContact>();
        minionSpawner = transform.parent.GetComponent<Spawn>();

        agent = transform.parent.GetComponent<NavMeshAgent>();
        //miniAgent = transform.parent.Find("MiniEnemy").GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lionBuff()
    {
        int newHitboxDamage = (int)(hitbox.damage * 1.2f);
        hitbox.damage = newHitboxDamage;

        minionSpawner.minionDamage = (int)(minionSpawner.minionDamage * 1.2f);
        //int newMiniDamage = (int)(mini.damage);
        //mini.damage = newMiniDamage;
    }

    public void bullBuff()
    {
        float newSpeed = agent.speed * 1.2f;
        agent.speed = newSpeed;

        minionSpawner.minionSpeed = minionSpawner.minionSpeed * 1.2f;
        //float newMiniSpeed = miniAgent.speed * 1.2f;
        // miniAgent.speed = newMiniSpeed;
    }

    public void eagleBuff()
    {

    }
}
