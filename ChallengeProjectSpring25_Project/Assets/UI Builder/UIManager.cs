using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private UIDocument uiDocument;

    private void Start()
    {
        uiDocument = GetComponent<UIDocument>();

        // Get root visual element
        var root = uiDocument.rootVisualElement;

        // Assign button callbacks
        var startButton = root.Q<Button>("StartButton");
        if (startButton != null)
        {
            //startButton.clicked += () => SceneNavigator.LoadScene("MAIN_GAME_SCENE");
            startButton.clicked += () => FindFirstObjectByType<GameLoopController>().FadeOutAndLoadScene("MAIN_GAME_SCENE");
        }

        var tutorialButton = root.Q<Button>("TutorialButton");
        if (tutorialButton != null)
        {
            tutorialButton.clicked += () => FindFirstObjectByType<GameLoopController>().FadeOutAndLoadScene("TUTORIAL");
        }

        var optionsButton = root.Q<Button>("OptionsButton");
        if (optionsButton != null)
        {
            optionsButton.clicked += () => SceneNavigator.LoadScene("OptionsScene");
        }
    }
}

