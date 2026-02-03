using UnityEngine;

public class Toggle : MonoBehaviour
{
    public void Toggleshape() 
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
        //if game object is off, turn it active
        //gameObject.SetActive(false);
        //if (gameObject.activeInHierarchy == false)
       // {
        //    gameObject.SetActive(true);
        //}
       // else if (gameObject.activeInHierarchy == true)
       // {
        //    gameObject.SetActive(false);
       // }
        //if game object is active, turn it inactive (call set active and pass false)
    }
}
