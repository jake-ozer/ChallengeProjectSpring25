using UnityEngine;
using UnityEngine.UIElements;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject theMagician;
    [SerializeField] GameObject theBoss;
    private float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;//timer for spawning of the magicians

        if (time == 10f)
        {
            Instantiate(theMagician, theBoss.transform.position, Quaternion.identity);
            time = 0;
        }
    }
}
