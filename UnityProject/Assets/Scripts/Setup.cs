using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    GameObject electricPotential;

    public int nX = 100;
    public int nY = 100;
    public int nZ = 100;

    public Material m_material;

    List<GameObject> meshes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        electricPotential = GameObject.Find("Electric Potential");

        PointCharge pointCharge = new PointCharge(new Vector3(1f,1f,1f), 1f);

        electricPotential.GetComponent<ElectricPotential>().RegisterCharge(pointCharge);
        electricPotential.GetComponent<ElectricPotential>().BuildScalarField(nX,nY,nZ);

        foreach (ScalarFieldPoint x in electricPotential.GetComponent<ElectricPotential>().scalarField )
        {
            print(x.potential);
        }

        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();

        int i = 0;

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
        mesh.SetVertices(vertices);
        mesh.SetTriangles(indices, 0);
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
