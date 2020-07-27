﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : UnitActions
{
    #region Variables

    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    public bool moving = false;
    public float moveSpeed = 2;
    public float jumpVelocity = 4.5f;

    Vector3 velocity = new Vector3(); // How fast the player moves from tile to tile
    Vector3 heading = new Vector3(); // Direction character is heading in

    float halfHeight = 0; // Distance from tile to center of the player

    // Jumping states
    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;
    Vector3 jumpTarget;

    // The actual tile the unit will move to in A*
    public Tile actualTargetTile;

    protected Unit unit;

    #endregion

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        halfHeight = GetComponent<Collider>().bounds.extents.y;

        unit = GetComponent<Unit>();

        
    }

    // Current tile sitting under unit (starting point for path finding)
    public void GetCurrentTile()
    {
        // Gets current tile for this unit
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    // Another tile to move to or that a unit is sitting on top of
    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;
        // Cast ray down from targeted object to find tile that it is on
        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }
        return tile;
    }

    public void ComputeAdjacencyLists(float jumpHeight, Tile target)
    {
        // Move this into here if tiles are going to be added or removed during gameplay
        // tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            // Get tile script from the game object
            Tile t = tile.GetComponent<Tile>();

            t.FindNeighbors(jumpHeight, target);
        }
    }

    // BFS for selectable tiles
    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists(unit.unitClass.JumpHeight.Value, null);
        GetCurrentTile();

        Queue<Tile> queue = new Queue<Tile>();

        queue.Enqueue(currentTile);
        currentTile.visited = true;

        while (queue.Count > 0)
        {
            Tile t = queue.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < unit.unitClass.Movement.Value)
            {
                foreach (Tile tile in t.adjacencyList)
                {
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.distance = t.distance + 1;
                        tile.visited = true;

                        queue.Enqueue(tile);
                    }
                }
            }
        }
    }

    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        // After you set destination you can no longer move
        moving = true;

        Tile next = tile;

        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }

    public void Move()
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            // Add halfHeight of player and the half height of the tile
            // Unit's position on top of the target tile
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                // See if a jump is needed to reach the target
                bool jump = transform.position.y != target.y;

                if (jump)
                {
                    Jump(target);
                }
                else
                {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                }

                // Locomotion (Add animation for movement here)
                transform.forward = heading; // Face direction unit is moving
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                // Tile center reached
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            moving = false;

            // End the units turn
            TurnManager.EndAction();
        }
    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (Tile tile in selectableTiles)
        {
            tile.Reset();
        }

        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;

        // Make a unit vector (normalize the values)
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    #region Jumping
    void Jump(Vector3 target)
    {
        if (fallingDown)
        {
            FallDownward(target);
        }
        else if (jumpingUp)
        {
            JumpUpward(target);
        }
        else if (movingEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }

    void PrepareJump(Vector3 target)
    {
        // Save the target's Y to use original y value for if statement
        float targetY = target.y;

        // Set the target's y to the units y to prevent tilting
        target.y = transform.position.y;

        CalculateHeading(target);

        // Jumping down to lower tile
        if (transform.position.y > targetY)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = true;

            // Halfway point between target and where unit is jumping to
            jumpTarget = transform.position + (target - transform.position) / 2.0f;
        }
        else
        {
            fallingDown = false;
            jumpingUp = true;
            movingEdge = false;

            // Slow down speed when jumping (change division value to edit)
            velocity = heading * moveSpeed / 3.0f;

            // How many tiles are being jumped
            float difference = targetY - transform.position.y;
            velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
        }
    }
    void FallDownward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        // Make unit land on tile without falling through
        if (transform.position.y <= target.y)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = false;

            Vector3 p = transform.position;
            p.y = target.y;
            transform.position = p;

            velocity = new Vector3();
        }
    }
    void JumpUpward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y > target.y)
        {
            jumpingUp = false;
            fallingDown = true;
        }
    }

    void MoveToEdge()
    {
        if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f)
        {
            SetHorizontalVelocity();
        }
        else
        {
            movingEdge = false;
            fallingDown = true;

            // Slow down jump velocity
            velocity /= 4.5f;
            // Add small jump after reaching edge
            velocity.y = 1.5f;
        }
    }
    #endregion

    // Use A* to find a path
    protected void FindPath(Tile target)
    {
        ComputeAdjacencyLists(unit.unitClass.JumpHeight.Value, target);
        GetCurrentTile();

        // When target tile is added to closed list the best path is found
        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currentTile);
        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);
        currentTile.f = currentTile.h;

        while (openList.Count > 0)
        {
            Tile t = FindLowestF(openList);

            closedList.Add(t);

            if (t == target)
            {
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }

            foreach (Tile tile in t.adjacencyList)
            {
                if (closedList.Contains(tile))
                {
                    // Already processed
                }
                else if (openList.Contains(tile))
                {
                    // Found second way to tile that is already on open list
                    // Check if there is a better path to get to the tile
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.g)
                    {
                        tile.parent = t;
                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    // First time node has been seen
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }
            }
        }

        // If path not found
        // ADD CODE
        Debug.Log("Path not found");

    }

    protected Tile FindEndTile(Tile t)
    {
        Stack<Tile> tempPath = new Stack<Tile>();

        // Dont include first node because tile will have a unit on it
        Tile next = t.parent;
        while (next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= unit.unitClass.Movement.Value)
        {
            return t.parent;
        }

        Tile endTile = null;
        for (int i = 0; i <= unit.unitClass.Movement.Value; i++)
        {
            endTile = tempPath.Pop();
        }
        return endTile;
    }

    protected Tile FindLowestF(List<Tile> list)
    {
        Tile lowest = list[0];

        foreach (Tile t in list)
        {
            if (t.f < lowest.f)
                lowest = t;
        }
        list.Remove(lowest);

        return lowest;
    }

    // How can I reorganize so I dont need this?
    public override void Execute()
    {
 
    }
}
