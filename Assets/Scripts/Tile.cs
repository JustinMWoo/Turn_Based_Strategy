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
    public List<Tile> adjacencyListAIAttack = new List<Tile>();

    // BFS variables
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0; // How far each tile is from the start

    // A* variables
    public float f = 0; // g + h
    public float g = 0; // Cost of parent to current tile
    public float h = 0; // Cost from processed tile to destination

    // AI variables
    public List<Unit> targetList = new List<Unit>();
    public Unit bestTarget;
    public int score;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
         if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (AOE)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
         else if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (attackRange)
        {
            GetComponent<Renderer>().material.color = new Color(1.0f, 0.67f, 0.67f);
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset(bool ability, bool clearTargets)
    {
        adjacencyList.Clear();
        
        target = false;
        
        if (!ability)
        {
            current = false;
            attackRange = false;
            selectable = false;
            AOE = false;
        }

        visited = false;
        parent = null;
        distance = 0;

        f = g = h = 0;

        if (clearTargets)
        {
            targetList.Clear();
            bestTarget = null;
        }
        
        score = 0;
    }

    public void FindNeighbors(float jumpHeight, Tile target, bool tilesWithObjectOnTop, bool ability)
    {

        Reset(ability, false);


        CheckTile(Vector3.forward, jumpHeight, target, tilesWithObjectOnTop, false);
        CheckTile(-Vector3.forward, jumpHeight, target, tilesWithObjectOnTop, false);
        CheckTile(Vector3.right, jumpHeight, target, tilesWithObjectOnTop, false);
        CheckTile(-Vector3.right, jumpHeight, target, tilesWithObjectOnTop, false);
    }

    public void FindNeighborsAI(float jumpHeight, float weaponVerticality)
    {
        Reset(false, false);

        CheckTile(Vector3.forward, jumpHeight, null, false, false);
        CheckTile(-Vector3.forward, jumpHeight, null, false, false);
        CheckTile(Vector3.right, jumpHeight,  null, false, false);
        CheckTile(-Vector3.right, jumpHeight, null, false, false);

        CheckTile(Vector3.forward, weaponVerticality, null, false, true);
        CheckTile(-Vector3.forward, weaponVerticality, null, false, true);
        CheckTile(Vector3.right, weaponVerticality, null, false, true);
        CheckTile(-Vector3.right, weaponVerticality, null, false, true);
    }

    public void CheckTile(Vector3 direction, float jumpHeight, Tile target, bool tilesWithObjectOnTop, bool AIAttack)
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
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == target) || tilesWithObjectOnTop)
                {
                    if (!AIAttack)
                    {
                        adjacencyList.Add(tile);
                    }
                    else
                    {
                        adjacencyListAIAttack.Add(tile);
                    }
                }
            }
        }
    }
}
