using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region Variables
    public bool walkable = true;
    public bool current = false;
    // Where you are moving to
    public bool target = false;
    // Where you can move to
    public bool selectable = false;
    public bool attackRange = false;
    public bool AOE = false;

    public List<Tile> adjacencyList = new List<Tile>();

    // BFS variables
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0; // How far each tile is from the start

    // A* variables
    public float f = 0; // g + h
    public float g = 0; // Cost of parent to current tile
    public float h = 0; // Cost from processed tile to destination


    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (AOE)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (attackRange)
        {
            GetComponent<Renderer>().material.color = new Color (1.0f,0.67f,0.67f);
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset(bool aoe)
    {
        adjacencyList.Clear();

        current = false;
        target = false;
        attackRange = false;
        if (!aoe)
        {
            selectable = false;
            AOE = false;
        }

        visited = false;
        parent = null;
        distance = 0;

        f = g = h = 0;
    }

    public void FindNeighbors(float jumpHeight, Tile target, bool attack, bool aoe)
    {

        Reset(aoe);


        CheckTile(Vector3.forward, jumpHeight, target, attack);
        CheckTile(-Vector3.forward, jumpHeight, target, attack);
        CheckTile(Vector3.right, jumpHeight, target, attack);
        CheckTile(-Vector3.right, jumpHeight, target, attack);
    }

    public void CheckTile(Vector3 direction, float jumpHeight, Tile target, bool attack)
    {
        // Check at approx half the size of one tile
        Vector3 halfExtents = new Vector3(0.25f, jumpHeight, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders)
        {
            // Ensure the item is a tile
            Tile tile = item.GetComponent<Tile>();
            if (tile != null && tile.walkable)
            {
                RaycastHit hit;

                // Check if there is an object above the tile (such as a unit or obstacle)
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == target) || attack)
                {
                    adjacencyList.Add(tile);
                }
            }
        }
    }
}
