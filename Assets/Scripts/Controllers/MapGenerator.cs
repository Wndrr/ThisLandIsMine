using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(MeshFilter))]
public class MapGenerator : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    public int xSize = 20;
    public int zSize = 20;
    public GameObject resourcePrefab;
    public int resourcesCount = 400;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        var meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        CreateShape();
        UpdateMesh();
        GetComponent<MeshCollider>().sharedMesh = mesh;
        SpawnResources();
    }

    private void SpawnResources()
    {
        var rand = new Random();

        for (int i = 0; i < resourcesCount; i++)
        {
            var randomPosition = rand.Next(0, mesh.vertices.Length);
            var targetSpawnVertex = vertices[randomPosition];
            Instantiate(resourcePrefab, targetSpawnVertex, Quaternion.identity);
        }

    }

    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        var i = 0;
        for (var z = 0; z <= zSize; z++)
        {
            for (var x = 0; x <= xSize; x++)
            {
                var y = Mathf.PerlinNoise(x * .3f, z * 1.5f) * 1.5f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        var vert = 0;
        var tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }

            vert++;
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}