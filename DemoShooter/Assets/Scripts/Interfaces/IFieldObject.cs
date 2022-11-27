using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFieldObject
{
    /// <summary>
    /// getter : public
    /// setter : public
    /// </summary>
    Vector3 Position { get; set; }
    
    /// <summary>
    /// getter : public
    /// setter : private
    /// </summary>
    Transform Transform { get; }
    
    /// <summary>
    /// getter : public
    /// setter : private
    /// </summary>
    float Angle { get; }
}
