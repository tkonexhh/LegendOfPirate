using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.Modules.AttachmentTools;
using Spine.Unity;
using Spine;
using Qarth;

namespace GameWish.Game
{
    public class SpineGraphicAttachmentEquip : MonoBehaviour
    {
        #region Inspector
        [SpineSkin]
        public string baseSkinName = "base";
        public Material sourceMaterial; // This will be used as the basis for shader and material property settings.

        [System.Serializable]
        public class EquipData
        {
            public string animationName;
            //public Sprite newSprite;
            [SpineSlot] public string slot;
            [SpineAttachment(slotField: "slot", skinField: "baseSkinName")] public string key = "m_food_1";
        }

        public List<EquipData> equipDataList = new List<EquipData>();

        [Header("Runtime Repack Required!!")]
        public bool repack = true;

        [Header("Do not assign")]
        public Texture2D runtimeAtlas;
        public Material runtimeMaterial;
        #endregion

        private Skin customSkin;
        private Skin baseSkin;
        private SkeletonGraphic skeletonGraphic;
        private Skeleton skeleton;

        private void Start()
        {
            skeletonGraphic = GetComponent<SkeletonGraphic>();
            skeleton = skeletonGraphic.Skeleton;

            baseSkin = skeleton.Data.FindSkin(baseSkinName);
            customSkin = customSkin ?? new Skin("custom skin");
        }

        private EquipData GetEquipData(string animationName)
        {
            foreach (EquipData d in equipDataList)
            {
                if (d.animationName == animationName)
                {
                    return d;
                }
            }

            return null;
        }

        public void EquipNewSprite(string animationName, Sprite sprite)
        {
            EquipData data = GetEquipData(animationName);
            if (data == null)
            {
                Log.e("Error, equip data is null");
                return;
            }
            //sprite = SpriteLoader.S.GetSpriteByName("bing_1");
            //sprite = Resources.Load("bing_1", typeof(Sprite)) as Sprite;

            int slotIndex = skeleton.FindSlotIndex(data.slot);
            Attachment baseAttachment = baseSkin.GetAttachment(slotIndex, data.key); // STEP 1.1
            Attachment newAttachment = baseAttachment.GetRemappedClone(sprite, sourceMaterial); // STEP 1.2 - 1.3
            if (newAttachment != null) customSkin.SetAttachment(slotIndex, data.key, newAttachment); // STEP 1.4

            if (repack)
            {
                var repackedSkin = new Skin("repacked skin");
                repackedSkin.Append(skeleton.Data.DefaultSkin);
                repackedSkin.Append(customSkin);
                repackedSkin = repackedSkin.GetRepackedSkin("repacked skin", sourceMaterial, out runtimeMaterial, out runtimeAtlas);
                skeleton.SetSkin(repackedSkin);
            }
            else
            {
                skeleton.SetSkin(customSkin);
            }

            skeleton.SetToSetupPose();
            skeletonGraphic.Update(0);
            skeletonGraphic.OverrideTexture = runtimeAtlas;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("key z pressed");
                EquipNewSprite("", SpriteLoader.S.GetSpriteByName("bing_1"));
            }
        }
    }
}