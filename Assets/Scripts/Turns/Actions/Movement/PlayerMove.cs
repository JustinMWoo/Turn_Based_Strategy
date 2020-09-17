﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : TacticsMove
{
    public override void Execute()
    {
        //Debug.DrawRay(transform.position, transform.forward);

        if (!unit.turn)
        {
            return;
        }

        if (!moving)
        {
            FindMoveableTiles();
            CheckMouse();
        }
        else
        {
            TurnManager.playerUnitTurnStart = false;
            Move(true);
        }
    }
    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                // Tile is clicked on
                if(hit.collider.CompareTag("Tile"))
                {
                    // Get tile script
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
            }
        }
    }
}
