using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPotential : MonoBehaviour
{
    List<PointCharge> chargesInScene = new List<PointCharge>();
    public List<ScalarFieldPoint> scalarField = new List<ScalarFieldPoint>();


    public void RegisterCharge(PointCharge input)
    {
        chargesInScene.Add(input);
    }

    public void BuildScalarField(int nX, int nY, int nZ)
    {
        int index = 0;
        scalarField.Clear();

        for(int i = 0; i < nX; i++)
            for (int j = 0; j < nY; j++)
                for (int k = 0; k < nZ; k++)
                {
                    scalarField.Add(new ScalarFieldPoint(this, index, new Vector3Int(i, j, k)));
                }
    }

    public float Potential(Vector3 Position)
    {
        float potential = 0;

        foreach(PointCharge x in chargesInScene)
        {
            potential += x.charge / (Position - x.position).magnitude;
        }

        return potential;
    }

}

public class ScalarFieldPoint
{
    public int index;
    public Vector3Int position;
    public float potential;

    public ScalarFieldPoint(ElectricPotential electricPotential, int index, Vector3Int position)
    {
        this.index = index;
        this.position = position;
        this.potential = electricPotential.Potential(this.position);
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
