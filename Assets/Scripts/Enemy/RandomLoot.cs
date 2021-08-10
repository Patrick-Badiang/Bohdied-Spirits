using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = ("New Loot Table"), menuName = ("ScriptableObject/LootTables/New Table"))]
public class RandomLoot : ScriptableObject
{
    public ChanceDrop[] equipmentThatCanBeDropped;

    
}

[System.Serializable]
public class ChanceDrop{
    public GameObject equipmentToBeDropped;
    
    [Range (1,100)]
    public int chanceOfDrop;
    

}
