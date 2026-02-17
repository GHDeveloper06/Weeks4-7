using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class EnemyType1 : MonoBehaviour
{
    //stat values
    public float health = 150f;
    public float speed = 2;
    public float danger = 100;

    //lets sound be played
    public AudioSource soundnoise;
    //lets soundeffect be changed
    public AudioClip soundclip;
    

    //create references for player object and its script
    public GameObject playerSubmarine; //the spawner gets this for this script because stinky enemy prefab couldnt get the player prefab after it spawned 
    public SubMover submarineScript;

    //attack cooldown
    [SerializeField] float attackCD = 0;
    //this is so enemy can reference spawner variables
    public SpawnEnemy EnemySpawn;
    //i think this is a leftover but i  dont want to remove it

    //float distance = Vector2.Distance(transform.position, mousePos);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        soundnoise.GetComponent<AudioSource>().playOnAwake = true;
        //prefab slider shenanigans gone wrong. could've probably been avoided if I made a list for the spawned enemies
        //EHPBar = GetComponent<Slider>();
        //SpawnEnemy.GetComponent<SpawnEnemy>();
        //spawnedSubmarine = SpawnEnemy.spawnedPlayer;
        //SubScript = spawnedSubmarine.GetComponent<SubMover>();
        //EHPBar.maxValue = health;
        //EHPBar.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        //because spawner references player game object, its script can be grabbed
        submarineScript = playerSubmarine.GetComponent<SubMover>();
        
        //have this script only run if the enemy is alive
        if (health >= 0)
        {
            
            //I should've used a list :( to avoid headaches
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            //checking for collision of aimfire bullet prefab with this enemy
            float railDistance = Vector2.Distance(transform.position, mousePos);
            attackCD += Time.deltaTime;
            //enemy dist checks if the player and enemy have collided so the enemy can damage it
            float enemyDistance = Vector2.Distance(transform.position, playerSubmarine.transform.position);
            
            //if the enemy is close enough and attack is not on cooldown, damage the player
            if (enemyDistance < 3 && attackCD > 3f) 
            {
                soundnoise.Play();
                submarineScript.SubHealth -= danger;
                //0 means the attack is on cooldown
                attackCD = 0;
                //roundabout method for attack cd. It can attack every 3 seconds I think
                //basically I have the timer constantly increasing so that when the enemy is moving around its attack won't be on cooldown
                //only when it attacks does it go on cooldown
                //this does mean that it can't attack right away on spawn though
            }

            //get the player's position so that the enemy can "face" it. this took forever :(
            Vector2 pointing = playerSubmarine.transform.position - transform.position;
            transform.up = pointing;
            //make the enemy move in the direction its pointing
            transform.position += transform.up * speed * Time.deltaTime;
            //if the railgun projectile touches the enemy or gets close enough, the enemy takes damage
            //at least that was the original plan, but i couldnt figure out how to get a player's aimfire script and its game objects
            //to be referenced in the enemy prefab without it dissapearing
            //rail distance is actually just the mouse's position in the world screen space
            if (railDistance <= 5 && submarineScript.canShoot == true && Mouse.current.leftButton.wasReleasedThisFrame)
            {
                //if cursor is close enough to target, and left mouse button was clicked, damage it
                health -= submarineScript.damage;
            }

        }
        if (health <= 0)
        {//if enough damage is dealt, stop sound effects, update the enemy spawner tracker and destroy the enemy
            EnemySpawn.eTracker--;
            soundnoise.Stop();
            Destroy(gameObject, 1f);
        }
    }
}
