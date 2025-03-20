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

        if(input.actions["Attack"].triggered && timer <= 0)
        {
            timer = attackCooldown;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("swordattack"))
            {
                animator.SetTrigger("reset");
            }
            animator.SetTrigger("attack");

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, attackRange, enemyLayer))
            {
                if (hitinfo.collider.gameObject.GetComponent<BossHealth>() != null || hitinfo.collider.gameObject.GetComponent<MiniEnemy>() != null)
                {
                    //Debug.Log("enemy hit");
                    //hitinfo.collider.gameObject.GetComponent<BossHealth>().TakeDamage(playerDmg);
                    attacking = true;
                    currentAttackTarget = hitinfo.collider.gameObject;
                }
            }
            else
            {
                attacking = false;
            }
        }

        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.yellow);
    }

    //used by animation event from sword anim to detect when player is at climax of swing
    public void PlayerAttackSignaled()
    {
        if (attacking)
        {
            if(currentAttackTarget.GetComponent<BossHealth>() != null)
            {
                currentAttackTarget.GetComponent<BossHealth>().TakeDamage(playerDmg);
            }
            if(currentAttackTarget.GetComponent<MiniEnemy>() != null)
            {
                Destroy(currentAttackTarget);
            }
            
        }
    }
}
