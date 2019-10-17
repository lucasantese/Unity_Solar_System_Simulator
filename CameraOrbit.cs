using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    //Set target planet in scene view
    public Transform target;

    //Set camera distance from planet 
    public float distance = 10f;

    //Set speed the camera will orbit horizontaly
    public float xSpeed = 50.0f;

    //Set speed the camera will orbit vertically
    public float ySpeed = 50.0f;

    //Lowest point camera can go from centre 
    public float yMinLimit = -80f;

    //Highest point camera can go from centre
    public float yMaxLimit = 80f;


    float x = 0.0f;
    float y = 0.0f;

    void LateUpdate()
    {
        if (target)
        {
            //Uses the current position of the camera and the speed at which it moves
            //to update the new position of the camera around the planet
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            //Calls function to make sure the camera stays within the defined limits
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);

            Vector3 position = rotation * negDistance + target.position;

            //Sets new rotation and position values for the cameras
            transform.rotation = rotation;
            transform.position = position;
        }
    }
    //Function to keep camera within limits
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}


