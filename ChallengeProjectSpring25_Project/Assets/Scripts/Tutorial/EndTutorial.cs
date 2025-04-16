using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTutorial : MonoBehaviour
{

    [SerializeField] private int sceneNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit trigger");
            SceneManager.LoadScene(sceneNumber);

        }
    }


}
