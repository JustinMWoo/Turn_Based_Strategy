using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsChainLightning : TacticsAbility
{
    protected int range = 4;
    protected int chainDistance = 3;
    protected int damage = 20;

    protected int cooldownMax = 5;

    List<Tile> selectableTiles = new List<Tile>();
    List<Tile> chainTiles = new List<Tile>();
    List<Tile> potentialChainTiles = new List<Tile>();

    protected bool attacking;

    public TacticsChainLightning()
    {
        levelRequirement = 5;
        id = "ChainLightning";
    }


    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists(1, null, true, false);
        GetCurrentTile();

        Queue<Tile> queue = new Queue<Tile>();

        queue.Enqueue(currentTile);
        currentTile.visited = true;

        while (queue.Count > 0)
        {
            Tile t = queue.Dequeue();

            RaycastHit hit;
            if (Physics.Raycast(t.transform.position, Vector3.up, out hit, 1))
            {
                Unit targetUnit = hit.collider.GetComponent<Unit>();

                if (targetUnit != null && !unit.CompareTag(targetUnit.tag))
                {
                    selectableTiles.Add(t);
                    t.selectable = true;
                }
            }

            t.attackRange = true;

            if (t.distance < range)
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

    public void FindChainTargets(Tile target)
    {
        potentialChainTiles.Clear();
        chainTiles.Clear();

        targetTile = target;

        // Using chain distance as value for height
        ComputeAdjacencyLists(chainDistance, null, true, true);

        Queue<Tile> queue = new Queue<Tile>();

        queue.Enqueue(target);
        target.visited = true;

        while (queue.Count > 0)
        {
            Tile t = queue.Dequeue();

            RaycastHit hit;

            // Check for units on tiles
            if (!t.Equals(targetTile) && Physics.Raycast(t.transform.position, Vector3.up, out hit, 1))
            {
                Unit targetUnit = hit.collider.GetComponent<Unit>();

                // Only add to the potential chain targets if the unit is on a different team
                if (targetUnit != null && !unit.CompareTag(targetUnit.tag))
                {
                    potentialChainTiles.Add(t);
                }
            }

            if (t.distance < chainDistance)
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

        foreach (Tile newChainTile in potentialChainTiles)
        {
            if (chainTiles.Count < 1)
            {
                chainTiles.Add(newChainTile);
            }
            else if (newChainTile.distance == chainTiles[0].distance)
            {
                chainTiles.Add(newChainTile);
            }
            else if (newChainTile.distance < chainTiles[0].distance)
            {
                chainTiles.Clear();
                chainTiles.Add(newChainTile);
            }
        }

        foreach (Tile actualChainTile in chainTiles)
        {
            actualChainTile.AOE = true;
        }

        chainTiles.Add(targetTile);
    }

    public void Attack()
    {
        CameraController.instance.panTarget = targetTile.transform;

        RaycastHit hit;
        foreach (Tile tile in chainTiles)
        {
            // Check for units on tiles
            if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
            {
                Unit targetUnit = hit.collider.GetComponent<Unit>();
                if (targetUnit != null)
                {
                    targetUnit.TakeDamage(damage);
                }
            }
        }
        Done();
        unit.abilityCooldowns[id] = cooldownMax;
        TurnManager.EndAction(true, true);
    }

    protected void RemoveSelectableAndChainTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
            targetTile = null;
        }

        foreach (Tile tile in selectableTiles)
        {
            tile.Reset(false);
        }

        foreach (Tile tile in potentialChainTiles)
        {
            tile.Reset(false);
        }
        foreach (Tile tile in chainTiles)
        {
            tile.Reset(false);
        }

        selectableTiles.Clear();
        potentialChainTiles.Clear();
        chainTiles.Clear();
    }

    public override void Done()
    {
        RemoveSelectableAndChainTiles();
        attacking = false;
    }
}
