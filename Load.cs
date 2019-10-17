using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class Load : MonoBehaviour {

    public Dropdown Dropdown1;
    public Dropdown Dropdown2;
    public Camera[] Cameras;
    public Text LeftText;
    public Text RightText;
    public Text CentreText;
    public float G = (6.674E-11f);

    //Create a list for all the values in CSV file
    public List<string> mass;
    public List<string> diameter;
    public List<string> gravity;
    public List<string> distancefromsun;
    public List<string> velocity;

    //Defines CSV file path
    CSV csv1 = new CSV("C:/Users/user/Desktop/specs.csv");
    CSV csv2 = new CSV("C:/Users/user/Desktop/Positions.csv");


    void Start() {
        //Populates lists with values from first CSV file
        mass = csv1.GetColumn("mass kg");
        diameter = csv1.GetColumn("diameter m");
        gravity = csv1.GetColumn("gravity m/s/s");
        distancefromsun = csv1.GetColumn("distance from sun m");
        velocity = csv1.GetColumn("velocity m/s");

        Dropdown1.onValueChanged.AddListener(delegate 
        {
            Dropdown1ValueChanged(Dropdown1);
        });

        Dropdown2.onValueChanged.AddListener(delegate 
        {
            Dropdown2ValueChanged(Dropdown2);
        });        
    }

    public void Dropdown1ValueChanged(Dropdown target)
    {
        //Creates int value to ease coding
        int d1 = Dropdown1.value;
        int d2 = Dropdown2.value;

        //Moves camera display to set position
        Cameras[d1].depth += 1;
        Cameras[d1].rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);

        //Creates list of Vector3 values
        List<Vector3> positions = 
            csv2.GetColumn("positions").Select(x => new Vector3(float.Parse(x.Split(':')[0]), float.Parse(x.Split(':')[1]), float.Parse(x.Split(':')[2]))).ToList<Vector3>();
        
        //Sets distance Vector3 as a variable r
        double r = (Vector3.Distance(positions[Dropdown1.value], positions[Dropdown2.value])) * 100000;

        //Calculates Angualr velocity of left planet
        double AvAngularVelocity1 = (Convert.ToDouble(velocity[d1])) / (Convert.ToDouble(distancefromsun[d1]));

        //Calculates Gravitational potential at planet surface
        double GravPotential1 = (-(G * Convert.ToDouble(mass[d1])) / (Convert.ToDouble(diameter[d1]) / 2));

        //Calculated gravitational field strength at surface
        double FiledStrength1 = ((G * Convert.ToDouble(mass[d1])) / ((Convert.ToDouble(diameter[d1]) / 2) * Convert.ToDouble(diameter[d1])));

        //Calculates centrapetal force of planet's orbit
        double CentrapetalForce1 = 
            ((Convert.ToDouble(mass[d1]) * (Convert.ToDouble(velocity[d1]) * Convert.ToDouble(velocity[d1])) / Convert.ToDouble(distancefromsun[d1])));

        //Calculates force of attraction between both planets at position when scene was changed
        double ForceofAttraction1 = (G * Convert.ToDouble(mass[d1]) * Convert.ToDouble(mass[d2])) / (r * r);

        //Sets middle textbox to Force of attraction
        CentreText.text = "Force of Attraction: " + Convert.ToString(ForceofAttraction1);

        //Sets left text to all the other values calculated inculding their units
        LeftText.text = "Average Angular Velocity: " + Convert.ToString(AvAngularVelocity1) + " m/s" 
            + "\nGravitational Potential at Surface: " + Convert.ToSingle(GravPotential1) + " J/Kg" 
            + "\nGavitational Field Strength at Surface: " + Convert.ToSingle(FiledStrength1) + " N/Kg" 
            + "\nAverage Centrapetal Force: " + Convert.ToString(CentrapetalForce1) + " N"; 
    }    
    public void Dropdown2ValueChanged(Dropdown target)
    {
        //Creates int value to ease coding
        int d1 = Dropdown1.value;
        int d2 = Dropdown2.value;

        //Moves camera display to set position
        Cameras[d2].depth = Cameras[d2].depth + 1;
        Cameras[d2].rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);

        //Creates list of Vector3 values
        List<Vector3> positions = 
            csv2.GetColumn("positions").Select(x => new Vector3(float.Parse(x.Split(':')[0]), float.Parse(x.Split(':')[1]), float.Parse(x.Split(':')[2]))).ToList<Vector3>();
        
        //Sets distance Vector3 as a variable r
        double r = (Vector3.Distance(positions[Dropdown1.value], positions[Dropdown2.value])) * 100000;

        //Calculates Angualr velocity of right planet
        double AvAngularVelocity2 = (Convert.ToDouble(velocity[d2])) / (Convert.ToDouble(distancefromsun[d2]));

        //Calculates Gravitational potential at planet surface
        double GravPotential2 = (-(G * Convert.ToDouble(mass[d2])) / (Convert.ToDouble(diameter[d2]) / 2));

        //Calculated gravitational field strength at surface
        double FiledStrength2 = ((G * Convert.ToDouble(mass[d2])) / ((Convert.ToDouble(diameter[d2]) / 2) * (Convert.ToDouble(diameter[d2]) / 2)));

        //Calculates centrapetal force of planet's orbit
        double CentrapetalForce2 = 
            ((Convert.ToDouble(mass[d2]) * (Convert.ToDouble(velocity[d2]) * Convert.ToDouble(velocity[d2])) / Convert.ToDouble(distancefromsun[d2])));

        //Calculates force of attraction between both planets at position when scene was changed
        double ForceofAttraction2 = (G * Convert.ToDouble(mass[d1]) * Convert.ToDouble(mass[d2])) / r * r;

        //Sets middle textbox to Force of attraction
        CentreText.text = "Force of Attraction: " + Convert.ToString(ForceofAttraction2);

        //Sets right text to all the other values calculated inculding their units
        RightText.text = "Average Angular Velocity: " + Convert.ToString(AvAngularVelocity2) + " m/s" 
            + "\nGravitational Potential at Surface: " + Convert.ToSingle(GravPotential2) + " J/Kg"
            + "\nGavitational Field Strength at Surface: " + Convert.ToSingle(FiledStrength2) + " N/Kg" 
            + "\nAverage Centrapetal Force: " + Convert.ToString(CentrapetalForce2) + " N";
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.C))
        {
            SceneManager.LoadScene("SimulatorV3");
        }
    } 

}


