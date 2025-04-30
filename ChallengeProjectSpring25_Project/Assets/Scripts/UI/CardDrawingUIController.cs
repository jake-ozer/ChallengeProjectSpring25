using TMPro;
using UnityEngine.UI;
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

    public void ShowCardGameOverUI(string name, Sprite image)
    {
        cardNameTextObj.GetComponent<TextMeshProUGUI>().text = name;
        cardNameTextObj.SetActive(true);
        cardDescTextObj.GetComponent<Image>().sprite = image;
        cardDescTextObj.SetActive(true);
    }

    public void HideCardInfoUI()
    {
        cardNameTextObj.SetActive(false);
        cardDescTextObj.SetActive(false);
    }
}
