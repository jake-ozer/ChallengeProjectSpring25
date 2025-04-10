using UnityEngine;

public class AttackSoundHelper : MonoBehaviour
{
    public BossMelee bm;

    public void PlayAttackSound()
    {
        bm.PlayAttackSound();
    }
}
