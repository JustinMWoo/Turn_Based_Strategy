using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: inheriting from tactics move is weird so eventually change and reorganize file system so it makes more sense
public class TacticsAI : TacticsMove
{
    List<Tile> moveableTiles = new List<Tile>();
    // Tiles that you unit can attack from and reach a target
    List<Tile> tilesWithTarget = new List<Tile>();
    List<Unit> party;
    Tile bestTile;
    GameObject target;

    bool targetSearchPerformed = false;
    bool targetSearchFinished = false;
    bool bestTargetSearchPerformed = false;
    bool bestTargetFound = false;
    bool dealingDamage = false;
    bool onlyMove;

    protected override void Start()
    {
        base.Start();

        // TODO: Maybe replace with something more efficient?
        party = GameObject.Find("PartySystem").GetComponent<PartySystem>().GetParty();

    }
    public override void Done()
    {
        base.Done();
        moveableTiles.Clear();
        tilesWithTarget.Clear();
        bestTile = null;
        target = null;
        targetSearchPerformed = false;
        targetSearchFinished = false;
        bestTargetSearchPerformed = false;
        bestTargetFound = false;
        dealingDamage = false;
    }

    public override void Execute()
    {
        if (!targetSearchPerformed)
        {
            FindTilesWithTarget();
            targetSearchFinished = true;
        }
        else if (targetSearchFinished && !bestTargetSearchPerformed)
        {
            // If there are no targets within range
            if (tilesWithTarget.Count < 1)
            {
                bestTargetSearchPerformed = true;
                onlyMove = true;
                // Path to nearest target
                FindNearestTarget();
                bestTile = GetTargetTile(target);
                bestTargetFound = true;
            }
            else
            {
                bestTargetSearchPerformed = true;
                onlyMove = false;
                bestTile = CalculateTileScores();
                bestTargetFound = true;
            }
        }
        else if (bestTargetFound && !doneMoving)
        {
            // A* to path to nearest
            if (!moving)
            {
                FindPath(bestTile, onlyMove);
                FindMoveableTiles();
                bestTile.target = true;
            }
            else
            {
                Move(false);
            }
        }
        else if (doneMoving && !dealingDamage)
        {
            dealingDamage = true;

            // Deal damage and end turn if in range
            // TODO: This is temporary to deal the damage, need to reorganize the file structure so I can access the stuff in tactics attack and use it here
            if (bestTile.bestTarget != null)
            {
                DamageCalculator.Current.DealDamage(unit, bestTile.bestTarget, DamageType.Physical);
            }


            Done();
            TurnManager.EndAction(true, true);
        }

    }


    // Returns the player units that are attackable from the location that the NPC is at
    private Unit[] InRangeOfUnits()
    {
        /* Intead of computing adjacency lists, try using find neighbors as needed for each tile
        * Then use 
        * if (t.distance < unit.unitClass.Movement.Value)
        *   find neighbors with units jump height
        * else if (t.distance < unit.unitClass.Movement.Value + unit.Weapon.weaponRange
        *   find neighbors with weapon range
        *
        * OR
        * 
        * just do a bfs from each tile that you can move to to see if there is a unit to attack (might be very costly)
        * should then store two adjecency lists in the tiles (one for using the weapon verticality and one with jump height)
        * and do a check to see if any of the tiles have a possible target (can use a bool and mark true if a target is found)
        */



        return null;
    }

