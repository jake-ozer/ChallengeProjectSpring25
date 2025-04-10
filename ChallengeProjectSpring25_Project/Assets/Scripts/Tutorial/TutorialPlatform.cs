using UnityEngine;

public class TutorialPlatform : MonoBehaviour
{
    public float timer;
    public GameObject platform;
    private float reappearTimer;
    private float disappearTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        disappearTimer = timer;
        reappearTimer = -1 * timer;
    }

    // Update is called once per frame
    void Update()
    {
        
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0 && platform.active == true)
        {
            platform.SetActive(false);
            
        }

        if (disappearTimer < reappearTimer)
        {
            Debug.Log(disappearTimer);
            platform.SetActive(true);
            disappearTimer = timer;
           }
    }
}
