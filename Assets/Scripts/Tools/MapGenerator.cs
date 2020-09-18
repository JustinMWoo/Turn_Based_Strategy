using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapGenerator : EditorWindow
{
    float x = 0;
    float z = 0;

    [MenuItem("Tools/Generate Map (DO NOT USE)")]
    public static void GenerateMapScript()
    {
        MapGenerator window = (MapGenerator)GetWindow(typeof(MapGenerator));
        window.minSize = new Vector2(600, 300);
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        x = EditorGUILayout.FloatField("X:", x);

        EditorGUILayout.BeginHorizontal();
        z = EditorGUILayout.FloatField("Z:", z);

        if (GUILayout.Button("Done"))
        {
            if (x > 0 && z > 0)
            {
                GameObject map = new GameObject("Map");
                map.transform.position = new Vector3(-((x - 1) / 2.0f), 0f, -((z - 1) / 2.0f));

                GameObject row = new GameObject("Row");
                row.transform.position = new Vector3(-((x - 1) / 2.0f), 0f, 0f);

                GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tile.name = "Tile";
                tile.transform.position = Vector3.zero;
                tile.gameObject.tag = "Tile";

                Material material = Resources.Load<Material>("Tile");
                tile.GetComponent<Renderer>().material = material;

                tile.transform.SetParent(row.transform);
                row.transform.SetParent(map.transform);
                for (int currX = 0; currX < x; currX++)
                {
                    Vector3 newPosition = tile.transform.position + (Vector3.right * currX);
                    Instantiate(tile, newPosition, tile.transform.rotation, row.transform);
                }

                for (int currZ = 1; currZ < z; currZ++)
                {
                    Vector3 newPosition = row.transform.position + (Vector3.forward * currZ);
                    Instantiate(row, newPosition, row.transform.rotation, map.transform);
                }
            }
            Close();
        }
        if (GUILayout.Button("Cancel"))
        {
            Close();
        }

    }
}
