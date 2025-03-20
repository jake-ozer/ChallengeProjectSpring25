using UnityEngine;

public class ScalePlatform : MonoBehaviour
{
    public GameObject platform1;
    
    public Transform platform1Start;
    public Transform platform1End;

    private int duration = 0;

    public float t = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      // Vector3 upPosition = new Vector3 (x, y3, z); 
      // Vector3 downPosition = new Vector3 (x, y1, z);
      // Vector3 midPosition = new Vector3 (x, y2, z);
    }

    // Update is called once per frame
    void Update()
    {
        //t += Time.deltaTime;
        //t = Mathf.Clamp(xValue, xMin, xMax);
        Debug.Log(t);
        platform1.transform.position = Vector3.Lerp(platform1Start.position, platform1End.position, t);

        
        /*duration++;

        if (duration == t)
        {
            Vector3 startPosition = transform.position;
            if(startPosition == downPosition)
            {
                if(!weight)
                {
                    //Vector3 targetPosition = new Vector3 (x, y2, z);
                    //if enemy is not on platform, it will rise from lowest position to middle position
                    transform.position = Vector3.Lerp(startPosition, midPosition, t);
                }
            }
            if(startPosition == midPosition)
            {
                if(weight)
                {
                    //if enemy is on platform, it will lower to its lowest position
                    transform.position = Vector3.Lerp(startPosition, downPosition, t);
                }
                else
                {
                    //if enemy is off platoform, it will rise to its highest position
                    transform.position = Vector3.Lerp(startPosition, upPosition, t);
                }
            }
            if(startPosition == upPosition)
            {
                if(weight)
                {
                    //if enemy is on platform, the platform will lower to the middle position.
                    transform.position = Vector3.Lerp(startPosition, midPosition, t);
                }
            }
            duration = 0;*/
    }
    

     /*IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }*/
}
