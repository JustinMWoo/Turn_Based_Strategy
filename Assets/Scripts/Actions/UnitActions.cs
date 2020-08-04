﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitActions : MonoBehaviour
{
    protected GameObject[] tiles;

    protected Tile currentTile;
    protected Tile targetTile;

    protected Vector3 velocity = new Vector3(); // How fast the player moves from tile to tile
    protected Vector3 heading = new Vector3(); // Direction character is heading in
    public float moveSpeed = 2;
    protected float halfHeight = 0; // Distance from tile to center of the player

    protected Unit unit;

    public abstract void Execute();
    public abstract void Done();

    protected virtual void Init()
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

    public void ComputeAdjacencyLists(float jumpHeight, Tile target, bool attack)
    {
        // Move this into here if tiles are going to be added or removed during gameplay
        // tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            // Get tile script from the game object
            Tile t = tile.GetComponent<Tile>();

            t.FindNeighbors(jumpHeight, target, attack);
        }
    }

    protected void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;

        // Make a unit vector (normalize the values)
        heading.Normalize();
    }

    protected void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

}