using Spine;
using Spine.Unity;
using Spine.Unity.Modules.AttachmentTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class EquipSystem : MonoBehaviour, IHasSkeletonDataAsset
    {
        // Implementing IHasSkeletonDataAsset allows Spine attribute drawers to automatically detect this component as a skeleton data source.
        public SkeletonDataAsset skeletonDataAsset;
        SkeletonDataAsset IHasSkeletonDataAsset.SkeletonDataAsset { get { return this.skeletonDataAsset; } }

        public Material sourceMaterial;
        public bool applyPMA = true;
        public List<EquipHook> equippables = new List<EquipHook>();

        public EquipVisualComponent target;
        public Dictionary<EquipHook, Attachment> cachedAttachments = new Dictionary<EquipHook, Attachment>();

        [System.Serializable]
        public class EquipHook
        {
            [SpineSlot]
            public string slot;
            [SpineSkin]
            public string templateSkin;
            [SpineAttachment(skinField: "templateSkin")]
            public string templateAttachment;
            public Sprite sprite;
        }

        public enum EquipType
        {
            Gun,
            Goggles
        }

        public void Equip(int index)
        {
            EquipHook hook = equippables[index];

            var skeletonData = skeletonDataAsset.GetSkeletonData(true);
            int slotIndex = skeletonData.FindSlotIndex(hook.slot);
            var attachment = GenerateAttachmentFromEquipAsset(hook, slotIndex, hook.templateSkin, hook.templateAttachment);
            target.Equip(slotIndex, hook.templateAttachment, attachment);

            Done();
        }

        Attachment GenerateAttachmentFromEquipAsset(EquipHook asset, int slotIndex, string templateSkinName, string templateAttachmentName)
        {
            Attachment attachment;
            cachedAttachments.TryGetValue(asset, out attachment);

            if (attachment == null)
            {
                var skeletonData = skeletonDataAsset.GetSkeletonData(true);
                var templateSkin = skeletonData.FindSkin(templateSkinName);
                Attachment templateAttachment = templateSkin.GetAttachment(slotIndex, templateAttachmentName);
                attachment = templateAttachment.GetRemappedClone(asset.sprite, sourceMaterial, premultiplyAlpha: this.applyPMA);

                cachedAttachments.Add(asset, attachment); // Cache this value for next time this asset is used.
            }

            return attachment;
        }

        public void Done()
        {
            target.OptimizeSkin();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Equip(0);
            }
        }
    }

}