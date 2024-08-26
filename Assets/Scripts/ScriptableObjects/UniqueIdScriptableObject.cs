using UnityEngine;

namespace SurvivalGame.ScriptableObjects
{
    public abstract class UniqueIdScriptableObject : ScriptableObject
    {
        public int UniqueId { get; private set; }

        public void AssignUniqueId(int uniqueId)
        {
            UniqueId = uniqueId;
        }
    }
}
