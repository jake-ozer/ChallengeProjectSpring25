using UnityEngine;
using System.Collections;

public class HealPlayerOnContact : MonoBehaviour
{
    //you can put this script onto any trigger game object that needs to heal the player
    [SerializeField]
    private float[] despawnTimes;

    private int flickerStage;
    private float timer;

    bool invisible;
    public int healing;

    public GameObject rend;

    private void Awake()
    {
        flickerStage = 0;
        //rend = GetComponent<MeshRenderer>();
        StartCoroutine(despawnTimer());

    }

    private void Update()
    {
        switch(flickerStage)
        {
            case 1:
                if(!invisible && timer >= .5f)
                {
                    rend.SetActive(false);
                    invisible = true;
                    timer = 0;
                }
                else if(invisible && timer >= .25f)
                {
                    rend.SetActive(true);
                    invisible = false;
                    timer = 0;
                }
                break;
            case 2:
                if (!invisible && timer >= .25f)
                {
                    rend.SetActive(false);
                    invisible = true;
                    timer = 0;
                }
                else if (invisible && timer >= .1f)
                {
                    rend.SetActive(true);
                    invisible = false;
                    timer = 0;
                }
                break;
        }

        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            other.gameObject.GetComponent<PlayerHealth>().HealDamage(healing);
            Destroy(gameObject);
            other.transform.GetChild(0).GetChild(3).GetComponent<BuffParticleManager>().EnableParticles(4);
        }
    }

    private IEnumerator despawnTimer()
    {
        yield return new WaitForSeconds(despawnTimes[0]);
        flickerStage++;
        yield return new WaitForSeconds(despawnTimes[1]);
        flickerStage++;
        yield return new WaitForSeconds(despawnTimes[2]);
        Destroy(gameObject);
    }
}
