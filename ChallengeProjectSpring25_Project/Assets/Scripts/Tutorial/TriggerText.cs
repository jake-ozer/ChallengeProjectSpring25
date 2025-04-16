using UnityEngine;

public class TriggerText : MonoBehaviour
{
    //[SerializeField] private Animator tutText = null;

    //[SerializeField] private bool textTrigger = false;
    //[SerializeField] bool isActive = false;

    public GameObject tutorialText;


    private void Start()
    {
        tutorialText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit trigger");
            tutorialText.SetActive(true); 
            
        }
    }

}
