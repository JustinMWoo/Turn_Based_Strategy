using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsFaceDir : UnitActions
{

    // Check the angle between the two locations and determine which direction to face
    public void FaceDirection(Vector3 target)
    {
        Vector3 noY = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 toTarget = target - noY;

        float angle = Vector3.SignedAngle(Vector3.forward, toTarget, Vector3.up);
        //Debug.Log(angle);

        // Face direction of target
        if (angle >= 45 && angle <= 135)
        {
            transform.forward = Vector3.right;
        }
        else if ((angle >= 135 && angle <= 180) || (angle >= -180 && angle <= -135))
        {
            transform.forward = -Vector3.forward;
        }
        else if (angle >= -135 && angle <= -45)
        {
            transform.forward = -Vector3.right;
        }
        else
        {
            transform.forward = Vector3.forward;
        }

    }
    public override void Done()
    {
        
    }

    public override void Execute()
    {

    }
}