    private void FindTilesWithTarget()
    {
        // Ensures this is only run once per turn
        targetSearchPerformed = true;

        // TODO: If enemy has no weapon?
        ComputeAdjacencyListsAI(unit.unitClass.JumpHeight.Value, unit.Weapon.WeaponVerticality);
        GetCurrentTile();

        Queue<Tile> queue = new Queue<Tile>();

        queue.Enqueue(currentTile);
        currentTile.visited = true;

        while (queue.Count > 0)
        {
            Tile t = queue.Dequeue();

            moveableTiles.Add(t);
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

        foreach (Tile tile in moveableTiles)
        {
            tile.Reset(false, true);
        }

        List<Tile> curAttackableTiles = new List<Tile>();

        // BFS from each player unit using AI unit's attack range to see if overlap between these tiles and tiles that can be moved to
        foreach (Unit playerUnit in party)
        {
            if (playerUnit.gameObject.activeSelf)
            {
                Tile playerUnitTile = GetTargetTile(playerUnit.gameObject);

                queue.Enqueue(playerUnitTile);
                playerUnitTile.visited = true;

                while (queue.Count > 0)
                {
                    Tile t = queue.Dequeue();

                    curAttackableTiles.Add(t);

                    // If there is overlap between the lists then the AI can attack move to that tile and attack the unit
                    if (moveableTiles.Contains(t))
                    {
                        if (!tilesWithTarget.Contains(t))
                        {
                            tilesWithTarget.Add(t);
                        }
                        t.targetList.Add(GetUnitOnTile(playerUnitTile));
                    }

                    //t.selectable = true;

                    if (t.distance < unit.Weapon.WeaponRange)
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

                // Reset all tiles
                foreach (Tile tile in curAttackableTiles)
                {
                    tile.Reset(false, false);
                }
            }
        }

        //Debug.Log("Num of tiles with target " + tilesWithTarget.Count);
        //foreach (Tile tile in tilesWithTarget)
        //{
        //    Debug.DrawRay(tile.transform.position, Vector3.up, Color.red, 999);
        //}
    }


    private Tile CalculateTileScores()
    {
        Tile bestTile = null;
        int bestTileScore = int.MinValue;

        foreach (Tile tile in tilesWithTarget)
        {
            int bestScore = int.MinValue;
            Unit bestTarget;
            int score;

            // Calculate a score for each target using dealable damage, dodge and if the attack will kill
            foreach (Unit target in tile.targetList)
            {
                score = 0;

                // Calculate damage to the target from the location of the tile that can be pathed to
                int damage = DamageCalculator.Current.CalculateDamage(unit, target, DamageType.Physical, false, tile.transform);
                if (damage >= target.currentHP) // The attack will kill the target
                {
                    // Adding weapon damage to have a flat value
                    // Ex without, unit at 1 hp would have very low chance of getting targeted
                    score += damage * 2 + unit.Weapon.Damage * 2;
                }
                else
                {
                    score += damage;
                }

                score -= (int)DamageCalculator.Current.CalculateDodge(target) / 3;

                 if (score > bestScore)
                {
                    tile.score = bestScore = score;
                    tile.bestTarget = bestTarget = target;
                }
                //Debug.Log("Tile " + tile.gameObject +" has score: " + score);
            }

            //Debug.Log("Next score: " + bestScore + " Best tile score: " + bestTileScore);

            if (bestScore > bestTileScore)
            {
                //Debug.Log("Best tile: " + tile.gameObject);
                bestTileScore = bestScore;
                bestTile = tile;
            }
        }

        //// Calculate distance scores
        //foreach (Unit playerUnit in party)
        //{
        //    if (playerUnit.gameObject.activeSelf)
        //    {
        //        Queue<Tile> queue = new Queue<Tile>();

        //        ComputeAdjacencyListsAI(playerUnit.unitClass.JumpHeight.Value, unit.Weapon.WeaponVerticality);

        //        Tile playerUnitTile = GetTargetTile(playerUnit.gameObject);

        //        queue.Enqueue(playerUnitTile);
        //        playerUnitTile.visited = true;

        //        while (queue.Count > 0)
        //        {
        //            Tile t = queue.Dequeue();

        //            // Add the weapon range and movement to get a general range of the unit
        //            if (t.distance < playerUnit.Weapon.WeaponRange + playerUnit.unitClass.Movement.Value)
        //            {
        //                // This player unit can attack* this tile
        //                // (May not be able to attack since it is the general range of the unit and does not consider weapon verticality)

        //                // RIGHT NOW THIS MEANS THAT IF THE UNIT CAN BE ATTACKED AT THAT LOCATION SCORE WILL INCREASE FOR TILE
        //                // BUT THIS MEANS THAT IF ON A TILE THAT CANNOT BE ATTACKED THEN LOWER SCORE
        //                if (tilesWithTarget.Contains(t))
        //                {
        //                    t.score += t.distance;
        //                }

        //                foreach (Tile adjTile in t.adjacencyList)
        //                {
        //                    if (!adjTile.visited)
        //                    {
        //                        adjTile.parent = t;
        //                        adjTile.distance = t.distance + 1;
        //                        adjTile.visited = true;

        //                        queue.Enqueue(adjTile);
        //                    }
        //                }
        //            }
        //        }

        //        // Reset all tiles
        //        foreach (Tile tile in curAttackableTiles)
        //        {
        //            tile.Reset(false, false);
        //        }
        //    }



            return bestTile;
    }

    void FindNearestTarget()
    {
        // Get all player units
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            // Calculate distance from this unit to player unit in array
            // Square magnitude for vector3 is more efficient
            float d = Vector3.Distance(transform.position, obj.transform.position);

            if (d < distance)
            {
                nearest = obj;
                distance = d;
            }
        }
        target = nearest;
    }
}
