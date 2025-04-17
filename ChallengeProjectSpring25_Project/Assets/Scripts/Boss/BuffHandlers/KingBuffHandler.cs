using UnityEngine;
using UnityEngine.AI;

public class KingBuffHandler : MonoBehaviour, IBoss
{
    private HurtPlayerOnContact hitbox;
    private MiniEnemy mini;

    private NavMeshAgent agent;
    private NavMeshAgent miniAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitbox = transform.parent.Find("BossDamageBox").GetComponent<HurtPlayerOnContact>();
        mini = transform.parent.Find("MiniEnemy").GetComponent<MiniEnemy>();

        agent = transform.parent.GetComponent<NavMeshAgent>();
        miniAgent = transform.parent.Find("MiniEnemy").GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lionBuff()
    {
        int newHitboxDamage = (int)(hitbox.damage * 1.2f);
        hitbox.damage = newHitboxDamage;

        int newMiniDamage = (int)(mini.damage);
        mini.damage = newMiniDamage;
    }

    public void bullBuff()
    {
        float newSpeed = agent.speed * 1.2f;
        agent.speed = newSpeed;

        float newMiniSpeed = miniAgent.speed * 1.2f;
        miniAgent.speed = newMiniSpeed;
    }

    public void eagleBuff()
    {

    }
}
