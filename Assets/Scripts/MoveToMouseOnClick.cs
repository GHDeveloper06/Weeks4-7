using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MoveToMouseOnClick : MonoBehaviour
{
    public SpriteRenderer player;
    //public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (Mouse.current.leftButton.wasPressedThisFrame && EventSystem.current.IsPointerOverGameObject())
        {
            transform.position = mousePos;
            //audioSource.Play();
        }
        
        
    }
}
