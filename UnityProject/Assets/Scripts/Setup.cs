using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script sets up the field and updates the vertices.

public class Setup : MonoBehaviour
{
    GameObject marchingCubes;

    public int nX = 10;
    public int nY = 10;
    public int nZ = 10;

    public float gridSize = 1f;

    public float thresholdValue = 0.5f;

    public Material m_material;

    List<GameObject> meshes = new List<GameObject>();

    Mesh mesh;
    GameObject go;

    void Start()
    {
        marchingCubes = this.gameObject;

        marchingCubes.GetComponent<Potential>().BuildScalarField(nX,nY,nZ, gridSize);
    }

    void  Update()
    {
        marchingCubes.GetComponent<Potential>().BuildScalarField(nX, nY, nZ, gridSize);

        List<Vector3> vertexList = new List<Vector3>();
        List<Vector4> normalList = new List<Vector4>();
        List<int> indexList = new List<int>();
        Dictionary<Vector3, int> vertexDict = new Dictionary<Vector3, int>();

        marchingCubes.GetComponent<MarchingCubes>().GetVerticesFromField(marchingCubes.GetComponent<Potential>().scalarField, thresholdValue, ref vertexList, ref indexList, ref normalList, ref vertexDict);

        Mesh mesh = new Mesh();

        mesh.SetVertices(vertexList);
        mesh.SetTriangles(indexList, 0);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        Destroy(go);

        go = new GameObject("Mesh");
        go.transform.parent = transform;
        go.AddComponent<MeshFilter>();
        go.AddComponent<MeshRenderer>();
        go.GetComponent<Renderer>().material = m_material;
        go.GetComponent<MeshFilter>().mesh = mesh;
        go.transform.localPosition = new Vector3(0f, 0f, 0f);

        meshes.Add(go);

    }

}
