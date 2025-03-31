using UnityEngine;

public class MeteorIndicator : MonoBehaviour
{

    public GameObject meteorText;
    public LayerMask meteorLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meteorText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray meteorDetect = new Ray(transform.position + Vector3.up * 0.5f, Vector3.up); //0.5 is the offset
        if(Physics.Raycast(meteorDetect, out hit, 100, meteorLayer))//100 is the height.
        {
            //Debug.Log("test good!!!!");
            meteorText.SetActive(true);
        } else
        {
            meteorText.SetActive(false);
        }
    }
}
