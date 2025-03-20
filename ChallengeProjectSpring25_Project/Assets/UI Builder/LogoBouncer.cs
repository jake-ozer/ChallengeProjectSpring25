using UnityEngine;
using UnityEngine.UIElements;

public class LogoBouncer : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float bounceSpeed = 2f;   // Controls how fast the logo bounces
    public float bounceHeight = 10f; // Controls how high the logo bounces
    public float bounceWidth = 5f;

    private VisualElement logoElement;

    void Start()
    {
        // Get the UIDocument attached to this GameObject
        UIDocument uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("No UIDocument component found on this GameObject.");
            return;
        }

        // Access the root visual element
        VisualElement root = uiDocument.rootVisualElement;

        // Find the logo element by its name (set in UI Builder)
        logoElement = root.Q<VisualElement>("Clairvoyant");
        if (logoElement == null)
        {
            Debug.LogError("Logo element not found in UI Document. Check the name in UI Builder.");
        }
    }

    void Update()
    {
        if (logoElement != null)
        {
            // Calculate the vertical offset using a sine wave.
            // Mathf.Abs ensures the bounce is always "upward" from its base position.
            float offsetY = Mathf.Cos((Time.time) * bounceSpeed) * bounceHeight;
            float offsetX = Mathf.Tan((Time.time)* bounceSpeed) * bounceWidth;
            

            // Update the logo's style to apply the bounce effect.
            logoElement.style.translate = new Translate(offsetX, offsetY, 0);
        }
    }
}
