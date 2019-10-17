using UnityEngine;

public class Gravity : MonoBehaviour
{
    //Creates variable which can be set in scene view
    public GameObject Sun;

    void Start()
    {
        //Creates array of all objects in scene with the tag "Planet"
        GameObject[] Objects = GameObject.FindGameObjectsWithTag("Planet");

        foreach (GameObject ObjectA in Objects)
        {

            //Creates Vector3 from the variabe saved in GravitySpecs 
            Vector3 movement = ObjectA.GetComponent<GravitySpecs>().movement;

            //Adds the movement vector to the ridgidbody attached to the planet
            ObjectA.GetComponent<Rigidbody>().AddForce(movement);
        }
    }
    void ApplyGravity(Rigidbody A, Rigidbody B)
    {
        //Distance between sun and planet in vector form
        Vector3 dist = B.transform.position - A.transform.position;

        //Magnitude of distance vector
        float r = dist.magnitude;

        dist /= r;
        //Newton's Equation'
        double G = 6.674f * (10 ^ 11);
        float force = ((float)G * A.mass * B.mass) / (r * r);

        //Add force of attraction to planet
        A.AddForce(dist * force);
    }
    void FixedUpdate()
    {
        //If variable sun is not set in scene the on=bject will be found automatically
        if (Sun == null)
        {
            Sun = GameObject.Find("Sun");
        }

        //Make sure the array contains the planets
        GameObject[] Objects = GameObject.FindGameObjectsWithTag("Planet");

        //Calls ApplyGravity function for each planet and sun
        foreach (GameObject ObjectA in Objects)
        {
            ApplyGravity(ObjectA.GetComponent<Rigidbody>(), Sun.GetComponent<Rigidbody>());
        }
    }
}

