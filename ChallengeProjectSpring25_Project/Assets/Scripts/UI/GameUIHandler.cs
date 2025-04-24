using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public PlayerControl PlayerControl;
    public UIDocument UIDoc;

    private Label m_HPLabel;
    private VisualElement m_HealthBarMask;

    private void Start()
    {
        PlayerControl.OnHealthChange += HealthChanged;
        m_HPLabel = UIDoc.rootVisualElement.Q<Label>("HPLabel");
        m_HealthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("HPMask");
        HealthChanged();
    }


    void HealthChanged()
    {
        m_HPLabel.text = $"{PlayerControl.CurrentHealth}/{PlayerControl.MaxHealth}";
        float healthRatio = (float)PlayerControl.CurrentHealth / PlayerControl.MaxHealth;
        float healthPercent = Mathf.Lerp(8, 88, healthRatio);
        m_HealthBarMask.style.width = Length.Percent(healthPercent);
    }
}