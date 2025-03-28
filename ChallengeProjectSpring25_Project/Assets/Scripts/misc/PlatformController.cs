using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject platform1;
    public GameObject platform2;

    public float platformOffset;
    public float p1Speed;
    public float p2Speed;

    private Vector3 p1Start;
    private Vector3 p2Start;
    private Vector3 p1End;
    private Vector3 p2End;
    private float p1TotalSpeed;
    private float p2TotalSpeed;
    

    private void Start()
    {
        p1Start = platform1.transform.position;
        p2Start = platform2.transform.position;
        p1End = new Vector3(p1Start.x, p1Start.y - platformOffset, p1Start.z);
        p2End = new Vector3(p2Start.x, p2Start.y - platformOffset, p2Start.z);
    }

    public float t1 = 0;
    public float t2 = 0;
    private void Update()
    {
        t1 += Time.deltaTime * p1TotalSpeed;
        t2 += Time.deltaTime * p2TotalSpeed;
        t1 = Mathf.Clamp01(t1);
        t2 = Mathf.Clamp01(t2);
        platform1.transform.position = Vector3.Lerp(p1Start, p1End, t1);
        platform2.transform.position = Vector3.Lerp(p2Start, p2End, t2);
        
    }

    //called by individual platform scripts that detect when something is on it
    //this is a state changer method, will not be called every frame
    public void SetPlatformChange(GameObject platform, float speedDir)
    {
        Debug.Log(platform.name);
        if (platform == platform1)
        {
            
            this.p1TotalSpeed = speedDir * p1Speed;
        }
        else if (platform == platform2)
        {
            this.p2TotalSpeed = speedDir * p2Speed;
        }
    }
}
