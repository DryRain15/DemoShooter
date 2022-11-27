using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Scriptable Object/Create new Stat", order = 1)]
public class Stats : ScriptableObject
{
    public float MaxHP = 50f;
    public float CurrentHP = 50f;
        
    public float MaxMP = 100f;
    public float CurrentMP = 100f;
    
    public float MoveSpeed = 3f;
    
    public float AtkBase = 3f;
    public float AtkMult = 1f;

    public float DefBase = 0f;
    public float DefMult = 1f;

    public float SpBase = 1f;
    public float SpMult = 1f;

    public override string ToString()
    {
        return $"HP: {CurrentHP:00.00} / {MaxHP:00.00}\nMP: {CurrentMP} / {MaxMP}\n Speed: {MoveSpeed}\nAtk: {AtkBase * AtkMult}\nDef: {DefBase * DefMult}";
    }

    public Stats CopyTo(Stats target)
    {
        target.MaxHP = MaxHP;
        target.CurrentHP = CurrentHP;
        target.MaxMP = MaxMP;
        target.CurrentMP = CurrentMP;
        target.MoveSpeed = MoveSpeed;
        target.AtkBase = AtkBase;
        target.AtkMult = AtkMult;
        target.DefBase = DefBase;
        target.DefMult = DefMult;
        target.SpBase  = SpBase ;

        return target;
    }
}
