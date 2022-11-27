using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSprite : MonoBehaviour
{
	private SpriteRenderer _sr;

	public CustomSpriteSet spriteSet;

	private float _innerTimer;
	private int _currentFrameIdx;
	
	private void Awake()
	{
		_sr = GetComponent<SpriteRenderer>();
		_innerTimer = 0f;
	}

	private void Update()
	{
		// 각종 예외처리
		if (spriteSet is null)
			return;
		
		if (spriteSet.sprites.Count == 0)
			return;

		var spriteCount = spriteSet.sprites.Count;
		var dt = Time.deltaTime;

		if (_innerTimer > 1f / spriteSet.DefaultFPS)
		{
			_innerTimer = 0f;
			_currentFrameIdx++;
		}
		
		if (_currentFrameIdx >= spriteCount)
			_currentFrameIdx = 0;
		
		if (_currentFrameIdx < 0)
			_currentFrameIdx = spriteCount - 1;

		_sr.sprite = spriteSet.sprites[_currentFrameIdx];

		_innerTimer += dt;
	}
}
