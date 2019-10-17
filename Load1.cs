using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load1 : MonoBehaviour {

    public Dropdown Dropdown1;
    public Dropdown Dropdown2;
    public Camera[] Cameras;

    void Start () {

        //Called when value changed, this called the dropdown function
        Dropdown1.onValueChanged.AddListener(delegate
        {
            Dropdown1ValueChanged(Dropdown1);
        });

        //Called when value changed, this called the dropdown function
        Dropdown2.onValueChanged.AddListener(delegate
        {
            Dropdown2ValueChanged(Dropdown2);
        });
    }

    public void Dropdown1ValueChanged(Dropdown target)
    {
        int d1 = Dropdown1.value;
        int d2 = Dropdown2.value;
        //Brings camera to front
        Cameras[d1].depth += 1;
        //Moves camera view to left side of the display
        Cameras[d1].rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
    }

    public void Dropdown2ValueChanged(Dropdown target)
    {
        int d2 = Dropdown2.value;
        int d1 = Dropdown1.value;
        Cameras[d2].depth = Cameras[d2].depth + 1;
        //Brings camera to front
        Cameras[d2].rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
        //Moves camera view to left side of the display
    }

}
