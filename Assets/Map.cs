using System.Collections.Generic;

public class Map
{
    public List<List<Tile>> Tiles = new List<List<Tile>>();
    public (int, int) PlayerPosition;
    public List<(int, int)> GemPositions = new List<(int, int)>();

    public Tile TileAt((int, int) position)
    {
        return Tiles[position.Item2][position.Item1];
    }
}