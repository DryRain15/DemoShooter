using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEvent : IEvent
{
    public DamageInfo Info;
    
    public DamageEvent(DamageInfo info)
    {
        Info = info;
    }

    public override string ToString()
    {
        return $"Damage Occured!\n | Sender: {Info.Sender.name}\n | Getter: {Info.Getter.name}\n | Type: {Info.Type}\n | Damage: {Info.Damage}";
    }
}

public enum DamageType
{
    Beam,
    Bullet,
    Explosion,
    Melee,
    Contact,
    Normal,
    Trap,
}

public struct DamageInfo
{
    public Character Sender;
    public Character Getter;

    public DamageType Type;
    
    /// <summary>
    /// 피격자의 방어력 및 공격자의 방어 무시 스탯이 반영되기 전의 대미지 수치
    /// </summary>
    public float Damage;
}
