using UnityEngine;
using UnityEngine.AI;

public class MageBuffHandler : MonoBehaviour, IBoss
{
    private HurtPlayerOnContact proj;
    private ShockWaveCollider wave;

    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        proj = transform.parent.Find("Projectile").GetComponent<HurtPlayerOnContact>();
        wave = transform.parent.Find("Shockwave").GetChild(0).GetComponent<ShockWaveCollider>();

        agent = transform.parent.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lionBuff()
    {
        int newProjDamage = (int)(proj.damage * 1.2f);
        proj.damage = newProjDamage;

        int newWaveDamage = (int)(wave.damage * 1.2f);
        wave.damage = newWaveDamage;

    }

    public void bullBuff()
    {
        float newSpeed = agent.speed * 1.2f;
        agent.speed = newSpeed;
    }

    public void eagleBuff()
    {

    }
}
