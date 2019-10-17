using UnityEngine;
using UnityEngine.SceneManagement;

public class ScaleChanger : MonoBehaviour {

    //Array with all the planets in 
    public GameObject[] Planets;

    //Difference in distances between scaled and unsclaed
    public float[] Distances;

    //Difference in size of planets
    public Vector3[] Scale;

    //Difference in mass
    public float[] ScaledMasses;
    public GameObject Moon;
    public bool Scaled;

    //Spawn locations
    public Transform[] teleport;

    //Asteroid models
    public GameObject[] prefeb;

    //Check to see if asteroids can spawn
    public bool CanSpawn;

    void Update ()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Speed up passage of time
            Time.timeScale = Time.timeScale + 0.01f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Slow passage of time
            Time.timeScale = Time.timeScale - 0.01f;
        }

        if (Input.GetKey(KeyCode.C))
        {
            SceneManager.LoadScene("CalculationScene");
        }

        //Where the asteroid will spawn
        int tele_num = Random.Range(0, 8);

        //Which asteroid will spawn
        int prefeb_num = Random.Range(0, 2);

        //Whether it will spawn
        int probability = Random.Range(0, 20);

        //Only chose one number rather than range so it is not constaly spawning asteroids
        if (probability < 12 && probability > 8 && CanSpawn == true)
        {
            //Moves prefab to a set position and rotation
            Instantiate(prefeb[prefeb_num], teleport[tele_num].position, teleport[tele_num].rotation);
        }
    }

    void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !Scaled)
        {
            for (int i = 0; i < 9; i++)
            {
                //Increase distance from sun by multiplying it by the preset number
                Planets[i].transform.position = Planets[i].transform.position * Distances[i];

                //Decrease size by adding the preset value to the Vector3
                Planets[i].transform.localScale += Scale[i];

                //Fetches rigidbody component from planet
                Rigidbody rb = Planets[i].GetComponent<Rigidbody>();

                //Add value stored in array to the mass
                rb.mass += ScaledMasses[i];

                //Change moon's scale
                Moon.transform.localScale = new Vector3(0.101f, 0.101f, 0.101f);
            }
            Scaled = true;
            CanSpawn = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && Scaled)
        {
            for (int i = 0; i < 9; i++)
            {
                //Decrease distance from sun by dividing it by the preset number
                Planets[i].transform.position = Planets[i].transform.position / Distances[i];

                //Increase size by subtracting the preset value to the Vector3
                Planets[i].transform.localScale -= Scale[i];

                //Fetches rigidbody component from planet
                Rigidbody rb = Planets[i].GetComponent<Rigidbody>();

                //Subtract value stored in array to the mass
                rb.mass -= ScaledMasses[i];

                //Change moon's scale
                Moon.transform.localScale = new Vector3(0.17f, 0.17f, 0.17f);
            }
            Scaled = false;
            CanSpawn = false;
        }

	}
}
