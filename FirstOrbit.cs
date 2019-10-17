using UnityEngine;

public class FirstOrbit : MonoBehaviour
{
    //Set centre of orbit
    public Vector3 center = new Vector3(0, 0, 0);
    //Set Radius in x plane   
    public float radiusA = 10;
    //Set Radius in z plane                     
    public float radiusB = 10;
    //Set Speed of Orbit                      
    public float speed = 1;
    //Set variable angle for later use
    private float angle;

    void Start()
    {

    }
    void Update()
    {
        /*This function will find then next coordinate for the planet 
        in a circular orbit every frame using trigonometry*/
        angle += speed * Time.deltaTime;

        float x = radiusA * Mathf.Cos(angle);

        float z = radiusB * Mathf.Sin(angle);

        //Updates position realative to variable 'center'
        transform.position = center + new Vector3(x, 0, z);
    }
}



