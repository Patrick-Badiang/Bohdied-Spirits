using UnityEngine;
    
    [CreateAssetMenu(fileName = "New Void Event", menuName = "ScriptableObject/GameEvents/Void")]
    public class VoidEvent : GameEvent<Void>
    {
        public void Raise () => Raise (new Void());



    }