using UnityEngine;

public class FlyCam : MonoBehaviour
{
    float mainSpeed = 100.0f; //Regular speed of the camera
     
    float shiftAdd = 250.0f;  //Multiplied by how long shift is held

    float maxShift = 1000.0f; //Maximum speed when holding shift

    float camSens = 0.25f;    //Mouse sensitivity

    private Vector3 lastMouse = new Vector3(255, 255, 255); //Moves the mouse to middle of screen for full movement
    private float totalRun = 1.0f;

    void Update()
    {
        //Sets the position of the mouse to allow for full movement
        //places pointer in the centre of the screen and sets the sensitivity of the mouse movement
        lastMouse = Input.mousePosition - lastMouse;

        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);

        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);

        transform.eulerAngles = lastMouse;

        lastMouse = Input.mousePosition;

        //Shift command will increase the speed at which the camera travels
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Adds 250 to speed every second shift is help until maxShift is met
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }
        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
        transform.Translate(p);
    }
    private Vector3 GetBaseInput()
    {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);      // sets W to move forward 1 unit 
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);    // sets A to move left 1 unit
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);    // sets S to move back 1 unit 
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);     // sets D to move right 1 unit 
    }
        return p_Velocity;
    }
}

