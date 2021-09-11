using UnityEngine;
using System.Collections.Generic;
using System;

// TiledMapEditorからエクスポートされるJSONファイルからMapを読み込むクラス。
public class MapReader
{
    public static Map ReadMap(string mapID)
    {
        // Load JSON File From Assets/Resources/Map
        TextAsset jsonText = Resources.Load("map" + mapID) as TextAsset;

        // Deserialize JSON Text
        TiledMapEditor.MapData mapData = JsonUtility.FromJson<TiledMapEditor.MapData>(jsonText.text);

        // Instantiate map class
        Map map = new Map();
        List<int> rawData = mapData.layers[0].data;
        for (int h = 0; h < mapData.height; h++)
        {
            List<Tile> row = new List<Tile>();
            int heightOffset = h * mapData.width;
            for (int w = 0; w < mapData.width; w++)
            {
                row.Add((Tile)Enum.ToObject(typeof(Tile), rawData[heightOffset + w]));
            }
            map.Tiles.Add(row);
        }

        map.PlayerPosition = ((int)(mapData.layers[1].objects[0].x / mapData.tilewidth)
        , (int)(mapData.layers[1].objects[0].y / mapData.tileheight));

        foreach (var gem in mapData.layers[2].objects)
        {
            map.GemPositions.Add(((int)(gem.x / mapData.tilewidth), (int)(gem.y / mapData.tileheight)));
        }
        return map;
    }
}

namespace TiledMapEditor
{
    // 
    // 以下はTiledMapEditorからエクスポートされるJSONファイルをデシリアライズできるC#Class群
    //
    [System.Serializable]
    public class Property
    {
        public string name;
        public string type;
        public int value;
    }

    [System.Serializable]
    public class Object
    {
        public int height;
        public int id;
        public string name;
        public bool point;
        public int rotation;
        public string type;
        public bool visible;
        public int width;
        public float x;
        public float y;
        public List<Property> properties;
    }
    [System.Serializable]
    public class Layer
    {
        public List<int> data;
        public int height;
        public int id;
        public string name;
        public int opacity;
        public string type;
        public bool visible;
        public int width;
        public int x;
        public int y;

        public List<Object> objects;
    }

    [System.Serializable]
    public class Tileset
    {
        public int firstgid;
        public string source;
    }

    [System.Serializable]
    public class MapData
    {
        public int compressionlevel;
        public int height;
        public bool infinite;
        public List<Layer> layers;
        public int nextlayerid;
        public int nextobjectid;
        public string orientation;
        public string renderorder;
        public string tiledversion;
        public int tileheight;
        public List<Tileset> tilesets;
        public int tilewidth;
        public string type;
        public string version;
        public int width;
    }
}