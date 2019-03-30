﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] AnimationController _animationController;
    [SerializeField] List<GameObject> _wayPointList;

    void Awake()
    {
        _characterController = gameObject.GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _stateDic.Add(eState.IDLE, new IdleState());
        _stateDic.Add(eState.WAIT, new WaitState());
        _stateDic.Add(eState.KICK, new KickState());
        _stateDic.Add(eState.WALK, new WalkState());
        _stateDic.Add(eState.RUN, new RunState());
        _stateDic.Add(eState.SLIDE, new SlideState());
        _stateDic.Add(eState.PATROL, new PatrolState());
        _stateDic.Add(eState.DEATH, new DeathState());

        for (int i=0; i< _stateDic.Count; i++)
        {
            eState state = (eState)i;
            _stateDic[state].SetCharacter(this);
        }

        ChangeState(eState.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        UpdateMove();
        if (eState.DEATH != _stateType)
        {
            UpdateDeath();
        }

    }

    // 상태

    public enum eState
    {
        IDLE,
        WAIT,
        KICK,
        WALK,
        RUN,
        SLIDE,
        PATROL,
        DEATH,
    }

    eState _stateType = eState.IDLE;

    public void ChangeState(eState state)
    {
        _stateType = state;
        _state = _stateDic[state];
        _state.Start();
    }

    void UpdateState()
    {
        _state.Update();
    }


    // State

    Dictionary<eState, State> _stateDic = new Dictionary<eState, State>();
    State _state = null;


    // Animation

    public void PlayAnimation(string trigger, System.Action endCallback)
    {
        _animationController.Play(trigger, endCallback);
    }


    // Movement

    CharacterController _characterController;
    float _moveSpeed = 0.0f;
    Vector3 _destPoint;

    void UpdateMove()
    {
        Vector3 moveDirection = GetMoveDirection();
        Vector3 moveVelocity = moveDirection * _moveSpeed;
        Vector3 gravityVelocity = Vector3.down * 9.8f;  // 중력

        Vector3 finalVelocty = (moveVelocity + gravityVelocity) * Time.deltaTime;
        _characterController.Move(finalVelocty);

        // 현재 위치와 목적지 까지의 거리를 계산해서
        // 적절한 범위 내에 들어오면 스톱.
        if(0.0f < _moveSpeed)
        {
            /*
            Vector3 charPos = transform.position;
            Vector3 curPos = new Vector3(charPos.x, 0.0f, charPos.z);
            Vector3 destPos = new Vector3(_destPoint.x, 0.0f, _destPoint.z);
            float distance = Vector3.Distance(curPos, destPos);
            */
            float distance = GetDistanceToTarget();
            if (distance < 0.5f)
            {
                _moveSpeed = 0.0f;
                ChangeState(eState.IDLE);
            }
        }
    }

    public float GetDistanceToTarget()
    {
        Vector3 charPos = transform.position;
        Vector3 curPos = new Vector3(charPos.x, 0.0f, charPos.z);
        Vector3 destPos = new Vector3(_destPoint.x, 0.0f, _destPoint.z);
        float distance = Vector3.Distance(curPos, destPos);
        return distance;
    }

    public void StartMove(float speed)
    {
        _moveSpeed = speed;
    }

    public void StopMove()
    {
        _moveSpeed = 0.0f;
    }

    public Vector3 GetWayPoint(int index)
    {
        return _wayPointList[index].transform.position;
    }

    public Vector3 GetRandomWayPoint()
    {
        int index = Random.Range(0, _wayPointList.Count);
        return GetWayPoint(index);
    }

    public int GetWayPointCount()
    {
        return _wayPointList.Count;
    }

    public void setWaypointList(List<GameObject> wayPointList)
    {
        _wayPointList = wayPointList;
    }

    public void SetDestination(Vector3 destPoint)
    {
        _destPoint = destPoint;
    }

    Vector3 GetMoveDirection()
    {
        // (목적위치 - 현재 위치) 노멀라이즈
        Vector3 charPos = transform.position;
        Vector3 curPos = new Vector3(charPos.x, 0.0f, charPos.z);
        Vector3 destPos = new Vector3(_destPoint.x, 0.0f, _destPoint.z);
        Vector3 direction = (destPos - curPos).normalized;

        Vector3 lookPos = new Vector3(_destPoint.x, charPos.y, _destPoint.z);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(
                                    transform.rotation,
                                    targetRotation,
                                    360.0f * Time.deltaTime);
        
        return direction;
    }

    float _deathTime = 30.0f;
    float _lifeTime = 0.0f;
    private void UpdateDeath()
    {
        if(_deathTime <= _lifeTime)
        {
            ChangeState(eState.DEATH);
        }
        _lifeTime += Time.deltaTime;
    }

    int _meetCounter = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(gameObject))
        {
            return;
        }
        _meetCounter++;
        if(_meetCounter >= 4)
        {
            ChangeState(eState.DEATH);
        }
        _lifeTime = 0.0f;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(gameObject))
        {
            return;
        }
        _meetCounter--;
    }
}
