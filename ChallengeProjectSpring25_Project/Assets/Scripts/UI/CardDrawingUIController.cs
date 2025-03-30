using TMPro;
using UnityEngine;

public class CardDrawingUIController : MonoBehaviour
{
    public GameObject cardNameTextObj;
    public GameObject cardDescTextObj;

    private void Awake()
    {
        HideCardInfoUI();
    }

    public void ShowCardInfoUI(string name, string desc)
    {
        cardNameTextObj.GetComponent<TextMeshProUGUI>().text = name;
        cardNameTextObj.SetActive(true);
        cardDescTextObj.GetComponent<TextMeshProUGUI>().text = desc;
        cardDescTextObj.SetActive(true);
    }

    public void HideCardInfoUI()
    {
        cardNameTextObj.SetActive(false);
        cardDescTextObj.SetActive(false);
    }
}
