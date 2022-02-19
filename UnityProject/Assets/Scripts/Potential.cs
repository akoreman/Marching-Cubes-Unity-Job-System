using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the construction and updating of the scalar field.

public class Potential : MonoBehaviour
{
    List<PointCharge> chargesInScene = new List<PointCharge>();
    public List<ScalarFieldPoint> scalarField = new List<ScalarFieldPoint>();

    public float fieldExponent = 1.0f;

    public void RegisterCharge(PointCharge input)
    {
        chargesInScene.Add(input);
    }

    public void RemoveCharge(PointCharge input)
    {
        chargesInScene.Remove(input);
    }

    // Flatten the 3D scalar field into a 1D array.
    public void BuildScalarField(int nX, int nY, int nZ, float gridSize)
    {
        scalarField.Clear();

        for (int i = 0; i < nX; i++)
            for (int j = 0; j < nY; j++)
                for (int k = 0; k < nZ; k++)
                {
                    scalarField.Add(new ScalarFieldPoint(this, new Vector3(i * gridSize, j * gridSize, k * gridSize)));
                }
    }

    public float GetPotential(Vector3 Position)
    {
        float potential = 0;

        // Choose between 1/r and 1/r^2 drop-off.
        foreach (PointCharge x in chargesInScene)
        {
            potential += x.charge / Mathf.Pow((Position - x.position).magnitude, fieldExponent);
        }

        return potential;
    }

}

public class ScalarFieldPoint
{
    public Vector3 position;
    public float potential;

    public ScalarFieldPoint(Potential potential, Vector3 position)
    {
        this.position = position;
        this.potential = potential.GetPotential(this.position);
    }
}


public class PointCharge
{
    public Vector3 position;
    public float charge;

    public PointCharge(Vector3 position, float charge)
    {
        this.position = position;
        this.charge = charge;
    }
}