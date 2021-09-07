using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    [SerializeField]
    private float _GridSizeInWorld;

    [SerializeField]
    private GameObject _loadTilePrefab;

    [SerializeField]
    private GameObject _wallTilePrefab;
    [SerializeField]
    private PlayerController _player;
    [SerializeField]
    private GameObject _upStairPrefab;
    [SerializeField]
    private GameObject _downStairPrefab;
    [SerializeField]
    private GameObject _gemPrefab;

    [SerializeField]
    private GameManager _gameManager;

    private Map _map;

    private Dictionary<(int, int), GameObject> _gemAt = new Dictionary<(int, int), GameObject>();

    void Start()
    {
        _map = MapReader.ReadMap("1");
        CreateField();
        _player.InitPosition(_map.PlayerPosition);
    }

    public void CreateField()
    {
        int ySize = _map.Tiles.Count;
        int xSize = _map.Tiles[0].Count;
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                Tile tile = _map.TileAt((x, y));
                Instantiate(PrefabOfTile(tile), new Vector3(GridToWorld(x), GridToWorld(y), 0), Quaternion.identity, this.transform);
            }
        }
        foreach (var pos in _map.GemPositions)
        {
            var gem = Instantiate(_gemPrefab, new Vector3(GridToWorld(pos.Item1), GridToWorld(pos.Item2), 0), Quaternion.identity, this.transform);
            _gemAt.Add(pos, gem);
        }
    }

    public float GridToWorld(float grid)
    {
        return grid * _GridSizeInWorld;
    }

    GameObject PrefabOfTile(Tile tile)
    {
        switch (tile)
        {
            case Tile.Load:
                return _loadTilePrefab;
            case Tile.Wall:
                return _wallTilePrefab;
            case Tile.UpStair:
                return _upStairPrefab;
            case Tile.DownStair:
                return _downStairPrefab;
        }
        throw new System.ArgumentException("Invalid Tile");
    }

    public Tile TileAt((int, int) position)
    {
        return _map.TileAt(position);
    }

    public bool GemExistsAt((int, int) position)
    {
        return _gemAt.ContainsKey(position);
    }

    public void PickupGemAt((int, int) position)
    {
        _gameManager.MineGem();
        Destroy(_gemAt[position]);
        _gemAt.Remove(position);
    }
}