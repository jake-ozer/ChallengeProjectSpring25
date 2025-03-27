using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cardObj;
    
    public void SpawnCardObj()
    {
        Instantiate(cardObj, new Vector3(0,0,0), Quaternion.identity);
    }
}
