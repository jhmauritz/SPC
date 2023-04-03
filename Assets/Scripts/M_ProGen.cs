using UnityEngine;
using System.Collections.Generic;

public class ProceduralMapGenerator : MonoBehaviour {

    public GameObject chunkPrefab;
    public int chunkSize;
    public int renderDistance;

    private Dictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();
    private Vector3Int playerChunkPosition;
    private Transform playerTransform;

    private void Start() {
        playerTransform = transform;
        GenerateChunksAroundPlayer();
    }

    private void Update() {
        Vector3Int newPlayerChunkPosition = GetPlayerChunkPosition();
        if (newPlayerChunkPosition != playerChunkPosition) {
            playerChunkPosition = newPlayerChunkPosition;
            GenerateChunksAroundPlayer();
        }
    }

    private void GenerateChunksAroundPlayer() {
        int halfRenderDistance = renderDistance / 2;
        for (int x = -halfRenderDistance; x <= halfRenderDistance; x++) {
            for (int y = -halfRenderDistance; y <= halfRenderDistance; y++) {
                for (int z = -halfRenderDistance; z <= halfRenderDistance; z++) {
                    Vector3Int chunkPosition = playerChunkPosition + new Vector3Int(x, y, z);
                    if (!chunks.ContainsKey(chunkPosition)) {
                        GenerateChunk(chunkPosition);
                    }
                }
            }
        }

        List<Vector3Int> chunksToRemove = new List<Vector3Int>();
        foreach (KeyValuePair<Vector3Int, Chunk> chunkPair in chunks) {
            if (Vector3Int.Distance(playerChunkPosition, chunkPair.Key) > halfRenderDistance) {
                chunksToRemove.Add(chunkPair.Key);
                Destroy(chunkPair.Value.gameObject);
            }
        }

        foreach (Vector3Int chunkPosition in chunksToRemove) {
            chunks.Remove(chunkPosition);
        }
    }

    private void GenerateChunk(Vector3Int chunkPosition) {
        GameObject chunkObject = Instantiate(chunkPrefab, chunkPosition * chunkSize, Quaternion.identity);
        Chunk chunk = chunkObject.GetComponent<Chunk>();
        chunk.Generate();
        chunks.Add(chunkPosition, chunk);
    }

    private Vector3Int GetPlayerChunkPosition() {
        int x = Mathf.FloorToInt(playerTransform.position.x / chunkSize);
        int y = Mathf.FloorToInt(playerTransform.position.y / chunkSize);
        int z = Mathf.FloorToInt(playerTransform.position.z / chunkSize);
        return new Vector3Int(x, y, z);
    }
}