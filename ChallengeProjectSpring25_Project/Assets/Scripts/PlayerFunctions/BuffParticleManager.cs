using UnityEngine;

public class BuffParticleManager : MonoBehaviour
{
    private ParticleSystem particle;
    [SerializeField]
    private Color eagle, bull, lion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableParticles(int index)
    {
        if (index != 0)
            particle.Play();
        switch(index)
        {
            case 1:
                particle.GetComponent<ParticleSystemRenderer>().material.color = eagle;
                break;
            case 2:
                particle.GetComponent<ParticleSystemRenderer>().material.color = lion;
                break;
            case 3:
                particle.GetComponent<ParticleSystemRenderer>().material.color = bull;
                break;
        }


    }
}
