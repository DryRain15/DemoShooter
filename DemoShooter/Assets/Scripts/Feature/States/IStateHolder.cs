using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateHolder
{
    public Character character { get; }
    public IState CurrentState { get; }
    public void SetState(IState state);
}
