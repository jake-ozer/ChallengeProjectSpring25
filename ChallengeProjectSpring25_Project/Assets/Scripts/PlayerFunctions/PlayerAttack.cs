using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public PlayerInput input;
    public float attackCooldown;
    public Animator animator;
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

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, attackRange))
            {
                if (hitinfo.collider.gameObject.tag == "Enemy")
                {
                    Debug.Log("enemy hit");
                    
                }
            }
        }

        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.yellow);
    }
}
