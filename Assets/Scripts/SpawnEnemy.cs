using JetBrains.Annotations;//i think a lot of these are leftover, not really sure how they appeared
using System;//don't want to remove them incase it breaks stuff
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    //make sprites, audioclips,  and spawn timers for each enemy varient
    //dsc = deep sea creature, a = alien, naf = nefarious anglerfish
    public Sprite dscE;
    public Sprite aE;
    public Sprite nafE;
    public AudioClip dscS;
    public AudioClip aS;
    public AudioClip nafS;
    public GameObject pfEnemyType1;
    public GameObject spawnedET1;
    public EnemyType1 enemyScript;
    [SerializeField] float ESTimer1 = 0f;
    [SerializeField] float ESTimer2 = 0f;
    [SerializeField] float ESTimer3 = 0f;
    //bool to check if any enemies are active
    public bool activeE = false;
    //counter for each enemy
    int dscCount = 0;
    int aCount = 0;
    int nafCount = 0;
    public int eTracker = 0;

    //get player character prefab
    public GameObject prefabplayerSub;
    public GameObject spawnedPlayer;
    //originally the enemy spawner didn't spawn the player
    //the player object also wasnt originally a prefab, but then i tried brute forcing my way around making an enemy list
    
    //get the aim fire script from the player
    public AimFire railgunScript;
    //int enemyLimit = 0;
    //get health bar for hp shenanigans
    public Slider HealthBarPlayer;
    //get Main player script
    public SubMover GetPlayerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {//spawn player sub
        spawnedPlayer = Instantiate(prefabplayerSub);
        //get the player's movement script
        GetPlayerScript = spawnedPlayer.GetComponent<SubMover>();
        //get player's hp values and set up slider values
        HealthBarPlayer.maxValue = GetPlayerScript.SubHealth;
        HealthBarPlayer.value = GetPlayerScript.SubHealth;

    }

    // Update is called once per frame
    void Update()
    {
        //variable to track how many enemies have spawned
        //is a bit of a left over, used to be important, not anymore
        eTracker = dscCount + aCount + nafCount;

        HealthBarPlayer.value = GetPlayerScript.SubHealth;

        //activate every timer
        ESTimer1 += Time.deltaTime;
        ESTimer2 += Time.deltaTime;
        ESTimer3 += Time.deltaTime;
        //each spawner if statement requires a specific time and a limit to how many of each enemy type it will spawn
        //if time requirement met, calls spawner function, updates count of that creature, resets the timer
        if (ESTimer1 > 26 && dscCount < 3)
        {
            DeepSeaCreature();
            ESTimer1 = 0;
            dscCount++;
        }
        if (ESTimer2 > 15 && aCount < 8)
        {
            Alien();
            ESTimer2 = 0;
            aCount++;
        }
        if (ESTimer3 > 8 && nafCount < 15)
        {
            NefariousAnglerfish();
            ESTimer3 = 0;
            nafCount++;
        }
        
    }
    //all functions below are pretty much the same just with slightly changed hp, speed, damage, and sprites
    //not super effecient :(
    void DeepSeaCreature()
    {
        //make spawn position be random
        Vector2 spawnPos = Random.insideUnitCircle * 4;
        //gave an offset because i liked constraining the randomness a bit and giving a specific area to spawn
        spawnPos.y += 3;
        //spawn the creature with no rotation
        spawnedET1 = Instantiate(pfEnemyType1, spawnPos, Quaternion.identity);
        //get the spawned enemy's script so its values can be changed
        enemyScript = spawnedET1.GetComponent<EnemyType1>();
        enemyScript.health = 150f;
        enemyScript.speed = 6f;
        enemyScript.danger = 100f;
        enemyScript.GetComponent<SpriteRenderer>().sprite = dscE;
        enemyScript.GetComponent<AudioSource>().clip = dscS;
        enemyScript.soundnoise = enemyScript.GetComponent<AudioSource>();
        enemyScript.soundclip = dscS;
        enemyScript.soundnoise.Play();
        //give the prefab the player prefab so it can properly track it
        //I wish i looked up how to do this instead of just going at it
        //decided I wanted the enemies to track the player instead of moving down towards a barrier
        //thought that would be more interesting
        enemyScript.playerSubmarine = spawnedPlayer;

        //this was so the enemy could reduce the enemy tracker variable on its death
        //not really needed anymore but I'm afraid to delete it right now
        enemyScript.EnemySpawn = GetComponent<SpawnEnemy>();
    }
    void Alien()
    {
        Vector2 spawnPos = Random.insideUnitCircle * 4;
        spawnPos.y += 3;
        spawnedET1 = Instantiate(pfEnemyType1, spawnPos, Quaternion.identity);
        enemyScript = spawnedET1.GetComponent<EnemyType1>();
        enemyScript.health = 90f;
        enemyScript.speed = 8f;
        enemyScript.danger = 30f;
        enemyScript.GetComponent<SpriteRenderer>().sprite = aE;
        enemyScript.GetComponent<AudioSource>().clip = aS;
        enemyScript.soundnoise = enemyScript.GetComponent<AudioSource>();
        enemyScript.soundclip = aS;
        enemyScript.soundnoise.Play();
        enemyScript.playerSubmarine = spawnedPlayer;
        enemyScript.EnemySpawn = GetComponent<SpawnEnemy>();

    }

    void NefariousAnglerfish()
    {
        Vector2 spawnPos = Random.insideUnitCircle * 4;
        spawnPos.y += 3;
        spawnedET1 = Instantiate(pfEnemyType1, spawnPos, Quaternion.identity);
        enemyScript = spawnedET1.GetComponent<EnemyType1>();
        enemyScript.health = 75f;
        enemyScript.speed = 10f;
        enemyScript.danger = 17f;
        enemyScript.GetComponent<SpriteRenderer>().sprite = nafE;
        enemyScript.soundnoise = enemyScript.GetComponent <AudioSource>();
        enemyScript.GetComponent<AudioSource>().clip = nafS;
        enemyScript.soundclip = nafS;
        enemyScript.soundnoise.Play();
        enemyScript.playerSubmarine = spawnedPlayer;
        enemyScript.EnemySpawn = GetComponent<SpawnEnemy>();
    }
}