using System;
using UnityEngine;

public class TerrainGenerator: MonoBehaviour
{
    public float baseHeight = 64;
    public NoiseOctaveSettings[] octaves;

    [Serializable]
    public class NoiseOctaveSettings
    {
        public FastNoiseLite.NoiseType noiseType;
        public float Frequency = 0.2f;
        public float Amplitude = 1;
    }

    private FastNoiseLite[] octaveNoises;
    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        octaveNoises = new FastNoiseLite[octaves.Length];
        for (int i = 0; i < octaves.Length; i++)
        {
            octaveNoises[i] = new FastNoiseLite();
            octaveNoises[i].SetNoiseType(octaves[i].noiseType);
            octaveNoises[i].SetFrequency(octaves[i].Frequency);

        }
    }
    public BlockType[,,] GenerateTerrain(float xOffset, float zOffset)
    {
        var result = new BlockType[ChunkRenderer.ChunkWidth, ChunkRenderer.ChunkHeight, ChunkRenderer.ChunkWidth];

        for (int x = 0; x < ChunkRenderer.ChunkWidth; x++)
        {
            for (int z = 0; z < ChunkRenderer.ChunkWidth; z++)
            {
                float height = GetHeight(x * ChunkRenderer.BlockScale + xOffset, z * ChunkRenderer.BlockScale + zOffset);
                for (int y = 0; y < height / ChunkRenderer.BlockScale; y++)
                {
                    result[x, y, z] = BlockType.Grass;
                }
            }
        }
        return result;
    }

    private float GetHeight(float x, float y)
    {
        float result = baseHeight;

        for (int i = 0; i < octaves.Length; i++)
        {
            float noise = octaveNoises[i].GetNoise(x, y);
            result += noise * octaves[i].Amplitude / 2;
        }
        return result;
    }
}
