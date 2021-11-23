using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment Object", menuName = ("ScriptableObject/Attributes/New Buff Attribute"))]
public class BuffAttributes : ScriptableObject
{
    public string Name;
    public int Id = -1;
}
