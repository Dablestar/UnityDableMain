using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModule : CharacterModule
{
    public PlayerModule(Character character):base(character)
    {
    }
    // Start is called before the first frame update
    override public void BuildStateList()
    {
        base.BuildStateList();
        _character.GetStateDic().Add(Character.eState.IDLE, new PlayerIdleState());
        /*
        _character.GetStateDic().Add(Character.eState.WAIT, new WaitState());
        _character.GetStateDic().Add(Character.eState.KICK, new KickState());
        _character.GetStateDic().Add(Character.eState.WALK, new WalkState());
        _character.GetStateDic().Add(Character.eState.RUN, new RunState());
        _character.GetStateDic().Add(Character.eState.SLIDE, new SlideState());
        _character.GetStateDic().Add(Character.eState.PATROL, new PatrolState());
        _character.GetStateDic().Add(Character.eState.DEATH, new DeathState());
        */
        for(int i = 0; i < _character.GetStateDic().Count; i++)
        {
            Character.eState state = (Character.eState)i;
           _character.GetStateDic()[state].SetCharacter(_character);
        }

        _character.ChangeState(Character.eState.IDLE);
    }
    override public void UpdateAI()
    {
        base.UpdateAI();
            if (true == Input.GetMouseButtonUp(0))   // 유니티에서 마우스 입력 처리 방식
            {
                Vector2 clickPos = Input.mousePosition;

                // 클릭한 화면좌표와 대응되는 월드 좌표 알아내야함.
                // Raycast 사용
                Ray ray = Camera.main.ScreenPointToRay(clickPos);
                RaycastHit hitInfo;
                if (true == Physics.Raycast(ray, out hitInfo, 100.0f, 1 << LayerMask.NameToLayer("Ground")))
                {
                    Vector3 destPos = hitInfo.point;
                   _character.SetDestination(destPos);
                   _character.ChangeState(Character.eState.WALK);
                }

            }

    }

    // Update is called once per frame
}
