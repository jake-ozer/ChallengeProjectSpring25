using UnityEngine;
using System.Collections;

public class WOFManager : MonoBehaviour
{

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

    private PlayerMovement movement;
    private PlayerAttack attack;
    private Camera cam;
    private BuffParticleManager particleManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>().gameObject;
        mode = 0;
        spinSpeed = 0;
        StartCoroutine(waitTimer());
        movement = player.GetComponent<PlayerMovement>();
        attack = player.GetComponent<PlayerAttack>();
        cam = Camera.main;
        particleManager = player.transform.GetChild(0).GetChild(3).GetComponent<BuffParticleManager>();
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
            float spinTimer = Random.Range(3.00f, 15.00f);
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
                break;
            case 1:
                Debug.Log("Eagle");
                StartCoroutine(effectEagle());
                break;
            case 2:
                Debug.Log("Lion");
                StartCoroutine(effectLion());
                break;
            case 3:
                Debug.Log("Bull");
                StartCoroutine(effectBull());
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
        movement.SetJump(movement.GetJump() * 2);
        yield return new WaitForSeconds(effectDuration);
        movement.SetJump(movement.GetJump() / 2);
        mode = 0;
        StartCoroutine(waitTimer());
    }

    private IEnumerator effectLion()
    {
        attack.SetDamage(attack.GetDamage() * 2);
        yield return new WaitForSeconds(effectDuration);
        attack.SetDamage(attack.GetDamage() / 2);
        mode = 0;
        StartCoroutine(waitTimer());
    }

    private IEnumerator effectBull()
    {
        movement.SetSpeed(movement.GetSpeed() * 2);
        cam.fieldOfView += 10;
        yield return new WaitForSeconds(effectDuration);
        movement.SetSpeed(movement.GetSpeed() / 2);
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
}
