using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public static CameraFollow Instance;

	private void Awake()
	{
		if (Instance)
		{
			Destroy(gameObject);
		}
		Instance = this;
	}

	public Vector3 Offset;
	public IFieldObject Target;

	private void Update()
	{
		if (Target is null)
			return;

		transform.position = Target.Position + Offset;
	}
}
