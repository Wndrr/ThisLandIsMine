using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    public GameObject ChunkPrefab;
    public int ChunkSize = 32;
    private Dictionary<Vector2, GameObject> LoadedChunks;
    private List<Vector2> ChunkToLoadTransformations;
    private Vector2 LastKnownChunkPosition;
    private int SquareRootOfNumberOfChunksToLoad = 6;

    // Start is called before the first frame update
    void Start()
    {
        LoadedChunks = new Dictionary<Vector2, GameObject>(9);

        ChunkToLoadTransformations = new List<Vector2>(SquareRootOfNumberOfChunksToLoad * SquareRootOfNumberOfChunksToLoad);
        for (int i = -SquareRootOfNumberOfChunksToLoad / 2; i < SquareRootOfNumberOfChunksToLoad / 2; i++)
        {
            for (int j = -SquareRootOfNumberOfChunksToLoad / 2; j < SquareRootOfNumberOfChunksToLoad / 2; j++)
            {
                if (!ChunkToLoadTransformations.Any(c => Math.Abs(c.x - i) < 0.0000001 && Math.Abs(c.y - j) < 0.0000001))
                    ChunkToLoadTransformations.Add(new Vector2(i, j));
            }
        }

        ChunkToLoadTransformations = ChunkToLoadTransformations.Distinct().ToList();

        Load(true);
    }

    // Update is called once per frame
    void Update()
    {
        Load();
    }

    private void Load(bool forceLoad = false)
    {
        var currentChunkPosition = GetCurrentChunkKey();

        if (LastKnownChunkPosition == currentChunkPosition && !forceLoad)
            return;

        LastKnownChunkPosition = currentChunkPosition;

        var chunksToLoad = ChunkToLoadTransformations.Select(s => currentChunkPosition + s).ToList();

        foreach (var chunk in LoadedChunks)
        {
            Destroy(chunk.Value);
        }

        LoadedChunks = chunksToLoad.ToDictionary(chunkKey => chunkKey, chunkKey =>
        {
                var positionOfChunkToLoad = chunkKey * ChunkSize;
                var chunkToLoad = Instantiate(ChunkPrefab, positionOfChunkToLoad.ToVector3(), Quaternion.identity);
                chunkToLoad.name = chunkKey.ToString();
                var chunkToLoadScript = chunkToLoad.GetComponent<Chunk>();
                chunkToLoadScript.Load(ChunkSize);
                return chunkToLoad;
       });
    }

    private Vector2 GetCurrentChunkKey()
    {
        var currentPosition = transform.position.ToVector2();
        var currentChunkPosition = new Vector2((int) currentPosition.x / ChunkSize, (int) currentPosition.y / ChunkSize);
        return currentChunkPosition;
    }
}

public static class VectorExtensions
{
    public static Vector2 ToVector2(this Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }

    public static Vector3 ToVector3(this Vector2 v2)
    {
        return new Vector3(v2.x, 0, v2.y);
    }
}