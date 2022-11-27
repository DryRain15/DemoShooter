using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualDashState : IState
{
    public Character Character;
    public IStateHolder holder { get; private set; }
    private ObjectPool _laserFX;

    private Vector3 _direction;
    private float _innerTimer = 0f;

    private const float Duration = 0.12f;

    public ManualDashState(IStateHolder holder, Vector3 dir)
    {
        this.holder = holder;
        Character = holder.character;
        _direction = dir;
    }
    
    public void OnStartState()
    {
        _laserFX = ObjectPoolController.Instance.GetOrCreate("FX", "TestLaserFX");
        
        Vector3 targetPos = Character.Position + Character.Stat.MoveSpeed * 0.48f * _direction.normalized;
        var trail = _laserFX.Instantiate() as TestLaserFX;
        trail.Initiate(Character.Position, targetPos);
    }

    public void OnState()
    {
        // 각 캐릭터별 State 내부로 옮길 예정
        var dt = Time.deltaTime;

        Character.Position += dt * Character.Stat.MoveSpeed * 4f * _direction;
        
        // 커서 위치를 향해 슈팅
        
        if (_innerTimer > Duration)
        {
            holder.SetState(new ManualControlState(holder));
            return;
        }

        _innerTimer += dt;
    }

    public void OnEndState()
    {
    }
}
