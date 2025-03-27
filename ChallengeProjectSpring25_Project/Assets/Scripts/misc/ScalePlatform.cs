using UnityEngine;

public class ScalePlatform : MonoBehaviour
{
    public GameObject platform1;
    
    public GameObject boss;
    public Transform platform1Start;
    public Transform platform1End;

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
    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            
        t ++;
        platform1.transform.position = Vector3.Lerp(platform1Start.position, platform1End.position, t);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
        t --;
        platform1.transform.position = Vector3.Lerp(platform1Start.position, platform1End.position, t);
        }
    }



     
}
