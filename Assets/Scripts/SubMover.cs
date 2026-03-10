using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
//originally the player was just a gun and didn't move
//then i thought that was too boring and i could do this fast
public class SubMover : MonoBehaviour
{
    //is the siren sound effect playing
    bool sirenIsPlaying = false;
    public GameObject Waypoint1; //start
    GameObject createWay1;
    public GameObject Waypoint; //end
    GameObject createWay;
    public GameObject AimFiring;
    //get the firing script (important for later)
    AimFire fireScript;

    public float damage = 50f;
    //can shoot is a leftover, mattered back when i intended to have a reload button
    public bool canShoot = true;
    //public Transform subPos;

    [SerializeField] bool activeWaypoint = false;
    [SerializeField] float t = 0;
    //player hp bar
    public float SubHealth = 765;
    public AudioSource warningsiren;

    //public Slider Sname;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //reference the aimfire script so that this can get the damage values
        fireScript = AimFiring.GetComponent<AimFire>();
        //i was not intending for this script to be what the spawned enemy prefab references when taking damage
        // this was a round about option because of how the player object and its children are poorly set up
    }

    // Update is called once per frame
    void Update()
    {
        //SubSpeed = Sname.value;
        damage = fireScript.damage;
        //only allow the player to move if they have health
        if (SubHealth > 0)
        {
            //if health gets low enough play a siren
            if (SubHealth < 365)
            {// i had to make an is playing boolean because otherwise it would play the siren every frame, instead of only once and looping it
                if (sirenIsPlaying == false)
                {
                    warningsiren.Play();
                    sirenIsPlaying = true;
                }
            }
            //pretty much this creates two objects that the player can lerp between
            //I did lerp because I didn't want to use standard movement controls

            //start point is at player position, end point is at mouse position
            //everytime player right clicks, new start and end positions are created and previous ones are deleted
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (Mouse.current.rightButton.wasPressedThisFrame && activeWaypoint == false)
            {
                //way points are transparent prefabs, hence why I need two game object references for each one
                createWay = Instantiate(Waypoint, mousePos, Quaternion.identity);
                createWay1 = Instantiate(Waypoint1, transform.position, Quaternion.identity);
                activeWaypoint = true;
                t = 0;
            }

            else if (Mouse.current.rightButton.wasPressedThisFrame && activeWaypoint == true)
            {
                Destroy(createWay);
                createWay = Instantiate(Waypoint, mousePos, Quaternion.identity);
                Destroy(createWay1);
                createWay1 = Instantiate(Waypoint1, transform.position, Quaternion.identity);
                activeWaypoint = true;
                t = 0;
            }
            //if there are active waypoints on screen, lerp the player between them
            if (activeWaypoint)
            {
                Transform start = createWay1.GetComponent<Transform>();
                Transform end = createWay.GetComponent<Transform>();
                transform.position = Vector2.Lerp(start.position, end.position, t);
                t += Time.deltaTime;
                //these if statements are to give it a "feel" of acceleration without acceleration
                if (t < 0.3)
                {
                    t += 0.7f * Time.deltaTime;
                }

                else if (t > 0.8 && t < 1)
                {
                    t += 0.5f * Time.deltaTime;
                }
                else if (t >= 1)
                {
                    //delete the waypoints once the player reaches the end
                    activeWaypoint = false;
                    Destroy(createWay);
                    Destroy(createWay1);
                }
                //acceleration in movement was not allowed?
            }
        }
        else 
        {
            //destroy/despawn the player after they dramatically float down losing movement control
            //also breaks enemy script so the player knows the game is over (unintentional)
            Destroy(gameObject, 30f);
            transform.position += transform.up * -1 * Time.deltaTime;
        }
        
    }
}
/* back up for when I was messing around with the movement in earlier versions
 *     //public GameObject Waypoint1; //start
    //GameObject createWay1;
    //public GameObject Waypoint; //end
    //GameObject createWay;

    //[SerializeField] bool activeWaypoint = false;
    //[SerializeField] float t = 0;
 * if (Mouse.current.rightButton.wasPressedThisFrame && activeWaypoint == false)
        {
            createWay = Instantiate(Waypoint, mousePos, Quaternion.identity);
            createWay1 = Instantiate(Waypoint1, transform.position, Quaternion.identity);
            activeWaypoint = true;
            t = 0;
        }

        else if (Mouse.current.rightButton.wasPressedThisFrame && activeWaypoint == true)
        {
            Destroy(createWay);
            createWay = Instantiate(Waypoint, mousePos, Quaternion.identity);
            Destroy(createWay1);
            createWay1 = Instantiate(Waypoint1, transform.position, Quaternion.identity);
            activeWaypoint = true;
            t = 0;
        }
 * if (activeWaypoint) 
        {
            Transform start = createWay1.GetComponent<Transform>();
            Transform end = createWay.GetComponent<Transform>();
            transform.position = Vector2.Lerp(start.position, end.position, t);
            t += Time.deltaTime;
            if (t < 0.3)
            {
                t += 0.7f * Time.deltaTime;
            }

            else if (t > 0.8 && t < 1)
            {
                t += 0.5f * Time.deltaTime;
            }
            else if (t >= 1) 
            {
                activeWaypoint = false;
                Destroy(createWay);
                Destroy(createWay1);
            }
        } */