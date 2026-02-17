using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
//this exists becaause I went too off plan and ran out of time to properly add a reload button and other stuff
//also because I stupidly decided to make the player projectiles and enemies not be in their own lists
public class MonsterHunter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject BirdUP;
    //bool monsterBird = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()//i guess this doesnt really need to be here
    {

    }

    //spawn the funny image when the ui button is pressed
    public void birdUPUP() 
    {
        Vector2 pawnPos = Random.insideUnitCircle * 4;
        Instantiate(BirdUP, pawnPos, Quaternion.identity);
    }
}
