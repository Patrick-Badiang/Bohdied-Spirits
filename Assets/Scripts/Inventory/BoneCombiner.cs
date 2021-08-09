using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCombiner
{

    private readonly Dictionary<int, Transform> _rootBoneDictionary = new Dictionary<int, Transform>();
    //Dictionary of the characters bones

    private readonly Transform[] _boneTransforms = new Transform[21];
    //An array set too the size of the bone structure

    private readonly Transform _transform;

    public BoneCombiner(GameObject rootObj)
    {
        _transform = rootObj.transform;
        TraverseHierarchy(_transform);
    }

    public Transform AddLimb(GameObject bonedObj, List<string> boneNames)
    {
        var limb = ProcessBonedObject(bonedObj.GetComponentInChildren<SkinnedMeshRenderer>(), boneNames);
        limb.SetParent(_transform);

        return limb;
    }

    public Transform ProcessBonedObject(SkinnedMeshRenderer renderer, IReadOnlyList<string> boneNames){
        var newObject = new GameObject();
        newObject.layer =  LayerMask.NameToLayer("Equipment");
        var bonedObject = newObject.transform;
        //Created subObject
        

        var meshRenderer = bonedObject.gameObject.AddComponent<SkinnedMeshRenderer>();
        //Added the renderer

        for (var i = 0; i < boneNames.Count; i++)
        {
            //If there is ever a Key error then check the bone names of the item and see if it matches with the charcter bone names
            _boneTransforms[i] = _rootBoneDictionary[boneNames[i].GetHashCode()];
        }
        //Assembled our bone structure ^


        meshRenderer.bones = _boneTransforms;
        meshRenderer.sharedMesh = renderer.sharedMesh;
        meshRenderer.materials = renderer.sharedMaterials;
        //Assembled our renderer ^^

        return bonedObject;
        //Returned the bone object to the Limb
    }

    private void TraverseHierarchy(IEnumerable transform)
    {
        foreach (Transform child in transform)
        {
            _rootBoneDictionary.Add(child.name.GetHashCode(), child);
            //GetHashCode returns the hash code reference of the name
            TraverseHierarchy(child);
        }
    }

}
