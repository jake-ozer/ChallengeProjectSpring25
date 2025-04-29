using UnityEngine;
using UnityEngine.AI;

public class DevilBuffHandler : MonoBehaviour, IBoss
{

    private HurtPlayerOnContact bossHitBox;
    private BossMeleeCollider meleeAttack;
    private HurtPlayerOnContact buttCheck;

    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossHitBox = transform.parent.Find("BossDamageBox").GetComponent<HurtPlayerOnContact>();
        meleeAttack = transform.parent.Find("AttackCollider").GetComponent<BossMeleeCollider>();
        buttCheck = transform.parent.Find("WhirlAttackCollider").GetComponent<HurtPlayerOnContact>();

        agent = transform.parent.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lionBuff()
    {
        int newBossDamage = (int) (bossHitBox.damage * 1.2);
        bossHitBox.damage = newBossDamage;

        int newMeleeDamage = (int) (meleeAttack.damage * 1.2);
        meleeAttack.damage = newMeleeDamage;

        int newButtDamage = (int)(buttCheck.damage * 1.2);
        buttCheck.damage = newButtDamage;
    }

    public void bullBuff()
    {
        double newSpeed = agent.speed * 1.2;
        agent.speed = (float)newSpeed;
    }

    public void eagleBuff()
    {

    }
}
