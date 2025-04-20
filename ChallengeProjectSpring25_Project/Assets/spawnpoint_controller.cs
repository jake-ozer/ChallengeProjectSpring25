using UnityEngine;
using UnityEngine.AI;

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
        var playerObj = FindFirstObjectByType<PlayerHealth>().gameObject;
        playerObj.GetComponent<CharacterController>().enabled = false;
        playerObj.transform.position = PlayerRelocatePoint.position;
        playerObj.GetComponent<CharacterController>().enabled = true;

        //make player look at the boss for good effect
        var bossObj = FindFirstObjectByType<BossHealth>().gameObject;
        Vector3 lookDirection = bossObj.transform.position - playerObj.transform.position;
        lookDirection.y = 0f;
        if (lookDirection != Vector3.zero)
        {
            playerObj.transform.rotation = Quaternion.LookRotation(lookDirection);
        }
        var playerCamera = playerObj.transform.Find("PlayerCamera").gameObject.transform;
        Vector3 camDir = bossObj.transform.position - playerCamera.position;
        float angleX = -Mathf.Atan2(camDir.y, new Vector2(camDir.x, camDir.z).magnitude) * Mathf.Rad2Deg;
        playerCamera.localEulerAngles = new Vector3(angleX, 0f, 0f);
    }

    public void RelocateBoss()
    {
        BossRelocatePoint = GameObject.FindWithTag("BossSpawnPoint").transform;
        FindFirstObjectByType<BossHealth>().gameObject.GetComponent<NavMeshAgent>().Warp(BossRelocatePoint.position);
    }

}
