using UnityEngine;

public class AstroidSpawn : MonoBehaviour
{
    //Spawn locations
    public Transform[] teleport;

    //Asteroid models
    public GameObject[] prefeb;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Where the asteroid will spawn
        int tele_num = Random.Range(0, 8);

        //Which asteroid will spawn
        int prefeb_num = Random.Range(0, 2);

        //Whether it will spawn
        int probability = Random.Range(0, 20);

        //Only chose one number rather than range so it is not constaly spawning asteroids
        if (probability == 10)
        {
            //Moves prefab to a set position and rotation
            Instantiate(prefeb[prefeb_num], teleport[tele_num].position, teleport[tele_num].rotation);
        }
    }
}

