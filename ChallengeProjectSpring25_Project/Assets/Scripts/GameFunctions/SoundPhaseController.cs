using UnityEngine;

public class SoundPhaseController : MonoBehaviour
{
    public PlayerAttack pa;
    public AudioSource musicSource;
    public AudioSource cardDrawingMusicSource;
    public AudioClip devilSong;
    public AudioClip magicianSong;
    public AudioClip emperorSong;

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

    //pass in which song we play
    public void Phase2(Card card)
    {
        switch (card.cardName)
        {
            case "The Devil":
                musicSource.clip = devilSong;
                break;
            case "The Magician":
                musicSource.clip = magicianSong;
                break;
            case "The Emperor":
                musicSource.clip = emperorSong;
                break;
        }

        pa.enabled = true;
        musicSource.enabled = true;
        cardDrawingMusicSource.enabled = false;

    }
}
