using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
public class Chunk : MonoBehaviour
{
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private int ChunkSize { get; set; }
    public void Load(int chunkSize)
    {
        _mesh = new Mesh();
        ChunkSize = chunkSize;

        CreateShape();
        
        _mesh.Clear();
        _mesh.vertices = _vertices.Select(vertex => vertex + transform.position).ToArray();
        transform.position = Vector3.zero;
        _mesh.triangles = _triangles;
        GetComponent<MeshFilter>().mesh = _mesh;
        GetComponent<MeshCollider>().sharedMesh = _mesh;
    }
    
    private void CreateShape()
    {
        _vertices = new Vector3[(ChunkSize + 1) * (ChunkSize + 1)];

        var i = 0;
        for (var z = 0; z <= ChunkSize; z++)
        {
            for (var x = 0; x <= ChunkSize; x++)
            {
                
                var bigPerlin = Mathf.PerlinNoise((transform.position.x + x  )* .004f, (transform.position.z + z) * .004f) * 50;
                var smallPerlin = Mathf.PerlinNoise((transform.position.x + x  )* .05f, (transform.position.z + z) * .05f) * 6;
                _vertices[i] = new Vector3(x, bigPerlin + smallPerlin, z);
                i++;
            }
        }

        _triangles = new int[ChunkSize * ChunkSize * 6];
        var vert = 0;
        var tris = 0;
        for (var z = 0; z < ChunkSize; z++)
        {
            for (var x = 0; x < ChunkSize; x++)
            {
                _triangles[tris + 0] = vert + 0;
                _triangles[tris + 1] = vert + ChunkSize + 1;
                _triangles[tris + 2] = vert + 1;
                _triangles[tris + 3] = vert + 1;
                _triangles[tris + 4] = vert + ChunkSize + 1;
                _triangles[tris + 5] = vert + ChunkSize + 2;
                vert++;
                tris += 6;
            }

            vert++;
        }
    }
}
