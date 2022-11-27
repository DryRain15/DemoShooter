using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlStateHolder : IStateHolder
{
	public ControlStateHolder(Character parent, IState defaultState = null)
	{
		character = parent;
		SetState(defaultState);
	}
	
	public Character character { get; private set; }
	public IState CurrentState { get; private set; }
	
	public void SetState(IState state)
	{
		CurrentState?.OnEndState();

		CurrentState = state;
		CurrentState?.OnStartState();
	}
}
