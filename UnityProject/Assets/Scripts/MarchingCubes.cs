using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCubes : MonoBehaviour
{
    GameObject electricPotential;

    public void GetVerticesFromField (List<ScalarFieldPoint> scalarField, float thresholdValue)
    {
        electricPotential = GameObject.Find("Electric Potential");

        List<flagNode> flagList = new List<flagNode>();
        List<Vector3> vertexList = new List<Vector3>();


        foreach (ScalarFieldPoint x in electricPotential.GetComponent<ElectricPotential>().scalarField)
        {
            print(x.potential);

            if (x.potential > thresholdValue)
                flagList.Add(new flagNode(true, x.position));
            else
                flagList.Add(new flagNode(false, x.position));
        }

        //return vertexList;
    }


}

public class flagNode
{
    public bool flag;
    public Vector3 position;

    public flagNode(bool flag, Vector3 position)
    {
        this.flag = flag;
        this.position = position;
    }
}
