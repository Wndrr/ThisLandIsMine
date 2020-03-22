using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Controllers.Misc
{
    [RequireComponent(typeof(MeshFilter))]
    public class MapGenerator : MonoBehaviour
    {
        private Mesh _mesh;
        private Vector3[] _vertices;
        private int[] _triangles;

        public int xSize = 20;
        public int zSize = 20;
        public GameObject resourcePrefab;
        public int resourcesCount = 400;

        // Start is called before the first frame update
        private void Start()
        {
            _mesh = new Mesh();
            var meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = _mesh;

            CreateShape();
            UpdateMesh();
            GetComponent<MeshCollider>().sharedMesh = _mesh;
            SpawnResources();
        }

        private void SpawnResources()
        {
            var rand = new Random();

            for (var i = 0; i < resourcesCount; i++)
            {
                var randomPosition = rand.Next(0, _mesh.vertices.Length);
                var targetSpawnVertex = _vertices[randomPosition];
                Instantiate(resourcePrefab, targetSpawnVertex, Quaternion.identity);
            }

        }

        private void CreateShape()
        {
            _vertices = new Vector3[(xSize + 1) * (zSize + 1)];

            var i = 0;
            for (var z = 0; z <= zSize; z++)
            {
                for (var x = 0; x <= xSize; x++)
                {
                    var y = Mathf.PerlinNoise(x * .1f, z * .1f) * 5f;
                    _vertices[i] = new Vector3(x, 0, z);
                    i++;
                }
            }

            _triangles = new int[xSize * zSize * 6];
            var vert = 0;
            var tris = 0;
            for (var z = 0; z < zSize; z++)
            {
                for (var x = 0; x < xSize; x++)
                {
                    _triangles[tris + 0] = vert + 0;
                    _triangles[tris + 1] = vert + xSize + 1;
                    _triangles[tris + 2] = vert + 1;
                    _triangles[tris + 3] = vert + 1;
                    _triangles[tris + 4] = vert + xSize + 1;
                    _triangles[tris + 5] = vert + xSize + 2;
                    vert++;
                    tris += 6;
                }

                vert++;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(_vertices.Last(), 10);
        }

        private void UpdateMesh()
        {
            _mesh.Clear();
            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
        }
    }
}