using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public PlayerInput input;
    public float attackCooldown;
    public Animator animator;
    public LayerMask enemyLayer;
    public int playerDmg;
    private float timer;
    private bool attacking;
    private GameObject currentAttackTarget;

    private void Update()
    {
        timer -= Time.deltaTime;

        //hit enemy
        if (input.actions["Attack"].triggered && timer <= 0)
        {
            timer = attackCooldown;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("swordattack"))
            {
                animator.SetTrigger("reset");
            }
            animator.SetTrigger("attack");
        }

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * attackRange, Color.yellow);
    }

    //used by animation event from sword anim to detect when player is at climax of swing
    public void PlayerAttackSignaled()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, attackRange, enemyLayer))
        {
            if (hitinfo.collider.gameObject.GetComponent<BossHealth>() != null || hitinfo.collider.gameObject.GetComponent<MiniEnemy>() != null)
            {
                if (hitinfo.collider.gameObject.GetComponent<BossHealth>() != null)
                {
                    hitinfo.collider.gameObject.GetComponent<BossHealth>().TakeDamage(playerDmg);
                }
                else if (hitinfo.collider.gameObject.GetComponent<MiniEnemy>() != null)
                {
                    Destroy(hitinfo.collider.gameObject);
                }
            }
        }
    }

    public int GetDamage()
    {
        return playerDmg;
    }

    public void SetDamage(int damage)
    {
        Debug.Log("Change damage to: " + damage);
        playerDmg = damage;
    }
}
