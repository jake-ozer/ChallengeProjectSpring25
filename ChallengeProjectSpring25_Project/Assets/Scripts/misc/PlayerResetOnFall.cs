using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerResetOnFall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FindFirstObjectByType<GameLoopController>().FadeOutAndLoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
