
using UnityEngine;
using UnityEngine.UI;
using GFrame.Editor;

namespace Qarth.Extension
{
    public abstract class AbstractBind : MonoBehaviour, IBind
    {
        public BindType MarkType = BindType.DefaultUnityElement;

        public string Comment
        {
            get { return CustomComment; }
        }

        public Transform Transform
        {
            get { return transform; }
        }

        //public string CustomComponentName;

        [HideInInspector] public string ComponentGeneratePath;

        [HideInInspector] public string CustomComment;

        public BindType GetBindType()
        {
            return MarkType;
        }

        [HideInInspector] [SerializeField] private string mComponentName;

        public virtual string ComponentName
        {
            get
            {
                if (MarkType == BindType.DefaultUnityElement)
                {
                    if (mComponentName.IsNullOrEmpty())
                    {
                        mComponentName = GetDefaultComponentName();
                    }

                    return mComponentName;
                }

                return transform.name;
            }
            set { mComponentName = value; }
        }


        string GetDefaultComponentName()
        {
            if (GetComponent<ViewController>()) return GetComponent<ViewController>().GetType().FullName;


            if (GetComponent("SkeletonAnimation")) return "SkeletonAnimation";
            if (GetComponent<ScrollRect>()) return "UnityEngine.UI.ScrollRect";
            if (GetComponent<InputField>()) return "UnityEngine.UI.InputField";

            // text mesh pro supported
            if (GetComponent("TMP.TextMeshProUGUI")) return "TMP.TextMeshProUGUI";
            if (GetComponent("TMPro.TextMeshProUGUI")) return "TMPro.TextMeshProUGUI";
            if (GetComponent("TMPro.TextMeshPro")) return "TMPro.TextMeshPro";
            if (GetComponent("TMPro.TMP_InputField")) return "TMPro.TMP_InputField";

            //ugui expand
            if (GetComponent<SoundButton>()) return "SoundButton";
            if (GetComponent<PopButton>()) return "PopButton";
            if (GetComponent<GButton>()) return "GFrame.Editor.GButton";
            if (GetComponent<IUListView>()) return "IUListView";

            // ugui bind
            if (GetComponent<Dropdown>()) return "UnityEngine.UI.Dropdown";
            if (GetComponent<Button>()) return "UnityEngine.UI.Button";
            if (GetComponent<Text>()) return "UnityEngine.UI.Text";
            if (GetComponent<RawImage>()) return "UnityEngine.UI.RawImage";
            if (GetComponent<Toggle>()) return "UnityEngine.UI.Toggle";
            if (GetComponent<Slider>()) return "UnityEngine.UI.Slider";
            if (GetComponent<Scrollbar>()) return "UnityEngine.UI.Scrollbar";
            if (GetComponent<Image>()) return "UnityEngine.UI.Image";
            if (GetComponent<ToggleGroup>()) return "UnityEngine.UI.ToggleGroup";

            // other
            if (GetComponent<Rigidbody>()) return "Rigidbody";
            if (GetComponent<Rigidbody2D>()) return "Rigidbody2D";

            if (GetComponent<BoxCollider2D>()) return "BoxCollider2D";
            if (GetComponent<BoxCollider>()) return "BoxCollider";
            if (GetComponent<CircleCollider2D>()) return "CircleCollider2D";
            if (GetComponent<SphereCollider>()) return "SphereCollider";
            if (GetComponent<MeshCollider>()) return "MeshCollider";

            if (GetComponent<Collider>()) return "Collider";
            if (GetComponent<Collider2D>()) return "Collider2D";

            if (GetComponent<Animator>()) return "Animator";
            if (GetComponent<Canvas>()) return "Canvas";
            if (GetComponent<Camera>()) return "Camera";
            if (GetComponent<RectTransform>()) return "RectTransform";
            if (GetComponent<MeshRenderer>()) return "MeshRenderer";

            if (GetComponent<SpriteRenderer>()) return "SpriteRenderer";



            // NGUI 支持
            if (GetComponent("UIButton")) return "UIButton";
            if (GetComponent("UILabel")) return "UILabel";
            if (GetComponent("UISprite")) return "UISprite";
            if (GetComponent("UISlider")) return "UISlider";
            if (GetComponent("UITexture")) return "UITexture";

            return "Transform";
        }
    }
}