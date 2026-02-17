using UnityEngine;
using UnityEngine.Events;

public class ContactSensor : MonoBehaviour
{
    public SpriteRenderer hazard;
    public bool isInHazard = false;
    public UnityEvent OnEnterHazard;
    public UnityEvent OnExitHazard;

    public UnityEvent<float> OnRandomNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //is the player inside the hazard sprite?
        if (hazard.bounds.Contains(transform.position) == true)
        {
            if (isInHazard == true)
            {
                //we're still in the hazard
            }
            else 
            {
                //just entered the hazard do something
                //Y: player has tripped the sensor
                isInHazard = true;
                Debug.Log("entered the hazard");
                OnEnterHazard.Invoke();
            }
            

        }
        else 
        {
            if (isInHazard == true)
            {
                //we just left the hazard
                //N: player has not tripped the sensor
                isInHazard = false;
                Debug.Log("exited the hazard");
                OnExitHazard.Invoke();
                OnRandomNumber.Invoke(Random.Range(0, 10));
            }
            else 
            { 
                //we're still not in the hazard
            }
            
        }


    }

    public void ShowNumber(float number) 
    {

        Debug.Log(number);
    }
}
