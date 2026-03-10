using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollow : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //super simple bread n butter script
        //get the mouse position, convert values so its not goofy
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //transform this game object's position equal to mouse position so it follows the mouse
        transform.position = mousePos;
    }
}
