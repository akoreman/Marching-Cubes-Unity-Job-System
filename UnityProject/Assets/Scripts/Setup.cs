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

    public Material meshMaterial;

    List<GameObject> meshes = new List<GameObject>();

    Mesh mesh;
    GameObject meshGameObject;

    void Start()
    {
        marchingCubes = this.gameObject;
        marchingCubes.GetComponent<Potential>().BuildScalarField(nX,nY,nZ, gridSize);
    }

    void  Update()
    {
        marchingCubes.GetComponent<Potential>().BuildScalarField(nX, nY, nZ, gridSize);

        List<Vector3> vertexList = new List<Vector3>();
        List<Vector3> normalList = new List<Vector3>();
        List<int> indexList = new List<int>();
        Dictionary<Vector3, int> vertexDict = new Dictionary<Vector3, int>();

        marchingCubes.GetComponent<MarchingCubes>().GetVerticesFromField(marchingCubes.GetComponent<Potential>().scalarField, thresholdValue, ref vertexList, ref indexList, ref normalList, ref vertexDict);

        Mesh mesh = new Mesh();

        mesh.SetVertices(vertexList);
        mesh.SetTriangles(indexList, 0);
        mesh.normals = NormalizedArrayFromList(normalList);

        //mesh.RecalculateNormals();

        Destroy(meshGameObject);

        meshGameObject = new GameObject("Marching Cubes Mesh");
        meshGameObject.transform.parent = transform;
        meshGameObject.AddComponent<MeshFilter>();
        meshGameObject.AddComponent<MeshRenderer>();
        meshGameObject.GetComponent<Renderer>().material = meshMaterial;
        meshGameObject.GetComponent<MeshFilter>().mesh = mesh;
        //meshGameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

        meshes.Add(meshGameObject);

    }

    Vector3[] NormalizedArrayFromList(List<Vector3> input)
    {
        Vector3[] output = new Vector3[input.Count];

        for (int i = 0; i < input.Count; i++)
        {
            Vector3 normal = new Vector3();
            normal.x = input[i].x;
            normal.y = input[i].y;
            normal.z = input[i].z;

            //print(input[i]);

            output[i] = normal.normalized;
        }

        return output;
    }
}
