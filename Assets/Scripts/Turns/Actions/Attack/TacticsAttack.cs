using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsAttack : UnitActions
{
    #region Variables

    List<Tile> attackableTiles = new List<Tile>();
    List<Tile> processedTiles = new List<Tile>();

    Vector3 targetVector;
    Vector3 returnVector;

    protected bool attacking = false;
    bool attacked = false;
    bool battleCameraActive = false;
    bool defocusing = false;

    bool moveToTarget = false;
    bool damageCalc = false;
    bool moveBack = false;
    GameObject attackTarget;
    #endregion

    // BFS for attackable tiles
    public void FindAttackableTiles()
    {
        RaycastHit hit;
        // REPLACE WITH VERTICALITY ATTACK VALUE
        ComputeAdjacencyLists(unit.weaponVerticality, null, true);
        GetCurrentTile();

        Queue<Tile> queue = new Queue<Tile>();

        queue.Enqueue(currentTile);
        currentTile.visited = true;

        while (queue.Count > 0)
        {
            Tile t = queue.Dequeue();

            // Debug.Log("Checking Attackable Tiles " + t.transform.position);
            // Debug.DrawRay(t.transform.position, Vector3.up, Color.green, 20.0f);

            // Only add to attackable tiles if there is a unit on top
            if (Physics.Raycast(t.transform.position, Vector3.up, out hit, 1))
            {
                // Debug.Log(hit.transform.tag);
                if ((hit.collider.CompareTag("NPC") && unit.CompareTag("Player")) || (hit.collider.CompareTag("Player") && unit.CompareTag("NPC")))
                {
                    // Debug.Log("Attackable tile found");
                    attackableTiles.Add(t);
                    t.selectable = true;
                }
            }
            processedTiles.Add(t);
            t.attackRange = true;

            // CHANGE TO RANGE VALUE OF EQUIPPED WEAPON ON UNIT
            if (t.distance < unit.weaponRange)
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

        // If there are no enemies in range
        if (attackableTiles.Count < 1)
        {
            TurnManager.EndAction(attacked, false);
            Done();
        }
    }
    protected void RemoveAttackableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
            targetTile = null;
        }

        foreach (Tile tile in processedTiles)
        {
            tile.Reset();
        }

        attackableTiles.Clear();
        processedTiles.Clear();
    }

    public void AttackTile(Tile tile)
    {
        tile.target = true;
        // After you set destination you can no longer move
        attacking = true;
        moveToTarget = true;

        targetTile = tile;

        // Get unit being attacked
        RaycastHit hit;
        if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
        {
            attackTarget = hit.collider.gameObject;
        }

        targetVector = targetTile.transform.position;
        returnVector = currentTile.transform.position;

        targetVector.y += halfHeight + tile.GetComponent<Collider>().bounds.extents.y;
        returnVector.y += halfHeight + tile.GetComponent<Collider>().bounds.extents.y;
    }

    public void Attack()
    {
        // Debug.Log("Attacking");
        // if (unit.weaponRange) > 2
        // code ranged attack animation
        if (!battleCameraActive)
        {
            BattleCameraOn();
            battleCameraActive = true;

            // Show the healthbars for the battle
            gameObject.GetComponent<Unit>().healthbar.gameObject.SetActive(true);
            attackTarget.GetComponent<Unit>().healthbar.gameObject.SetActive(true);
        }

        // Move to the target for melee attacks
        if (moveToTarget)
        {
            if (Vector3.Distance(transform.position, targetVector) >= 0.05f)
            {
                SetHorizontalVelocity();
                CalculateHeading(targetVector);

                transform.forward = heading; // Face direction unit is moving
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                moveToTarget = false;
                damageCalc = true;
            }
        }
        else if (damageCalc)
        {
            attackTarget.GetComponent<Unit>().TakeDamage(unit.weaponDamage);

            // ADD DAMAGE CALCULATIONS
            damageCalc = false;
            moveBack = true;
        }
        else if (moveBack)
        {
            if (Vector3.Distance(transform.position, returnVector) >= 0.05f)
            {
                SetHorizontalVelocity();
                CalculateHeading(returnVector);

                transform.forward = heading; // Face direction unit is moving
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                moveBack = false;
            }
        }
        else
        {
            if (!defocusing)
            {
                BattleCameraOff();

                // Face towards the unit that just got attacked
                // Inside of if so it only runs once at the first call
                // CHANGE THIS TO FACE THE SIDE RATHER THAN DIRECTLY FACE THE ENEMY (Only matters for ranged later)
                transform.forward = -transform.forward;
            }

            if (CameraController.instance.doneBattle)
            {
                // Turn of healthbars checking if units have died
                if (gameObject != null)
                    gameObject.GetComponent<Unit>().healthbar.gameObject.SetActive(false);
                if (attackTarget != null)
                    attackTarget.GetComponent<Unit>().healthbar.gameObject.SetActive(false);

                // Reset flags
                attacking = false;
                attacked = true;

                // End the action
                TurnManager.EndAction(attacked, true);
                Done();
            }
        }
    }

    // Moves camera into position for attack animation
    void BattleCameraOn()
    {
        CameraController.instance.FocusOnAttack(transform.position, attackTarget.transform.position);
    }

    // Returns camera control to the user
    void BattleCameraOff()
    {
        defocusing = true;
        CameraController.instance.Defocus();
    }

    public override void Done()
    {
        battleCameraActive = false;
        defocusing = false;
        RemoveAttackableTiles();
        attacked = false;
    }




    public override void Execute()
    {

    }


}
