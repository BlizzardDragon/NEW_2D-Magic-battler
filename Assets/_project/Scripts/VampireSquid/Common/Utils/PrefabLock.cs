using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace VampireSquid.Common.Utils
{
    [ExecuteInEditMode]
    public class PrefabLock : MonoBehaviour
    {
#if UNITY_EDITOR
        // Update is called once per frame
        void Update()
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (!PrefabUtility.IsPartOfAnyPrefab(gameObject))
            {
                Debug.LogWarning("Must Be On Prefab!");
                this.enabled = false;
                return;
            }

            List<ObjectOverride> overrides = PrefabUtility.GetObjectOverrides(gameObject);

            foreach (ObjectOverride objectOverride in overrides)
            {
                if (objectOverride.instanceObject == this)
                {
                    continue;
                }
            
                objectOverride.Revert(InteractionMode.AutomatedAction);
            }

            List<RemovedComponent> removedComponents = PrefabUtility.GetRemovedComponents(gameObject);

            foreach (RemovedComponent component in removedComponents)
            {
                component.Revert(InteractionMode.AutomatedAction);
            }
        }
    
#endif
    }
}