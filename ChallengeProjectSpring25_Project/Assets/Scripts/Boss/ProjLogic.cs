using UnityEngine;

public class ProjLogic : MonoBehaviour
{

    public float speed;
    //public Vector3 dir;

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            //Debug.Log(other.gameObject.tag);
            //Destroy(gameObject);
        }
    }
}
