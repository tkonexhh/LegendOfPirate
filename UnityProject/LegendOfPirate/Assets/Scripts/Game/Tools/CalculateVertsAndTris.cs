using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateVertsAndTris : MonoBehaviour {

    public float f_UpdateInterval = 0.5F;  //刷新间隔
    private float f_LastInterval;      //上一次刷新的时间间隔

    public static int verts;
    public static int tris;
    // Use this for initialization
    GUIStyle myStyle;

    void Start()
    {
        f_LastInterval = Time.realtimeSinceStartup;

        myStyle = new GUIStyle();
        myStyle.fontSize = 36;
        myStyle.normal.textColor = Color.white;
    }
    /// <summary>
    /// 得到场景中所有的GameObject
    /// </summary>
    void GetAllObjects()
    {
        verts = 0;
        tris = 0;
        GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject obj in ob)
        {
            GetAllVertsAndTris(obj);
        }
    }
    //得到三角面和顶点数
    void GetAllVertsAndTris(GameObject obj)
    {
        Component[] filters;
        filters = obj.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter f in filters)
        {
            tris += f.sharedMesh.triangles.Length / 3;
            verts += f.sharedMesh.vertexCount;
        }
    }
    void OnGUI()
    {
        string vertsdisplay = verts.ToString("#,##0 verts");
        //GUILayout.Label(vertsdisplay);
        GUI.Label(new Rect(200, Screen.height - 36, 200, 200), vertsdisplay, myStyle);

        string trisdisplay = tris.ToString("#,##0 tris");
        //GUILayout.Label(trisdisplay);
        GUI.Label(new Rect(200, Screen.height - 72, 200, 200), trisdisplay, myStyle);

    }
    // Update is called once per frame
    void Update()
    {

        if (Time.realtimeSinceStartup > f_LastInterval + f_UpdateInterval)
        {
            f_LastInterval = Time.realtimeSinceStartup;
            GetAllObjects();
        }
    }
}
