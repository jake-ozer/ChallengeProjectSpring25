using UnityEngine;

public class ScalePlatform : MonoBehaviour
{
    public GameObject platform1;
    
    public GameObject boss;
    public Vector3 platformUp;
    public Vector3 platformDown;

    private bool bossOnPlatform;

    //private int duration = 0;

    public float t = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      bossOnPlatform = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(t);
        if(bossOnPlatform)
        {
            platform1.transform.position = Vector3.MoveTowards(platform1.transform.position, platformDown, t*Time.deltaTime);
        }
        else{
            platform1.transform.position = Vector3.MoveTowards(platform1.transform.position, platformUp, t*Time.deltaTime);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        
        Debug.Log(other.gameObject.name);
        if (other.gameObject.layer == 9)
        {
            Debug.Log("Found boss");
            bossOnPlatform = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.layer == 9)
        {
            bossOnPlatform = false;
        }
    }
     
}
