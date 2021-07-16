using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = ("ScriptableObject/Items/New Food Item"))]
public class FoodObject : ItemObject
{
    public void Awake(){
        type = ItemType.Food;
    }
}
