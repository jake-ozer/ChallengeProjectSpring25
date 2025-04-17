using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickTutorialLoad : MonoBehaviour
{
   

    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("QuickTutorial");
    }
}
