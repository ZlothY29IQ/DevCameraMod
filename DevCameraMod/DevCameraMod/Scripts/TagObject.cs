using System.Collections;
using Photon.Pun;
using Photon.Voice.PUN;
using UnityEngine;
using UnityEngine.UI;

namespace DevCameraMod.Scripts
{
    public class TagObject : MonoBehaviour
    {
        public  VRRig           rig;
        public  TagSpawner      tagSpawner;
        public  GameObject      tagObject;
        public  Text            tex;
        public  Canvas          canv;
        public  Vector3         scale;
        public  RawImage        speaker;
        public  RawImage        image;
        public  PhotonVoiceView view;
        public  CanvasScaler    scaler;
        private bool            lastEnable;

        public void LateUpdate()
        {
            if (tagObject == null)
                return;

            tagObject.transform.localScale = scale * rig.transform.localScale.y;
            tagObject.transform.position = rig.headMesh.transform.position +
                                           new Vector3(0, 0.364f, 0) * rig.transform.localScale.y;

            tagObject.transform.rotation = Plugin.Instance.camera.transform.rotation;
            tex.text                     = rig.playerText1.text;
            tex.color = rig.setMatIndex == 0
                                ? rig.materialsToChangeTo[0].color
                                : new Color(0.4588235f, 0.1098039f, 0);

            image.texture = rig.materialsToChangeTo[rig.setMatIndex].mainTexture;
            image.color   = rig != null ? rig.playerColor : Color.white;

            if (view != null) speaker.enabled = view.IsSpeaking || view.IsRecording;

            if (PhotonNetwork.InRoom && rig.isOfflineVRRig) canv.enabled                             = false;
            else if (!PhotonNetwork.InRoom & rig.isOfflineVRRig || !rig.isOfflineVRRig) canv.enabled = Plugin.Instance.nameTags;

            if (lastEnable != canv.enabled && canv.enabled)
                if (scaler != null)
                    StartCoroutine(CanvasFixer());

            lastEnable = canv.enabled;
        }

        public void StartDelay() => Invoke("StartImmediate", 2);

        public void StartImmediate()
        {
            tagObject = Instantiate(Plugin.Instance.nametagBase);
            scale     = tagObject.transform.localScale;
            tex       = tagObject.GetComponentInChildren<Text>();
            canv      = tagObject.GetComponentInChildren<Canvas>() ?? tagObject.GetComponentInParent<Canvas>();
            speaker   = tagObject.transform.Find("RawImageSpeaker").GetComponent<RawImage>();
            image     = tagObject.transform.Find("RawImage").GetComponent<RawImage>();
            rig.TryGetComponent(out view);
            tagObject.TryGetComponent(out scaler);
            speaker.enabled                          = false;
            tagObject.AddComponent<TagDespawn>().Rig = rig;
        }

        private IEnumerator CanvasFixer()
        {
            scaler.enabled = false;

            yield return new WaitForEndOfFrame();

            scaler.enabled = true;
        }
    }
}