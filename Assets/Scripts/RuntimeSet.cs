using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    private List<T> items = new List<T>(); //Not going to be accessed through inspector

    public void Initialize(){
        items.Clear();
    }

    public T GetItemIndex(int index){

        return items[0]; //returns any item index

    }

    public void AddToList(T thingToAdd){

        if(!items.Contains(thingToAdd)){ //a safety check so that there are no duplicates
            items.Add(thingToAdd);
        }

    }

    public void RemoveFromList(T thingToRemove){

        if(items.Contains(thingToRemove)){
            items.Remove(thingToRemove);
        }
        
    }
}
