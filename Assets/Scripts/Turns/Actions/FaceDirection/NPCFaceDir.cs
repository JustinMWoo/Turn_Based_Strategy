using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFaceDir : TacticsFaceDir
{
    public override void Execute()
    {
        Done();
        TurnManager.EndAction(true, true);
    }

    public override void Done()
    {

    }
}
