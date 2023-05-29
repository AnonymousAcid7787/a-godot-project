using Godot;
using System;

public partial class TerrainGen : Node2D
{
	private float[,] noiseGrid;
	public override void _Draw()
	{
		for(var y = 0; y < 128; y++) {
			for(var x = 0; x < 128; x++) {
				DrawRect(
					new Rect2(
						new Vector2(x*5, y*5),
						new Vector2(5,5)
					),
					new Color(0.5f, noiseGrid[y,x], 0.5f),
					true);
			}
		}
	}
	public override void _Ready()
	{
		noiseGrid = FractalNoise(
						0,0, /*chunk x,y*/
						128,128,/*grid width, grid height*/
						0,1,/*min height, max height*/
						0.5f,/*frequency*/
						3,/*octaves*/
						2f,/*lacunarity*/
						0.2f/*persistence*/);

		// string gridStr = "";
		// for(var y = 0; y < 20; y++) {
		// 	for(var x = 0; x < 20; x++) {
		// 		gridStr += noiseGrid[y, x] + ", ";
		// 	}
		// 	gridStr += "\n";
		// }
		// DisplayServer.ClipboardSet(gridStr);
	}

	public static float[,] FractalNoise(int chunkX, int chunkY, int gridWidth, int gridHeight, int minHeight, int maxHeight, float frequency, int octaves, float lacunarity, float persistence) {
		FastNoiseLite noise = new FastNoiseLite();
		noise.DomainWarpType = FastNoiseLite.DomainWarpTypeEnum.Simplex;

		float[,] grid = new float[gridHeight, gridWidth];
		float amplitude = maxHeight/2f;

		for(int y = chunkX; y < gridHeight; y++) {
			for(int x = chunkY; x < gridWidth; x++) {
				float cellElevation = amplitude;
				float tFrequency = frequency;
				float tAmplitude = amplitude;

				for(int octave = 0; octave < octaves; octave++) {
					float sampleX = x * tFrequency;
					float sampleY = y * tFrequency;
					cellElevation += noise.GetNoise2D(sampleX, sampleY) * tAmplitude;

					tFrequency *= lacunarity;
					tAmplitude *= persistence;
				}

				cellElevation = Mathf.Clamp(cellElevation, minHeight, maxHeight);
				grid[y, x] = cellElevation;
			}
		}

		return grid;
	}
}
