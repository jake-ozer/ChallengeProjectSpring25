using UnityEngine;
using System.Collections;
using TMPro;

public class WOFManager : MonoBehaviour
{
    private bool begun;

    [SerializeField]
    private GameObject outsideObject;
    [SerializeField]
    private GameObject healthPrefab;
    [SerializeField]
    private int numberOfPickups;
    [SerializeField]
    private float spawnRadius;

    private GameObject player;

    [SerializeField]
    private GameObject arrow;

    [SerializeField]
    private int mode;
    /* 0 = wait
     * 1 = start spin
     * 2 = spin
     * 3 = slow spin
     * 4 = effect trigger
     * 5 = effect duration */

    [SerializeField]
    private float waitTime;
    [SerializeField]
    private float spinMaxSpeed;
    [SerializeField]
    private float spinSpeed;
    [SerializeField]
    private float spinAccel;
    [SerializeField]
    private float effectDuration;
    [SerializeField]
    private bool effectBoss;

    [Header("Buff Multipliers")]
    [SerializeField]
    private float eagleMult;
    [SerializeField]
    private float lionMult;
    [SerializeField]
    private float bullMult;

    private PlayerMovement movement;
    private PlayerAttack attack;
    private Camera cam;
    private BuffParticleManager particleManager;
    private IBoss iBoss;

    public TextMeshProUGUI buffText;
    public Color eagleColor;
    public Color bullColor;
    public Color lionColor;
    public Color angelColor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        begun = false;
        mode = -1;
        spinSpeed = 0;
        buffText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Spin platform
        switch (mode)
        {
            case 0:
                //wait mode
                break;
            case 1:
                //start spin mode
                startSpin();
                break;
            case 2:
                //spin mode
                break;
            case 3:
                //stop spin
                stopSpin();
                break;
            case 4:
                mode = 5;
                StartCoroutine(triggerEffect());
                break;
            default:
                break;

        }

        outsideObject.transform.Rotate(new Vector3(0, spinSpeed, 0) * Time.deltaTime);
    }

    private IEnumerator waitTimer()
    {
        yield return new WaitForSeconds(waitTime);
        waitTime *= .75f;
        mode = 1;
    }

    private IEnumerator spinTime(float time)
    {
        yield return new WaitForSeconds(time);
        mode = 3;
    }

    private void startSpin()
    {
        spinSpeed += spinAccel * Time.deltaTime;
        if (spinSpeed >= spinMaxSpeed)
        {
            spinSpeed = spinMaxSpeed;
            mode++;
            float spinTimer = Random.Range(0.00f, 4.00f);
            Debug.Log("Spin Time: " + spinTimer);
            StartCoroutine(spinTime(spinTimer));

        }

    }

    private void stopSpin()
    {
        spinSpeed -= spinAccel * Time.deltaTime;
        if (spinSpeed <= 0)
        {
            spinSpeed = 0;
            mode++;

        }

    }

    private IEnumerator triggerEffect()
    {
        int effect = arrow.GetComponent<WOFArrow>().GetEffect();
        /* 0 = Angel
         * 1 = Eagle
         * 2 = Lion
         * 3 = Bull */

        yield return new WaitForSeconds(0.945f);

        switch (effect)
        {
            case 0:
                Debug.Log("Angel");
                StartCoroutine(effectAngel());
                StartCoroutine(showBuffText(angelColor, "Angel: Health Drops Spawned"));
                break;
            case 1:
                Debug.Log("Eagle");
                StartCoroutine(effectEagle());
                StartCoroutine(showBuffText(eagleColor, "Eagle: +Player Jump Height"));
                break;
            case 2:
                Debug.Log("Lion");
                StartCoroutine(effectLion());
                StartCoroutine(showBuffText(lionColor, "Lion: +Player/Golem Damage"));
                break;
            case 3:
                Debug.Log("Bull");
                StartCoroutine(effectBull());
                StartCoroutine(showBuffText(bullColor, "Bull: +Player/Golem Speed"));
                break;
            default:
                Debug.Log("No effect");
                break;

        }

        particleManager.EnableParticles(effect);
    }

    private IEnumerator effectAngel()
    {
        //Angel Effect starts here
        for(int i = 0; i < numberOfPickups; i++)
        {
            Vector3 randomPos = getSpawnPos();
            Instantiate(healthPrefab, randomPos, Quaternion.identity);
        }
        yield return new WaitForSeconds(effectDuration);
        //Angel Effect turns off here
        mode = 0;
        StartCoroutine(waitTimer());
    }

    private IEnumerator effectEagle()
    {
        movement.SetJump(movement.GetJump() * eagleMult);
        iBoss.eagleBuff();
        yield return new WaitForSeconds(effectDuration);

        mode = 0;
        StartCoroutine(waitTimer());
    }

    private IEnumerator effectLion()
    {
        attack.SetDamage((int)(attack.GetDamage() * lionMult));
        iBoss.lionBuff();
        yield return new WaitForSeconds(effectDuration);
        
        mode = 0;
        StartCoroutine(waitTimer());
    }

    private IEnumerator effectBull()
    {
        movement.SetSpeed(movement.GetSpeed() * bullMult);
        cam.fieldOfView += 10;
        iBoss.bullBuff();
        yield return new WaitForSeconds(effectDuration);

        cam.fieldOfView -= 10;
        mode = 0;
        StartCoroutine(waitTimer());
    }

    private Vector3 getSpawnPos()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * spawnRadius;

        float x = Mathf.Cos(angle) * distance;
        float z = Mathf.Sin(angle) * distance;

        return new Vector3(x, -1.5f, z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "Player" && !begun)
        {
            Debug.Log("Begun");
            begun = true;
            StartCoroutine(waitTimer());
            mode = 0;
            player = FindFirstObjectByType<PlayerHealth>().gameObject;
            movement = player.GetComponent<PlayerMovement>();
            attack = player.GetComponent<PlayerAttack>();
            cam = Camera.main;
            particleManager = player.transform.GetChild(0).GetChild(3).GetComponent<BuffParticleManager>();
            iBoss = GameObject.FindGameObjectsWithTag("Enemy")[0].transform.Find("BuffHandler").GetComponent<IBoss>();
        }
    }

    private IEnumerator showBuffText(Color c, string t)
    {
        buffText.color = c;
        buffText.text = t;
        buffText.enabled = true;
        yield return new WaitForSeconds(2.5f);
        buffText.enabled = false;
    }
}
