using UnityEngine;

public class AttackSoundHelper : MonoBehaviour
{
    public BossMelee bm;

    public void PlayAttackSound()
    {
        if(bm != null)
        {
            bm.PlayAttackSound();
        }
        
    }

    public void DoNothing()
    {
        //do nothing
    }
}
