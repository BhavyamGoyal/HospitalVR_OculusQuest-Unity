using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCFramework
{
    public static class Utils
    {
        
        public static void EventAsync(GameEvent HCEvent)
        {
            EventManager.Instance.TriggerEvent(HCEvent);
        }

        public static GameObject FindChildGameObject(this Transform parent, string childName, bool checkDisabled)
        {
            if (parent.name.Equals(childName)) return parent.gameObject;
            Transform[] allChildTransforms = parent.GetComponentsInChildren<Transform>(checkDisabled);

            foreach (Transform tr in allChildTransforms)
            {
                if (tr.name == childName) return tr.gameObject;
            }
            return null;
        }
    }
}