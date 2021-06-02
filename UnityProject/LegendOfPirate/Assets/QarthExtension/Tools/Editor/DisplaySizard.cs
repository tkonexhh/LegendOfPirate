using GameWish.Game;
using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DisplaySizard : EditorWindow
{
    public bool TestModle = false;

    public static DisplaySizard DisplaySizardStatic;
    public static TestPanel TestPanelStatic;
    public static bool InitState = false;
    [MenuItem("Tools/DisplaySizard")]
    //public static void CreateWizard()
    //{
    //    DisplaySizardStatic = ScriptableWizard.DisplayWizard<DisplaySizard>("我是一个弹窗");
    //    OnInit();
    //}
    //private void OnWizardOtherButton()
    //{

    //}

    //private void OnWizardUpdate()
    //{
    //    if (InitState)
    //    {
    //        TestPanelStatic.PlatformHelper = TestModle;
    //    }
    //}   
    [MenuItem("Tools/DisplaySizard %q")]
    public static void OpenWindow()
    {
        //打开窗口的方法 注释掉的方法你可以设置打开的窗口的位置和大小
        //可以直接用没参数的重载方法,参数用来设置窗口名称，是否用标签窗口形式之类的
        EditorWindow.GetWindow<DisplaySizard>(false, "一个窗口");
        //EditorWindow.GetWindowWithRect<MyFirstWindow>(new Rect(0f, 0f, 500, 500));
        OnInit();
    }

    //private ItemID itemID = ItemID._3;
    //private ItemType itemType = ItemType.Consumable;
    private long numer = 0;
    private void OnGUI()
    {
        if (InitState)
        {
            TestPanelStatic.PlatformHelper = EditorGUILayout.Toggle("TestMode", TestPanelStatic.PlatformHelper);

            EditorGUILayout.Space();

            //...水平布局
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("增加物品", new[] { GUILayout.Width(100) });
            //itemID = (ItemID)EditorGUILayout.EnumPopup(itemID, new[] { GUILayout.Width(100) });
            //itemType = (ItemType)EditorGUILayout.EnumPopup(itemType, new[] { GUILayout.Width(100) });
            //numer = (long)EditorGUILayout.FloatField(numer, new[] { GUILayout.Width(100) });
            //bool isClick = GUILayout.Button("确定增加", new[] { GUILayout.Width(100) });
            //if (isClick && Application.isPlaying)
            //{
            //    UIMgr.S.ClosePanelAsUIID(UIID.MainMenuPanel);
            //    InventroyMgr.S.AddItemForID(itemType, itemID, numer);
            //    UIMgr.S.OpenPanel(UIID.MainMenuPanel);
            //    EditorUtility.DisplayDialog("提示", "增加成功","确认");
            //    //EditorWindow.ShowPopup()
            //}
            GUILayout.EndHorizontal();


           
            int gridId = 1;
            gridId = GUILayout.SelectionGrid(gridId, new[] { "1", "2", "3", "4", "5", "6" }, 4);
            //EditorGUILayout.LabelField("一个label");
            //EditorGUILayout.EnumFlagsField(ItemID._6, new[] { GUILayout.Height(200), GUILayout.Width(200) });
            //f = EditorGUILayout.FloatField("一个数字框", f);
            //col = EditorGUILayout.ColorField("一个颜色选择框", col);
        }
     
    }


    public static void OnInit()
    {
        TestPanelStatic = Resources.Load<TestPanel>("TestConfig");
        

        InitState = true;
    }

    //MyAsset untitledInstaller = Resources.Load<MyAsset>("GameData");//load这个资源文件
    //untitledInstaller.playData.money = 777;//可以修改这个字段
    //    Debug.LogError(untitledInstaller.author);
    //    Debug.LogError(untitledInstaller.playData.money);



    private void OnWizardCreate()
    {
    }
}


