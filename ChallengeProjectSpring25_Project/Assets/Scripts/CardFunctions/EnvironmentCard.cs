using UnityEngine;

public class EnvironmentCard : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    
    public void SpawnEnvironmentEffect()
    {
        Instantiate(effect, new Vector3(0,0,0), Quaternion.identity);
    }
}
