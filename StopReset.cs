using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StopReset : MonoBehaviour {

    //Create arrays for previous speeds and postions and also for the planets
    public Vector3[] oldPositions;
    public Vector3[] oldVelocities;
    public GameObject[] planets;
    public string[] names;

	void Start () {

        //Stops planets destroying on the load of a new scene
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < 9; i++)
        {
            names[i] = planets[i].gameObject.name;
        }
    }

	void LateUpdate ()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {

            for (int i = 0; i < 9; i++)
            {

                //Saves the current position of the planet in the array
                oldPositions[i] = planets[i].transform.position;

                //Saves current speed using the properties from the ridgidbody
                oldVelocities[i] = planets[i].GetComponent<Rigidbody>().velocity;
            }

            //Finally loads condenced scene
            SceneManager.LoadScene("SimNotScaled");
            GetPlanets();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {

            for (int i = 0; i < 9; i++)
            {

                //Saves the current position of the planet in the array
                oldPositions[i] = planets[i].transform.position;

                //Saves current speed using the properties from the ridgidbody
                oldVelocities[i] = planets[i].GetComponent<Rigidbody>().velocity;
            }
            //Loads scaled scene
            SceneManager.LoadScene("SolarSystemSimulator");
            GetPlanets();
        }
        
	}

    //When this function is called it finds all the planets in the scene and 
    //changes their postions to the postion saved in the arrays for postion and velocity
    public void GetPlanets()
    {

        for (int j = 0; j < 9; j++)
        {

            planets[j] = GameObject.Find(names[j]);
            planets[j].transform.position = oldPositions[j];
            planets[j].GetComponent<Rigidbody>().velocity = oldVelocities[j];
        }
    }
}

