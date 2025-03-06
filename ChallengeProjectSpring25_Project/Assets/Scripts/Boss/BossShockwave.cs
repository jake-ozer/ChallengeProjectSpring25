using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossShockwave : MonoBehaviour
{
    public float shockTime; //will determine how often boss does this move.
    public float shockSpeed; //determines how fast the shockwave will grow
    public float shockHeight; //how tall shockwave is
    private float timer;
    private NavMeshAgent bossMovement;
    [SerializeField] private GameObject shockwaveObj;
    private Vector3 initialObjVector;
    private bool shocking = false;
    
    //Logic behind this script
    //Every 5/user defined seconds the boss will stop and a shockwave will spawn.
    //The shockwave will have a certain height, and increase at a constant rate.
    //The shockwave will end when it hits the player or time runs out.

    private void Start()
    {
        timer = shockTime;
        bossMovement = GetComponent<NavMeshAgent>();
        Vector3 newScale = shockwaveObj.transform.localScale;
        newScale.y = shockHeight;
        shockwaveObj.transform.localScale = newScale;
        initialObjVector = shockwaveObj.transform.localScale;
    }

    private void Update()
    {
        if(!shocking)
        {
            timer -= Time.deltaTime;
            
        }
        
        
        if(timer < 0)
        {
            //start shockwave
            
            shocking = true;
            timer = shockTime;
            StartCoroutine("StartShockwave");
            
        }
    }

    private IEnumerator StartShockwave()
    {
        bossMovement.enabled = false;
        shockwaveObj.SetActive(true);
     
        for (int i = 0; i < 30; i++)
        {
            shockwaveObj.transform.localScale += new Vector3(shockSpeed, 0, shockSpeed);
            yield return new WaitForSeconds(.1f);
           
        }
        bossMovement.enabled = true;
        shockwaveObj.transform.localScale = initialObjVector;
        shockwaveObj.SetActive(false);
        shocking = false;
        

        
    }

   



}
