using UnityEngine;
using UnityEngine.Serialization;

namespace DevCameraMod.Scripts
{
    public class TagDespawn : MonoBehaviour
    {
        [FormerlySerializedAs("rig")] public VRRig Rig;
        public void LateUpdate()
        {
            if (Rig == null) Destroy(gameObject);
        }
    }
}
