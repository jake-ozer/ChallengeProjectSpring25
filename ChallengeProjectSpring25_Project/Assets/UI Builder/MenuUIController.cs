using UnityEngine;
using UnityEngine.UIElements;

public class MenuUIController : MonoBehaviour
{
    // References to the UI panels
    private VisualElement mainMenuPanel;
    private VisualElement settingsPanel;
    private VisualElement creditsPanel;

    // Reference to buttons
    private Button settingsButton;
    private Button creditsButton;
    private Button backButton; // Used for navigating back to the main menu

    void Start()
    {
        // Get the UI Document's root
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // Query the panels by their names
        mainMenuPanel = root.Q<VisualElement>("MainMenu");
        settingsPanel = root.Q<VisualElement>("Settings");
        creditsPanel = root.Q<VisualElement>("Credits");

        // Query the buttons by their names
        settingsButton = root.Q<Button>("SettingsButton");
        creditsButton = root.Q<Button>("CreditsButton");
        backButton = root.Q<Button>("BackButton");

        // Set the initial display states
        mainMenuPanel.style.display = DisplayStyle.Flex;    // Show main menu
        settingsPanel.style.display = DisplayStyle.None;      // Hide settings
        creditsPanel.style.display = DisplayStyle.None;       // Hide credits

        // Register click callbacks for the buttons
        if (settingsButton != null)
        {
            settingsButton.RegisterCallback<ClickEvent>(ev =>
            {
                // Show settings panel and hide main menu
                mainMenuPanel.style.display = DisplayStyle.None;
                settingsPanel.style.display = DisplayStyle.Flex;
            });
        }

        if (creditsButton != null)
        {
            creditsButton.RegisterCallback<ClickEvent>(ev =>
            {
                // Show credits panel, hide main menu
                mainMenuPanel.style.display = DisplayStyle.None;
                creditsPanel.style.display = DisplayStyle.Flex;
            });
        }

        if (backButton != null)
        {
            backButton.RegisterCallback<ClickEvent>(ev =>
            {
                // Hide all additional panels and return to the main menu
                settingsPanel.style.display = DisplayStyle.None;
                creditsPanel.style.display = DisplayStyle.None;
                mainMenuPanel.style.display = DisplayStyle.Flex;
            });
        }
    }
}
