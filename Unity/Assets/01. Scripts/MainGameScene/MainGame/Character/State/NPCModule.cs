using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCModule : CharacterModule
{

    public NPCModule(Character character):base(character)
    {
    }
    // Start is called before the first frame update
    override public void BuildStateList()
    {
        base.BuildStateList();
        _character.GetStateDic().Add(Character.eState.IDLE, new IdleState());
        /*
        
        _character.GetStateDic().Add(Character.eState.WAIT, new WaitState());
        _character.GetStateDic().Add(Character.eState.KICK, new KickState());
        _character.GetStateDic().Add(Character.eState.WALK, new WalkState());
        _character.GetStateDic().Add(Character.eState.RUN, new RunState());
        _character.GetStateDic().Add(Character.eState.SLIDE, new SlideState());
        _character.GetStateDic().Add(Character.eState.PATROL, new PatrolState());
        _character.GetStateDic().Add(Character.eState.DEATH, new DeathState());
        */
        for (int i = 0; i < _character.GetStateDic().Count; i++)
        {
            Character.eState state = (Character.eState)i;
            _character.GetStateDic()[state].SetCharacter(_character);
        }

        _character.ChangeState(Character.eState.IDLE);
    }

    // Update is called once per frame
}
