using UnityEngine;

public class WOFArrow : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetEffect()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red, Mathf.Infinity);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            hit.transform.gameObject.GetComponent<WOFGlow>().Glow();
            string name = hit.transform.name;
            Debug.Log("Object hit: " + name);
            switch (name)
            {
                case "Angel":
                    return 0;
                    break;
                case "Eagle":
                    return 1;
                    break;
                case "Lion":
                    return 2;
                    break;
                case "Bull":
                    return 3;
                    break;
                default:
                    return 4;
            }

        }
        else
            return 4;
    }
}
