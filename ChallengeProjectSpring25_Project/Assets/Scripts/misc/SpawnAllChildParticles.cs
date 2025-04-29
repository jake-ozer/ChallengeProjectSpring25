using UnityEngine;

public class SpawnAllChildParticles : MonoBehaviour
{
    public void Spawn()
    {
        foreach(ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
    }
}
