using UnityEngine;

public class SoundPhaseController : MonoBehaviour
{
    public PlayerAttack pa;
    public AudioSource musicSource;
    public AudioSource cardDrawingMusicSource;

    private void Start()
    {
        Phase1();
    }

    private void Phase1()
    {
        pa.enabled = false;
        musicSource.enabled = false;
        cardDrawingMusicSource.enabled = true;
    }

    public void Phase2()
    {
        pa.enabled = true;
        musicSource.enabled = true;
        cardDrawingMusicSource.enabled = false;
    }
}
