using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    public float speed = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.dKey.isPressed) 
        { 
            transform.position += transform.right * speed * Time.deltaTime;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            transform.position += transform.right * -speed * Time.deltaTime;
        }
    }
}
