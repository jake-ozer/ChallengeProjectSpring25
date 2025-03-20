using UnityEngine;

public class SignalPlayerAttack : MonoBehaviour
{
    public PlayerAttack pa;

    public void SignalPlayerHit()
    {
        pa.PlayerAttackSignaled();
    }
}
