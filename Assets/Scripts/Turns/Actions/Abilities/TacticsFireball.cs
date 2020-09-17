using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsFireball : TacticsAbility
{
    protected int range = 5;
    protected int AOESize = 2;
    protected int damage = 20;

    protected int cooldownMax = 3;

    List<Tile> selectableTiles = new List<Tile>();
    List<Tile> AOETiles = new List<Tile>();

    protected bool attacking;

    public TacticsFireball()
    {
        levelRequirement = 5;
        id = "Fireball";
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

            selectableTiles.Add(t);
            t.selectable = true;

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

    public void FindAOETiles(Tile target)
    {
        AOETiles.Clear();
        targetTile = target;

        ComputeAdjacencyLists(1, null, true, true);

        Queue<Tile> queue = new Queue<Tile>();

        queue.Enqueue(target);
        target.visited = true;

        while (queue.Count > 0)
        {
            Tile t = queue.Dequeue();

            AOETiles.Add(t);
            t.AOE = true;

            if (t.distance < AOESize)
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

    public void Attack()
    {
        CameraController.instance.SetPanTarget(targetTile.transform.position);

        RaycastHit hit;
        foreach (Tile tile in AOETiles)
        {
           // Check for units on tiles
           if(Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
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
    protected void RemoveSelectableAndAOETiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
            targetTile = null;
        }

        foreach (Tile tile in selectableTiles)
        {
            tile.Reset(false, true);
        }

        foreach (Tile tile in AOETiles)
        {
            tile.Reset(false, true);
        }

        selectableTiles.Clear();
        AOETiles.Clear();
    }

    public override void Done()
    {
        RemoveSelectableAndAOETiles();
        attacking = false;
    }
}
