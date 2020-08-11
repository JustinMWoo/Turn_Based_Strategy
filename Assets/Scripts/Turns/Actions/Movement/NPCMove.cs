using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove
{
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Execute()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (!unit.turn)
        {
            return;
        }

        if (!moving)
        {
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;
        }
        else
        {
            Move();
        }
    }

    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
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
