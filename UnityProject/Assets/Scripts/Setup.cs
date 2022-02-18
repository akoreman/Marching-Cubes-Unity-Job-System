using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Jobs;
using Unity.Collections;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;

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

    public bool vertexWelding = true;

    List<GameObject> meshes = new List<GameObject>();

 
    [Unity.Collections.WriteOnly]
    public NativeQueue<Triangle> triangleQueue;

    List<Vector3> vertexList;
    Dictionary<Vector3, int> vertexDict;
    List<Vector3> normalList;
    List<int> indexList;

    Mesh mesh;
    GameObject meshGameObject;

    void Start()
    {
        marchingCubes = this.gameObject;
        marchingCubes.GetComponent<Potential>().BuildScalarField(nX,nY,nZ, gridSize);

        UpdateMesh();
    }

    void Update()
    {
        UpdateMesh();
    }

    void LateUpdate()
    {
        marchingCubes.GetComponent<MarchingCubes>().triangleListModificationJobHandle.Complete();
        CreateVertexIndexNormalListsFromTriangles(triangleQueue, ref vertexList, ref indexList, ref normalList, ref vertexDict);

        triangleQueue.Dispose();

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

    void  UpdateMesh()
    {
        marchingCubes.GetComponent<Potential>().BuildScalarField(nX, nY, nZ, gridSize);

        vertexList = new List<Vector3>();
        vertexDict = new Dictionary<Vector3, int>();

        normalList = new List<Vector3>();
        indexList = new List<int>();

        //List<Triangle> triangleList = new List<Triangle>();

        triangleQueue = new NativeQueue<Triangle>(Allocator.Persistent);

        //marchingCubes.GetComponent<MarchingCubes>().GetVerticesFromField(marchingCubes.GetComponent<Potential>().scalarField, thresholdValue, ref vertexList, ref indexList, ref normalList, ref vertexDict);
        //marchingCubes.GetComponent<MarchingCubes>().GetVerticesFromField(marchingCubes.GetComponent<Potential>().scalarField, thresholdValue, ref triangleList);

        marchingCubes.GetComponent<MarchingCubes>().GetVerticesFromField(marchingCubes.GetComponent<Potential>().scalarField, thresholdValue);

        //Mesh mesh = new Mesh();


        //marchingCubes.GetComponent<MarchingCubes>().triangleListModificationJobHandle.Complete();
        //CreateVertexIndexNormalListsFromTriangles(triangleQueue, ref vertexList, ref indexList, ref normalList, ref vertexDict);

        //triangleList.Dispose();
        //triangleQueue.Dispose();

        //mesh.SetVertices(vertexList);
        //mesh.SetTriangles(indexList, 0);
        //mesh.normals = NormalizedArrayFromList(normalList);

        //mesh.RecalculateNormals();

        //Destroy(meshGameObject);

        //meshGameObject = new GameObject("Marching Cubes Mesh");
        //meshGameObject.transform.parent = transform;
        //meshGameObject.AddComponent<MeshFilter>();
        //meshGameObject.AddComponent<MeshRenderer>();
        //meshGameObject.GetComponent<Renderer>().material = meshMaterial;
        //meshGameObject.GetComponent<MeshFilter>().mesh = mesh;
        //meshGameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

        //meshes.Add(meshGameObject);

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

    void CreateVertexIndexNormalListsFromTriangles(NativeQueue<Triangle> triangleList, ref List<Vector3> vertexList, ref List<int> indexList, ref List<Vector3> normalList, ref Dictionary<Vector3, int> vertexDictionary)
    {
        while(triangleList.Count > 0)
        {
            Triangle triangle = triangleList.Dequeue();

            Vector3 edge0 = triangle.vertex1 - triangle.vertex0;
            Vector3 edge1 = triangle.vertex2 - triangle.vertex0;

            Vector3 triangleNormal = Vector3.Cross(edge1, edge0).normalized;


            // FOR WELDED VERTICES
            if (vertexWelding)
            {
                int vertexCount = vertexList.Count;

                List<int> triangleIndexList = new List<int>();

                if (vertexDictionary.ContainsKey(triangle.vertex0))
                {
                    triangleIndexList.Add(vertexDictionary[triangle.vertex0]);

                    normalList[vertexDictionary[triangle.vertex0]] += triangleNormal;
                }
                else
                {
                    vertexList.Add(triangle.vertex0);
                    vertexDictionary.Add(triangle.vertex0, vertexCount);
                    triangleIndexList.Add(vertexCount);

                    normalList.Add(triangleNormal);

                    vertexCount++;
                }

                if (vertexDictionary.ContainsKey(triangle.vertex1))
                {
                    triangleIndexList.Add(vertexDictionary[triangle.vertex1]);

                    normalList[vertexDictionary[triangle.vertex1]] += triangleNormal;
                }
                else
                {
                    vertexList.Add(triangle.vertex1);
                    vertexDictionary.Add(triangle.vertex1, vertexCount);
                    triangleIndexList.Add(vertexCount);

                    normalList.Add(triangleNormal);

                    vertexCount++;
                }

                if (vertexDictionary.ContainsKey(triangle.vertex2))
                {
                    triangleIndexList.Add(vertexDictionary[triangle.vertex2]);

                    normalList[vertexDictionary[triangle.vertex2]] += triangleNormal;
                }
                else
                {
                    vertexList.Add(triangle.vertex2);
                    vertexDictionary.Add(triangle.vertex2, vertexCount);
                    triangleIndexList.Add(vertexCount);

                    normalList.Add(triangleNormal);

                    vertexCount++;
                }

                if (thresholdValue > 0f)
                {
                    indexList.Add(triangleIndexList[2]);
                    indexList.Add(triangleIndexList[1]);
                    indexList.Add(triangleIndexList[0]);
                }
                else
                {
                    indexList.Add(triangleIndexList[0]);
                    indexList.Add(triangleIndexList[1]);
                    indexList.Add(triangleIndexList[2]);
                }
            }
            else
            {
                // FOR NON WELDED VERTICES            
                int offset = indexList.Count;
                vertexList.Add(triangle.vertex0);
                vertexList.Add(triangle.vertex1);
                vertexList.Add(triangle.vertex2);

                normalList.Add(triangleNormal);
                normalList.Add(triangleNormal);
                normalList.Add(triangleNormal);

                if (thresholdValue > 0f)
                {
                    indexList.Add(offset + 2);
                    indexList.Add(offset + 1);
                    indexList.Add(offset + 0);
                }
                else
                {
                    indexList.Add(offset + 0);
                    indexList.Add(offset + 1);
                    indexList.Add(offset + 2);
                }
            }
        }
    }
}

public struct Triangle
{
    public Vector3 vertex0;
    public Vector3 vertex1;
    public Vector3 vertex2;
}
