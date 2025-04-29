using UnityEngine;

public class GolemSpawnHelper : MonoBehaviour
{
    public void CueSpawn()
    {
        FindFirstObjectByType<Spawn>().SpawnMinions();
    }
}
