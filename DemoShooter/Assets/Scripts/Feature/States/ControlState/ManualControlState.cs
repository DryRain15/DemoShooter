using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualControlState : IState
{
    public Character Character;
    public IStateHolder holder { get; private set; }

    private ObjectPool _laserFX;
    private ObjectPool _cannonFX;

    public ManualControlState(IStateHolder holder)
    {
        this.holder = holder;
        Character = holder.character;
    }
    
    public void OnStartState()
    {
        _laserFX = ObjectPoolController.Instance.GetOrCreate("FX", "TestLaserFX");
        _cannonFX = ObjectPoolController.Instance.GetOrCreate("FX", "TestCannonFX");
        ObjectPoolController.Instance.GetOrCreate("FX", "TestExplosionFX");
    }

    public void OnState()
    {
        // 각 캐릭터별 State 내부로 옮길 예정
        var dt = Time.deltaTime;
        var hInput = Input.GetAxisRaw("Horizontal");
        var vInput = Input.GetAxisRaw("Vertical");

        Character.Position += dt * Character.Stat.MoveSpeed * (hInput * Vector3.right + vInput * Vector3.up);
        
        // 커서 위치를 향해 슈팅
        var cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition).GetXY0();

        if (Input.GetMouseButtonDown(0))
        {
            var laserHit = Physics2D.Raycast(Character.Position, cursor - Character.Position, 30f,
                1 << LayerMask.NameToLayer("Enemy"));
            Vector3 targetPos = Character.Position + (cursor - Character.Position).normalized * 30f;

            if (laserHit)
            {
                targetPos = laserHit.point;
                MessageSystem.Publish(new DamageEvent(new DamageInfo()
                {
                    Damage = 3,
                    Getter = laserHit.collider.GetComponent<Character>(),
                    Sender = Character,
                    Type = DamageType.Beam
                }));
            }
            
            var laser = _laserFX.Instantiate() as TestLaserFX;
            laser.Initiate(Character.Position, targetPos);
        }
        
        
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 targetPos = Character.Position + (cursor - Character.Position).normalized * 6f;

            var cannon = _cannonFX.Instantiate() as TestCannonFX;
            cannon.Initiate(Character.Position, targetPos, Character);
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            holder.SetState(new ManualDashState(holder, (hInput * Vector3.right + vInput * Vector3.up)));
        }
    }

    public void OnEndState()
    {
    }
}
