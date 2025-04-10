using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public Slider easeSlider;
    public float easeWait;
    public float easeSpeed;
    //public HealthGFXController controller;
    private bool easing;
    private float startEase;
    private float lerpFactor;

    public void SetMaxStamina(float max)
    {
        staminaSlider.maxValue = max;
        staminaSlider.value = max;

        easeSlider.maxValue = max;
        easeSlider.value = max;
    }

    public void SetStamina(float stam)
    {
        staminaSlider.value = stam;
        //controller.ShowBars();
        if (this.enabled)
        {
            StartCoroutine("EaseBar");
        }
    }


    private void Update()
    {
        if (easing)
        {
            lerpFactor += Time.deltaTime * easeSpeed;
            easeSlider.value = Mathf.Lerp(startEase, staminaSlider.value, lerpFactor);
            if (Mathf.Abs(easeSlider.value - staminaSlider.value) < 0.01f)
            {
                easing = false;
            }
        }
    }
    private IEnumerator EaseBar()
    {
        startEase = easeSlider.value;
        lerpFactor = 0f;
        yield return new WaitForSeconds(easeWait);
        easing = true;
    }

    
}

