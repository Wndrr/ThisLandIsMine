using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject ItemToSpawn;
    private int InitialMapSize = 2;
    /// <summary>
    /// Chunks are ALWAYS square. This is the size of ONE side of the chunk. Totla tiles count will be the square of this value
    /// </summary>
    public const int ChunkWidth = 10;
    
    private List<Chunk> Chunks = new List<Chunk>();

    // Start is called before the first frame update
    void Start()
    {
        for (var x = 0; x <= InitialMapSize; x++)
        {
            for (var y = 0; y <= InitialMapSize; y++)
            {
                var chunk = new Chunk(new Vector2(x, y), ItemToSpawn);
                Chunks.Add(chunk);
            }
        }
    }
}

public class Chunk
{

    public Vector2 Key { get; set; }
    public List<Tile> Tiles = new List<Tile>(TerrainGenerator.ChunkWidth * TerrainGenerator.ChunkWidth);

    public Chunk(Vector2 key, GameObject itemToSpawn)
    {
        Key = key;
        
        for (var x = 1; x < TerrainGenerator.ChunkWidth; x++)
        {
            for (var y = 1; y < TerrainGenerator.ChunkWidth; y++)
            {
                var tile = new Tile(new Vector2(x, y),Key, itemToSpawn);
                Tiles.Add(tile);
            }
        }
    }
}

public class Tile
{
    public Tile(Vector2 key, Vector2 parentKey, GameObject content)
    {
        Key = key;
        ParentKey = parentKey;
        
        Content = Object.Instantiate(content, key * parentKey, Quaternion.identity);
    }

    /// <summary>
    /// The position of the tile in a chunk. MUST be comprised BETWEEN 0 <see cref="Chunk.ChunkWidth"/> minus 1 (indexing starts at 0)
    /// </summary>
    public Vector2 Key { get; set; }

    public readonly Vector2 ParentKey;
    public GameObject Content { get; set; }
}