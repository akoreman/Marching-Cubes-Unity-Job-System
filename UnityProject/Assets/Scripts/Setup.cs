using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    GameObject electricPotential;

    public int nX = 10;
    public int nY = 10;
    public int nZ = 10;

    public float thresholdValue = 0.5f;

    public Material m_material;

    List<GameObject> meshes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //electricPotential = GameObject.Find("Electric Potential");
        electricPotential = this.gameObject;

        PointCharge pointCharge = new PointCharge(new Vector3(5f,5f,5f), 1f);

        electricPotential.GetComponent<ElectricPotential>().RegisterCharge(pointCharge);
        electricPotential.GetComponent<ElectricPotential>().BuildScalarField(nX,nY,nZ);

        /*
        foreach (ScalarFieldPoint x in electricPotential.GetComponent<ElectricPotential>().scalarField )
        {
            print(x.potential);
        }
        */

        List<Vector3> vertexList = new List<Vector3>();
        List<int> indexList = new List<int>();



        electricPotential.GetComponent<MarchingCubes>().GetVerticesFromField(electricPotential.GetComponent<ElectricPotential>().scalarField, thresholdValue, ref vertexList, ref indexList);





        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();


        vertices.Add(new Vector3(1f,2f,2f));
        vertices.Add(new Vector3(2f, 2f, 2f));
        vertices.Add(new Vector3(1f, 2f, 5f));
        vertices.Add(new Vector3(4f, 1f, 5f));

        indices.Add(2);
        indices.Add(1);
        indices.Add(0);
        indices.Add(3);
        indices.Add(1);
        indices.Add(2);


        Mesh mesh = new Mesh();
        //mesh.SetVertices(vertices);
        //mesh.SetTriangles(indices, 0);
        mesh.SetVertices(vertexList);
        mesh.SetTriangles(indexList, 0);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        GameObject go = new GameObject("Mesh");
        go.transform.parent = transform;
        go.AddComponent<MeshFilter>();
        go.AddComponent<MeshRenderer>();
        go.GetComponent<Renderer>().material = m_material;
        go.GetComponent<MeshFilter>().mesh = mesh;
        go.transform.localPosition = new Vector3(0f, 0f, 0f);

        meshes.Add(go);


    }

}
