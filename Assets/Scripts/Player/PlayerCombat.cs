using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Attribute[] attributes;

    public void AttributeModified(Attribute attribute){
        Debug.Log(string.Concat(attribute.type, " was updated! Value: ",attribute.value.ModifiedValue ));
    }
}

[System.Serializable]
public class Attribute{
    [System.NonSerialized]
    public PlayerCombat parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(PlayerCombat _parent){
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }

    public void AttributeModified(){
        parent.AttributeModified(this);
    }
}