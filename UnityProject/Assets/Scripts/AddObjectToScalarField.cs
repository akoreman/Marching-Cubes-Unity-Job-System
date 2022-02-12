using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this script to gameObjects which have to have a scalar charge.

public class AddObjectToScalarField : MonoBehaviour
{
    GameObject marchingCubes;

    public float charge = 1f;

    PointCharge pointCharge;

    void Start()
    {
        marchingCubes = GameObject.Find("Marching Cubes");

        pointCharge = new PointCharge(this.transform.position, charge);

        marchingCubes.GetComponent<Potential>().RegisterCharge(pointCharge);
    }

    void Update()
    {
        marchingCubes.GetComponent<Potential>().RemoveCharge(pointCharge);

        pointCharge = new PointCharge(this.transform.position, charge);

        marchingCubes.GetComponent<Potential>().RegisterCharge(pointCharge);
    }
}
