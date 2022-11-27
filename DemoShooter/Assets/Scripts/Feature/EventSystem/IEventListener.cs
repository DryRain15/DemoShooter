using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventListener
{
    public bool OnEvent(IEvent e);
}
