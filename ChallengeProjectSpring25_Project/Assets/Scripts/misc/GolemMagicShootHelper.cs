using UnityEngine;

public class GolemMagicShootHelper : MonoBehaviour 
{
    public void CueShoot()
    {
        FindFirstObjectByType<BossProj>().CueShoot();
    }
    
}
