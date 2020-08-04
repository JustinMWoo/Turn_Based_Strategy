using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerFaceDir : TacticsFaceDir
{
    bool init = false;
    Vector3 startDir;

    public override void Execute()
    {
        Debug.DrawRay(transform.position, transform.forward*5, Color.red);
        if (!init)
        {
            startDir = transform.forward;
            init = true;
        }

        CheckMouse();

        // User clicks and mouse is not over the UI
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            init = false;
            Done();
            TurnManager.EndAction(true);
        }
    }

    void CheckMouse()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        //Create ray from the location of the mouse click
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Sets entry to the distance along the ray where it intersects the plane
            if (plane.Raycast(ray, out float entry))
            {
                Vector3 clickLocation = ray.GetPoint(entry);
                FaceDirection(clickLocation);
            }
        }
        else
        {
            transform.forward = startDir;
        }
    }
    public override void Done()
    {
        init = false;
    }
}
