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
                if (hitinfo.collider.gameObject.GetComponent<BossHealth>() != null)
                {
                    //Debug.Log("enemy hit");
                    hitinfo.collider.gameObject.GetComponent<BossHealth>().TakeDamage(playerDmg);
                }


            }
        }

        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.yellow);
    }
}
