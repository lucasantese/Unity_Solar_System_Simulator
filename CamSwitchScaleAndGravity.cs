using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CamSwitchScaleAndGravity : MonoBehaviour {

    //CamSwitch
    public GameObject[] cameraList;
    public int lastCamera;

    //Gravity
    public GameObject Sun;

    //Scale
    public GameObject[] Planets;
    public bool Scaled;
    public float[] Distances;
    public Vector3[] Scale;
    public float[] ScaledMasses;
    public GameObject Moon;

    void Start ()
    {

        //CamSwitch
        foreach (GameObject camera in cameraList)
        {
            camera.SetActive(false);
        }

        //Gravity
        GameObject[] Objects = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject ObjectA in Objects)
        {
            Vector3 movement = ObjectA.GetComponent<GravitySpecs>().movement;
            ObjectA.GetComponent<Rigidbody>().AddForce(movement);
        }


    }
	void Update ()
    {

        //CamSwtich
        cameraList[0].SetActive(true);

        if (Input.anyKey /*&& Scaled == true*/)
        {
            for (int i = 0; i < cameraList.Length; i++)
            {
                try
                {
                    if (Convert.ToInt32(Input.inputString) == i)
                    {
                        cameraList[lastCamera].SetActive(false);
                        cameraList[i].SetActive(true);
                        lastCamera = i;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        //Scale
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Time.timeScale = Time.timeScale + 0.01f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Time.timeScale = Time.timeScale - 0.01f;
        }

        if (Input.GetKey(KeyCode.C))
        {
            List<string> planetPostiions = new List<string>();
            for (int planetIndex = 0; planetIndex < Planets.Length; planetIndex++)
            {
                //Formats Vector3 so it is able to be read in the other script
                planetPostiions.Add(Planets[planetIndex].transform.position.ToString().Replace(",", ":").Replace("(","").Replace(")",""));
            }
            //Establishes CSV file path
            CSV csv = new CSV("C:/Users/user/Desktop/Positions.csv");
            csv.Table[0].list = planetPostiions;
            //Saves values to the CSV
            csv.Save("C:/Users/user/Desktop/Positions.csv");
            //Loads next scene
            SceneManager.LoadScene("CalculationsScene");
        }
    }

    //Gravity
    void ApplyGravity(Rigidbody A, Rigidbody B)
    {
        Vector3 dist = B.transform.position - A.transform.position;
        float r = dist.magnitude;
        dist /= r;
        //Newton's Equation
        double G = 6.674f * (10 ^ 11);
        float force = ((float)G * A.mass * B.mass) / (r * r);
        A.AddForce(dist * force);
    }

    void FixedUpdate()
    {
        //Gravity
        if (Sun == null)
        {
            Sun = GameObject.Find("Sun");
        }

        GameObject[] Objects = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject ObjectA in Objects)
        {
            ApplyGravity(ObjectA.GetComponent<Rigidbody>(), Sun.GetComponent<Rigidbody>());
        }

        //Scale
        if (Input.GetKey(KeyCode.UpArrow) && !Scaled)
        {
            for (int i = 0; i < 9; i++)
            {
                Planets[i].transform.position = Planets[i].transform.position * Distances[i];
                Planets[i].transform.localScale += Scale[i];
                Rigidbody rb = Planets[i].GetComponent<Rigidbody>();
                rb.mass += ScaledMasses[i];
                Moon.transform.localScale = new Vector3(0.101f, 0.101f, 0.101f);
            }
            Scaled = true;
        }

        if (Input.GetKey(KeyCode.DownArrow) && Scaled)
        {
            for (int i = 0; i < 9; i++)
            {
                Planets[i].transform.position = Planets[i].transform.position / Distances[i];
                Planets[i].transform.localScale -= Scale[i];
                Rigidbody rb = Planets[i].GetComponent<Rigidbody>();
                rb.mass -= ScaledMasses[i];
                Moon.transform.localScale = new Vector3(0.17f, 0.17f, 0.17f);
            }
            Scaled = false;
        }
    }
}
