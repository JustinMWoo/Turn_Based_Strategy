using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractAction : UnitActions
{
    bool interactablesChecked = false;
    bool interactableFound = false;

    public override void Execute()
    {
        if (!interactablesChecked)
        {
            FindInteractableTiles();
        }

        if (!interactableFound)
        {
            Done();
            TurnManager.EndAction(false, false);
        }
        else
        {
            CheckMouse();
        }
    }

    protected void FindInteractableTiles()
    {
        GetCurrentTile();
        currentTile.FindNeighbors(0, null, true, false);

        RaycastHit hit;

        // For each tile around the tile the unit is on check for chests
        foreach (Tile tile in currentTile.adjacencyList)
        {
            if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
            {
                if (hit.collider.CompareTag("Chest"))
                {
                    tile.selectable = true;
                    interactableFound = true;
                }
            }
        }
        interactablesChecked = true;
    }
    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Tile is clicked on
                if (hit.collider.CompareTag("Tile"))
                {
                    // Get tile script
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        Interact(t);
                    }
                }
            }
        }
    }

    void Interact(Tile tile)
    {
        RaycastHit hit;
        if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
        {
            if (hit.collider.CompareTag("Chest") && !hit.collider.GetComponent<Chest>().opened)
            {
                hit.collider.GetComponent<Animator>().Play("OpenChest");
                // TODO: Add chests item to units inventory
            }
        }
    }

    public override void Done()
    {
        interactablesChecked = false;
        foreach (Tile tile  in currentTile.adjacencyList)
        {
            tile.Reset(false);
        }

        currentTile.Reset(false);
    }

   


}
