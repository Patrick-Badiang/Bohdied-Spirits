using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCombiner
{

    public readonly Dictionary<int, Transform> _RootBoneDictionary = new Dictionary<int, Transform>();

    private readonly Transform[] _boneTransforms = new Transform[67];

    private readonly Transform _transform;

    public BoneCombiner (GameObject rootObj){
        _transform = rootObj.transform;
        TraverseHierarchy(_transform);
    }

    public Transform AddLimb(GameObject bonedObj, List<string> boneNames){
        var limb = ProcessBonedObject(bonedObj.GetComponentInChildren<SkinnedMeshRenderer>(), boneNames);

        limb.SetParent(_transform);
        return limb;
    }

    public Transform ProcessBonedObject(SkinnedMeshRenderer renderer, List<string> boneNames){
        var bonedObject = new GameObject().transform;
        //Created subObject

        var meshRenderer = bonedObject.gameObject.AddComponent<SkinnedMeshRenderer>();
        //Added the renderer


        for (int i = 0; i < boneNames.Count; i++)
        {
            _boneTransforms[i] = _RootBoneDictionary[boneNames[i].GetHashCode()];
        }
        //Assembled our bone structure ^


        meshRenderer.bones = _boneTransforms;
        meshRenderer.sharedMesh = renderer.sharedMesh;
        meshRenderer.materials = renderer.sharedMaterials;

        //Assembled our renderer ^^

        return bonedObject;
        //Returned the bone object to the Limb
    }

    private void TraverseHierarchy(Transform _transform){
        foreach (Transform child in _transform)
        {
            _RootBoneDictionary.Add(child.name.GetHashCode(), child);
            //GetHashCode returns the hash code reference of the name
            TraverseHierarchy(child);
        }
    }

}
