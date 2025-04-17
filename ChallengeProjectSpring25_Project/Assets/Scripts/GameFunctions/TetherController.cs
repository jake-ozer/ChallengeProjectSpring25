using UnityEngine;

public class TetherController : MonoBehaviour
{
    private GameObject tetherIndicatorObj;
    private PlayerTether playerTether;

    private void Start()
    {
        playerTether = FindFirstObjectByType<PlayerTether>();
        playerTether.enabled = true;
    }

    private void Update()
    {
        if (FindFirstObjectByType<BossHealth>() != null)
        {
            tetherIndicatorObj = FindFirstObjectByType<BossHealth>().gameObject.transform.Find("TetherCircleIndicator").gameObject;
            tetherIndicatorObj.SetActive(true);
        }
    }
}
