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
            startButton.clicked += () => SceneNavigator.LoadScene("TestScene");
        }

        var optionsButton = root.Q<Button>("OptionsButton");
        if (optionsButton != null)
        {
            optionsButton.clicked += () => SceneNavigator.LoadScene("OptionsScene");
        }
    }
}

