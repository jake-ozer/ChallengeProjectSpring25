using UnityEngine;

public class WhirlAttackHelper : MonoBehaviour
{
    public void CueAttack()
    {
        FindFirstObjectByType<BossButtCheckAttack>().CueAttack();
    }
}
