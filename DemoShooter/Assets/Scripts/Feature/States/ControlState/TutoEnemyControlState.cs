using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoEnemyControlState : IState
{
    public Character Character;
    public IStateHolder holder { get; private set; }
    private ObjectPool _explosionFX;

    public TutoEnemyControlState(IStateHolder holder)
    {
        this.holder = holder;
        Character = holder.character;
    }

    public void OnStartState()
    {
        _explosionFX = ObjectPoolController.Instance.GetOrCreate("FX", "TestExplosionFX");
    }

    public void OnState()
    {
        // 각 캐릭터별 State 내부로 옮길 예정
        var dt = Time.deltaTime;
        var dist = (Game.Instance.player.Position - Character.Position).GetXY0();

        Character.Position += dt * Character.Stat.MoveSpeed * dist.normalized;

        if (Character.Stat.CurrentHP <= 0f)
        {
            var exp = _explosionFX.Instantiate() as TestExplosionFX;
            exp.Position = Character.Position;
            exp.Initiate(Character);
            
            ObjectPoolController.Instance.GetOrCreate("Objects/CharacterHolder").Dispose(Character);
            holder.SetState(null);
        }
    }

    public void OnEndState()
    {
        var chr = ObjectPoolController.Instance.GetOrCreate("Objects", "CharacterHolder").Instantiate() as Character;
        chr.Initiate(CharacterType.TutoEnemy);
    }
}
