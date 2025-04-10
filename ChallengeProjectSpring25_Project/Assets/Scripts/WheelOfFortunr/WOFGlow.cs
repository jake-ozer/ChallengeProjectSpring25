using UnityEngine;
using System.Collections;

public class WOFGlow : MonoBehaviour
{
    private Material mat;
    [SerializeField]
    private AudioSource soundEffect;
    private int count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        mat = GetComponent<MeshRenderer>().materials[1];
        mat.DisableKeyword("_EMISSION");
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Glow()
    {
        soundEffect.Play();
        StartCoroutine(glowEffect());
    }

    private IEnumerator glowEffect()
    {
        yield return new WaitForSeconds(0.175f);
        mat.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.140f);
        mat.DisableKeyword("_EMISSION");
        count++;
        if(count != 3)
        {
            StartCoroutine(glowEffect());
        }
        else
        {
            count = 0;
        }
        GetComponent<MeshRenderer>().materials[1] = mat;
    }

}
