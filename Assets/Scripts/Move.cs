using UnityEngine;
//script from gym repurposed kinda
public class Move : MonoBehaviour
{
    //left overs from me trying to get the railgun projectile to damage the enemies instead of the enemies checking if the railgun damages them
    //public GameObject enemylocation;
    //public EnemyType1 ET1Script;
    //public GameObject spawnedET;
    //public EnemyType1 enemyShotScript;
    //public SpawnEnemy seScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //destroys itself after some time to save memory
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        //moves in the direction its pointing
        transform.position += transform.right * 100 * Time.deltaTime;
        //more leftovers from the same thing i described earlier
        //enemyShotScript = spawnedET.GetComponent<EnemyType1>();
        //float distance = Vector2.Distance(transform.position, spawnedET.transform.position);
        //if (distance < 1) 
        //{
          //  enemyShotScript.health -= damage;
        //}

    }
}
