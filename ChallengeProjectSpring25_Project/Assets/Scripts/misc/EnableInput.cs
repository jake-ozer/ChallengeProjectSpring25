using UnityEngine;
using UnityEngine.InputSystem;

public class EnableInput : MonoBehaviour
{
    private void Awake()
    {
        Invoke("FixInput", 0.5f);
        

    }

    private void FixInput()
    {
       // Debug.Log("tried to fix input");
        var playerInput = GetComponent<PlayerInput>();
        playerInput.enabled = false;
        playerInput.enabled = true;
    }
}
