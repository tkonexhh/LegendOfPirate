
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


            // text mesh pro supported
            if (GetComponent("TextMeshProUGUI")) return "TextMeshProUGUI";
            if (GetComponent("TextMeshPro")) return "TextMeshPro";
            if (GetComponent("TMP_InputField")) return "TMP_InputField";

            //ugui expand
            if (GetComponent<SoundButton>()) return "SoundButton";
            if (GetComponent<PopButton>()) return "PopButton";

            if (GetComponent<IUListView>()) return "IUListView";

            // ugui bind
            if (GetComponent<ScrollRect>()) return "ScrollRect";
            if (GetComponent<InputField>()) return "InputField";
            if (GetComponent<Dropdown>()) return "Dropdown";
            if (GetComponent<Button>()) return "Button";
            if (GetComponent<Text>()) return "Text";
            if (GetComponent<RawImage>()) return "RawImage";
            if (GetComponent<Toggle>()) return "Toggle";
            if (GetComponent<Slider>()) return "Slider";
            if (GetComponent<Scrollbar>()) return "Scrollbar";
            if (GetComponent<Image>()) return "Image";
            if (GetComponent<ToggleGroup>()) return "ToggleGroup";

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



            // // NGUI 支持
            // if (GetComponent("UIButton")) return "UIButton";
            // if (GetComponent("UILabel")) return "UILabel";
            // if (GetComponent("UISprite")) return "UISprite";
            // if (GetComponent("UISlider")) return "UISlider";
            // if (GetComponent("UITexture")) return "UITexture";

            return "Transform";
        }
    }
}