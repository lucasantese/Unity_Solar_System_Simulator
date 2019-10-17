using UnityEngine;
using System.Collections;
using System;

public class PlanetCameraSwitch : MonoBehaviour
{
    //sets an array for all the cameras in the scene that can be added to in the scene view
    public GameObject[] cameraList;

    public int lastCamera;

    void Start()
    {
        //Disables cameras until user chooses one
        foreach (GameObject camera in cameraList)
        {

            camera.SetActive(false);

        }
    }

    //changes the camera to the same key pressed as the position in the array
    void Update()
    {

        //Toggles free camera to active from the start
        cameraList[0].SetActive(true);

        if (Input.anyKey)
        {

            for (int i = 0; i < cameraList.Length; i++)
            {
                try
                {

                    //Activate chosen camera and disable the last camera used 
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

    }
}

