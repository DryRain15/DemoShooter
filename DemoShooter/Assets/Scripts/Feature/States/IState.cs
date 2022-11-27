using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public IStateHolder holder { get; }
    public void OnStartState();
    public void OnState();
    public void OnEndState();
}
