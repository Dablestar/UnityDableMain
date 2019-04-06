using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    // Start is called before the first frame update
    
    override public void Start()
    {
        _character.StopMove();
        _character.PlayAnimation("idle", null);
    }

    // Update is called once per frame
}
