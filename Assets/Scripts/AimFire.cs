using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//this script is mostly reused code from the code gym tank challenge
public class AimFire : MonoBehaviour
{
    //create references for prefab projectile
    //one for the prefab, the other so you clone the prefab without cloning the prefab directly
    public GameObject prefabtankBullet;
    public GameObject spawnedBullet;
    //reference to grab the projectile's script (this is for later)
    public Move bulletScript;
    
    //damage the projectile will do
    public float damage = 50f;

    //audiojungle is for firing
    //audiojunjle is for charge sound
    //two audio sources so that they can happen without cutting eachother off
    //probably could've optomised with oneshot
    public AudioSource audiojungle;
    public AudioSource audiojunjle;
    public AudioClip firing;
    public AudioClip charging;
    //public int loaderLimit = 5;(leftover when i still planned/had time to add a reload button) 

    //limiter value so damage doesnt increase forever
    public float damageCap = 300f;
    //check if charging
    bool isCharging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get mouse pos so this gun can point at the mosue
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = mousePos-(Vector2)transform.position;

        //if damage hasnt reached cap, and the player is charging, damage can increase
        if (damage < damageCap && isCharging == true)
        {
            damage += 5 * Time.deltaTime;
        }//if damage has reached cap, you cannot increase damage anymore
        if (damage >= damageCap) { isCharging = false; }

        //point the gun in direction of mouse
        transform.up = direction;
        //charge damage and play charge sound on click
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            audiojungle.clip = charging;
            audiojungle.Play();
            isCharging = true;
            
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            //on release stop the charging sound and play the firing sound
            audiojungle.Stop();
            
            audiojunjle.PlayOneShot(firing);
            //If only I made this a list from the start :(, would've saved so many headaches

            //spawn the projectile 
            spawnedBullet = Instantiate(prefabtankBullet, transform.position, Quaternion.identity);
            //get the projectiles script
            bulletScript = spawnedBullet.GetComponent<Move>();
            //reference the projectiles transform position and set it to direction so the projectile points in the proper direction when fired
            bulletScript.transform.right = direction;
            //reset damage value
            damage = 20;
            //turn off charging
            isCharging = false;
        }
        
    }
}
