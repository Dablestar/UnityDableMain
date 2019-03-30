using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    // Start is called before the first frame update
    override public void Start()
    {
        _character.PlayAnimation("death", () =>
        {
            GameObject.Destroy(_character.gameObject);
        });
    }
}
