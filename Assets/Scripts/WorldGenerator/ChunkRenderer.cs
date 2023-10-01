using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(requiredComponent:typeof(MeshRenderer), requiredComponent2:typeof(MeshFilter))]
public class ChunkRenderer : MonoBehaviour
{
    public const int ChunkWidth = 25;
    public const int ChunkHeight = 128;
    public const float BlockScale = 1f;

    public ChunkData ChunkData;
    private Mesh chunkMesh;
    private List<Vector3> verticies = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();

    private List<int> triangles = new List<int>();
    public GameWorld ParentWorld;
    void Start()
    {
        chunkMesh = new Mesh();
        RegenerateMesh();

        GetComponent<MeshFilter>().mesh = chunkMesh;
        GetComponent<MeshCollider>().sharedMesh = chunkMesh;
    }

   
    public void SpawnBlock(Vector3Int blockPosition)
    {
        ChunkData.Blocks[blockPosition.x, blockPosition.y, blockPosition.z] = BlockType.Grass;
        RegenerateMesh();
    }

    public void DestroyBlock(Vector3Int blockPosition)
    {
        ChunkData.Blocks[blockPosition.x, blockPosition.y, blockPosition.z] = BlockType.Air;
        RegenerateMesh();
    }

    void RegenerateMesh()
    {
        verticies.Clear();
        triangles.Clear();
        uvs.Clear();
        for (int y = 0; y < ChunkHeight; y++)
        {
            for (int x = 0; x < ChunkWidth; x++)
            {
                for (int z = 0; z < ChunkWidth; z++)
                {
                    GenerateBlock(x, y, z);
                }

            }
        }

        chunkMesh.triangles = Array.Empty<int>();
        chunkMesh.vertices = verticies.ToArray(); 
        chunkMesh.triangles = triangles.ToArray();
        chunkMesh.uv = uvs.ToArray();
        chunkMesh.Optimize();

        chunkMesh.RecalculateNormals();
        chunkMesh.RecalculateBounds();

        GetComponent<MeshCollider>().sharedMesh = chunkMesh;

    }

