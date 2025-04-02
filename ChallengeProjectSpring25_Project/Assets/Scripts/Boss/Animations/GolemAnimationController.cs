using UnityEngine;
using UnityEngine.AI;

public class GolemAnimationController : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent nmAgent;

    private void Update()
    {
        //changing state from idle <==> walking
        if(nmAgent.velocity.magnitude > 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }
}
