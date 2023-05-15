using Godot;
using System;

public partial class TerrainGen : Node2D
{
    private float[,] noiseGrid;
    public override void _Draw()
    {
        for(var y = 0; y < 20; y++) {
            for(var x = 0; x < 20; x++) {
                DrawRect(
                    new Rect2(
                        new Vector2(x*5, y*5),
                        new Vector2(5,5)
                    ),
                    new Color(100, noiseGrid[y,x], 100, 1),
                    true);
            }
        }
    }
    public override void _Ready()
    {
        noiseGrid = FractalNoise(0,0, 20,20, 1,100, 0.5f, 3, 2f, 0.2f);

        string gridStr = "";
        for(var y = 0; y < 20; y++) {
            for(var x = 0; x < 20; x++) {
                gridStr += noiseGrid[y, x] + ", ";
            }
            gridStr += "\n";
        }
        DisplayServer.ClipboardSet(gridStr);
    }

    public static float[,] FractalNoise(int chunkX, int chunkY, int gridWidth, int gridHeight, int minHeight, int maxHeight, float frequency, int octaves, float lacunarity, float persistence) {
        FastNoiseLite noise = new FastNoiseLite();
        noise.DomainWarpType = FastNoiseLite.DomainWarpTypeEnum.Simplex;

        float[,] grid = new float[gridHeight, gridWidth];
        float amplitude = maxHeight/2f;

        for(int y = chunkX; y < gridHeight; y++) {
            for(int x = chunkY; x < gridWidth; x++) {
                float cellElevation = amplitude;
                float cellFrequency = frequency;
                float cellAmplitude = amplitude;

                //Perlin noise octaves
                for(int octave = 0; octave < octaves; octave++) {
                    float sampleX = x * cellFrequency;
                    float sampleY = y * cellFrequency;
                    cellElevation += noise.GetNoise2D(sampleX, sampleY) * cellAmplitude;

                    cellFrequency *= lacunarity;
                    cellAmplitude *= persistence;
                }

                cellElevation = Mathf.Clamp(cellElevation, minHeight, maxHeight);
                grid[y, x] = cellElevation;
            }
        }

        return grid;
    }
}