    private void GenerateBlock(int x, int y, int z)
    {
        var blockPosition = new Vector3Int(x, y, z);
        if (GetBlockAtPosition(blockPosition) == 0) return;

        if(GetBlockAtPosition(blockPosition + Vector3Int.right) == 0)GenerateRightSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.left) == 0) GenerateLeftSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.forward) == 0) GenerateFrontSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.back) == 0) GenerateBackSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.up) == 0) GenerateTopSide(blockPosition);
        if (GetBlockAtPosition(blockPosition + Vector3Int.down) == 0) GenerateBottomSide(blockPosition);
    }

    private BlockType GetBlockAtPosition(Vector3Int blockPosition)
    {
        if (blockPosition.x >= 0 && blockPosition.x < ChunkWidth
            && blockPosition.y >= 0 && blockPosition.y < ChunkHeight
            && blockPosition.z >= 0 && blockPosition.z < ChunkWidth)
        {
            return ChunkData.Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
        }
        else
        {
            if (blockPosition.y < 0 || blockPosition.y >= ChunkHeight) return BlockType.Air;


            Vector2Int adjacentChunkPosition = ChunkData.ChunkPosition;
            if (blockPosition.x < 0)
            {
                adjacentChunkPosition.x--;
                blockPosition.x += ChunkWidth;
            }
            else if (blockPosition.x >= ChunkWidth)
            {
                adjacentChunkPosition.x++;
                blockPosition.x -= ChunkWidth;
            }

            if (blockPosition.z < 0)
            {
                adjacentChunkPosition.y--;
                blockPosition.z += ChunkWidth;
            }
            else if (blockPosition.z >= ChunkWidth)
            {
                adjacentChunkPosition.y++;
                blockPosition.z -= ChunkWidth;
            }


            if(ParentWorld.ChunkDatas.TryGetValue(adjacentChunkPosition, out ChunkData adjacentChunk))
            {
                return adjacentChunk.Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
            }
            else
            {
                return BlockType.Air;
            }

        }
    }

    private void GenerateRightSide(Vector3Int blockPosition)
    {
        uvs.Add(new Vector2(257f / 2048, 1921f / 2048));
        uvs.Add(new Vector2(257f / 2048, 1));
        uvs.Add(new Vector2(383f / 2048, 1921f / 2048));
        uvs.Add(new Vector2(383f / 2048, 1));

        verticies.Add((new Vector3(1, 0, 0) + blockPosition) * BlockScale);
        verticies.Add((new Vector3(1, 1, 0) + blockPosition) * BlockScale);
        verticies.Add((new Vector3(1, 0, 1) + blockPosition) * BlockScale);
        verticies.Add((new Vector3(1, 1, 1) + blockPosition) * BlockScale);
        AddLastVerticiesSquare();
    }

    private void GenerateLeftSide(Vector3Int blockPosition)
    {
        uvs.Add(new Vector2(257f / 2048, 1921f / 2048));
        uvs.Add(new Vector2(257f / 2048, 1));
        uvs.Add(new Vector2(383f / 2048, 1921f / 2048));
        uvs.Add(new Vector2(383f / 2048, 1));

        verticies.Add((new Vector3(0, 0, 0) + blockPosition)* BlockScale);
        verticies.Add((new Vector3(0, 0, 1) + blockPosition)* BlockScale);
        verticies.Add((new Vector3(0, 1, 0) + blockPosition)* BlockScale);
        verticies.Add((new Vector3(0, 1, 1) + blockPosition)* BlockScale);
        AddLastVerticiesSquare();
    }

    private void GenerateFrontSide(Vector3Int blockPosition)
    {
        uvs.Add(new Vector2(257f / 2048, 1921f / 2048));
        uvs.Add(new Vector2(257f / 2048, 1));
        uvs.Add(new Vector2(383f / 2048, 1921f / 2048));
        uvs.Add(new Vector2(383f / 2048, 1));

        verticies.Add((new Vector3(0, 0, 1) + blockPosition)* BlockScale);
        verticies.Add((new Vector3(1, 0, 1) + blockPosition)* BlockScale);
        verticies.Add((new Vector3(0, 1, 1) + blockPosition)* BlockScale);
        verticies.Add((new Vector3(1, 1, 1) + blockPosition)* BlockScale);

        AddLastVerticiesSquare();
    }

    private void GenerateBackSide(Vector3Int blockPosition)
    {

        uvs.Add(new Vector2(257f / 2048, 1921f / 2048));
        uvs.Add(new Vector2(257f / 2048, 1));
        uvs.Add(new Vector2(383f / 2048, 1921f / 2048));
        uvs.Add(new Vector2(383f / 2048, 1));

        verticies.Add((new Vector3(0, 0, 0) + blockPosition)*BlockScale);
        verticies.Add((new Vector3(0, 1, 0) + blockPosition)*BlockScale);
        verticies.Add((new Vector3(1, 0, 0) + blockPosition)*BlockScale);
        verticies.Add((new Vector3(1, 1, 0) + blockPosition)*BlockScale);

        AddLastVerticiesSquare();
    }

    private void GenerateTopSide(Vector3Int blockPosition)
    {
        uvs.Add(new Vector2(128f / 2048, 1921f / 2048));
        uvs.Add(new Vector2(128f / 2048, 1));
        uvs.Add(new Vector2(256f / 2048, 1921f / 2048));
        uvs.Add(new Vector2(256f / 2048, 1));

        verticies.Add((new Vector3(0, 1, 0) + blockPosition)*BlockScale);
        verticies.Add((new Vector3(0, 1, 1) + blockPosition)*BlockScale);
        verticies.Add((new Vector3(1, 1, 0) + blockPosition)*BlockScale);
        verticies.Add((new Vector3(1, 1, 1) + blockPosition)*BlockScale);

        AddLastVerticiesSquare();
    }

    private void GenerateBottomSide(Vector3Int blockPosition)
    {
        uvs.Add(new Vector2(257f / 2048, 1921f / 2048));
        uvs.Add(new Vector2(257f / 2048, 1));
        uvs.Add(new Vector2(383f / 2048, 1921f / 2048));
        uvs.Add(new Vector2(383f / 2048, 1));

        verticies.Add((new Vector3(0, 0, 0) + blockPosition)*BlockScale);
        verticies.Add((new Vector3(1, 0, 0) + blockPosition)*BlockScale);
        verticies.Add((new Vector3(0, 0, 1) + blockPosition)*BlockScale);
        verticies.Add((new Vector3(1, 0, 1) + blockPosition)*BlockScale);

        AddLastVerticiesSquare();
    }

    private void AddLastVerticiesSquare()
    {

        //uvs.Add(new Vector2(0, 0));
        //uvs.Add(new Vector2(0, 1));
        //uvs.Add(new Vector2(1, 0));
        //uvs.Add(new Vector2(1, 1));

        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 2);
    }
}
