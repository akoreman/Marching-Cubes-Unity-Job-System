using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCubes : MonoBehaviour
{
    GameObject electricPotential;
    int nX;
    int nY;
    int nZ;
    //GameObject setup;

    public void GetVerticesFromField (List<ScalarFieldPoint> scalarField, float thresholdValue, ref List<Vector3> vertexList, ref List<int> indexList)
    {
        electricPotential = GameObject.Find("Electric Potential");
        //setup = GameObject.Find("Setup");

        nX = electricPotential.GetComponent<Setup>().nX;
        nY = electricPotential.GetComponent<Setup>().nY;
        nZ = electricPotential.GetComponent<Setup>().nZ;

        List<flagNode> flagList = new List<flagNode>();

        foreach (ScalarFieldPoint x in electricPotential.GetComponent<ElectricPotential>().scalarField)
        {
            //print(x.potential);

            if (x.potential > thresholdValue)
                flagList.Add(new flagNode(true, x.position));
            else
                flagList.Add(new flagNode(false, x.position));
        }

        //int index = 0;

        flagNode[] cube = new flagNode[8];

        int offset = 0;

        for (int i = 0; i < nX - 1; i++)
            for (int j = 0; j < nY - 1; j++)
                for (int k = 0; k < nZ - 1; k++)
                {
                    uint cubeIndex = 0;

                    // a |= b shorthand for a = a | b with | the bitwise OR operator.
                    if (flagList[GetLinearIndex(i, j, k)].flag) { cubeIndex |= 1; }
                    if (flagList[GetLinearIndex(i + 1, j, k)].flag) { cubeIndex |= 2; }
                    if (flagList[GetLinearIndex(i + 1, j + 1, k)].flag) { cubeIndex |= 4; }
                    if (flagList[GetLinearIndex(i + 1, j + 1, k + 1)].flag) { cubeIndex |= 8; }
                    if (flagList[GetLinearIndex(i + 1, j, k + 1)].flag) { cubeIndex |= 16; }
                    if (flagList[GetLinearIndex(i, j + 1, k)].flag) { cubeIndex |= 32; }
                    if (flagList[GetLinearIndex(i, j + 1, k + 1)].flag) { cubeIndex |= 64; }
                    if (flagList[GetLinearIndex(i, j, k + 1)].flag) { cubeIndex |= 128; }

                    if (cubeIndex != 0 && cubeIndex != 255)
                    {
                        print(cubeIndex);

                        vertexList.Add(flagList[GetLinearIndex(i, j, k)].position);
                        vertexList.Add(flagList[GetLinearIndex(i , j + 1, k)].position);
                        vertexList.Add(flagList[GetLinearIndex(i, j, k + 1)].position);

                        indexList.Add(offset + 2);
                        indexList.Add(offset + 0);
                        indexList.Add(offset + 1);

                        offset += 3;
                    }
                }

    }

    public void GetVerticesFromCube (int cubeIndex, ref List<Vector3> vertexList, ref List<Vector3> indexList)
    {

    }

    public int GetLinearIndex (int i, int j, int k)
    {
        int x = i* nY * nZ + j * nZ+ k;

        return x;
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
