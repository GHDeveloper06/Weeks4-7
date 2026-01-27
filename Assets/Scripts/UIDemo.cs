using UnityEngine;
using UnityEngine.InputSystem;

public class UIDemo : MonoBehaviour
{
    SpriteRenderer SR;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.anyKey.wasPressedThisFrame == true)
        {
            ChangeColour();
        }
    }

    public void ChangeColour() 
    {
        SR.color = Random.ColorHSV();
    }

    public void SetScale(float scale) 
    {
        transform.localScale = Vector3.one * scale;
    }
}
