using UnityEngine;

public class Falling_Meteor : MonoBehaviour
{
    public GameObject Meteor;
    [SerializeField] private int x1;
    [SerializeField] private int x2;
    [SerializeField] private int y;
    [SerializeField] private int z1;
    [SerializeField] private int z2;
    [SerializeField] private int timer;
    private int timerCap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerCap = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0)
        {
        RandomMeteorSpawn();
        timer = timerCap;
        }
        else
        {
            timer--;
        } 
    }

    void RandomMeteorSpawn()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(x1,x2), y, Random.Range(z1,z2));
        Instantiate(Meteor, randomSpawnPosition, Quaternion.identity);
        
    }


}
