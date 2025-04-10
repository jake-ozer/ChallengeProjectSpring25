using UnityEngine;

public class spawnpoint_controller : MonoBehaviour
{
    //public GameObject playerPrefab; 
    //public GameObject bossPrefab;   
    private Transform PlayerRelocatePoint; 
    private Transform BossRelocatePoint;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SpawnPlayer();
        //SpawnBoss();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RelocatePlayer()
    {
        PlayerRelocatePoint = GameObject.FindWithTag("PlayerSpawnPoint").transform;
        FindFirstObjectByType<PlayerHealth>().gameObject.transform.position = PlayerRelocatePoint.position;
        //Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        Debug.LogError("Player relocated");
        
    }

    public void RelocateBoss()
    {
        BossRelocatePoint = GameObject.FindWithTag("BossSpawnPoint").transform;
        FindFirstObjectByType<BossHealth>().gameObject.transform.position = BossRelocatePoint.position;
        //Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        Debug.LogError("Boss relocated");
    
    }

}
