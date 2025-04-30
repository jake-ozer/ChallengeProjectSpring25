using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartMainLevel : MonoBehaviour
{
    private GameLoopController glc;

    private void Start()
    {
        glc = FindFirstObjectByType<GameLoopController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("tried to restart level");
            Color color = Color.black;
            color.a = 0f;
            //FindFirstObjectByType<GameLoopController>().fadeBackground.SetActive(false);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
            //glc.fadeBackground.SetActive(false);
            glc.FadeOutAndLoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
