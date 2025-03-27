using UnityEngine;

public class ScalePlatform : MonoBehaviour
{
    public GameObject platform1;
    
    public GameObject boss;
    public Vector3 platformUp;
    public Vector3 platformDown;

    //private int duration = 0;

    public float t = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(t);
        
    }
    
    void OnTriggerStay(Collider other)
    {
        
        Debug.Log(other.gameObject.name);
        if (other.gameObject.layer == 9)
        {
            Debug.Log("Found boss");
            
        
            platform1.transform.position = Vector3.MoveTowards(platform1.transform.position, platformDown, t);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
        
        platform1.transform.position = Vector3.Lerp(platform1.transform.position, platformUp, t);
        }
    }
     
}
