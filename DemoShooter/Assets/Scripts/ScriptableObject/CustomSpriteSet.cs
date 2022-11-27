using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpriteSet", menuName = "Scriptable Object/Create new Sprite set", order = 1)]
public class CustomSpriteSet : ScriptableObject
{
    public float DefaultFPS = 10f;
    
    public List<Sprite> sprites = new List<Sprite>();
}
