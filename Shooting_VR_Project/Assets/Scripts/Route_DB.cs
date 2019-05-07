using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif


[CreateAssetMenu(fileName = "Route", menuName = "Creat_RouteData")]
public class Route_DB : ScriptableObject
{
    [SerializeField]
    public List<Vector3> routeList;

    public List<Vector3> GetList()
    {
        return routeList;
    }

    
    [CustomEditor(typeof(Route_DB))]
    public class RouteDB_Inspector : Editor
    {
        ReorderableList reorderableList;
        Vector3 snap;

        void OnEnable()
        {
            var prop = serializedObject.FindProperty("routeList");

            reorderableList = new ReorderableList(serializedObject, prop);

            reorderableList.drawElementCallback = (rect, index, isActive, isFocused) => {
                var element = prop.GetArrayElementAtIndex(index);
                rect.height -= 4;
                rect.y += 2;
                EditorGUI.PropertyField(rect, element);
                
            };

            //SnapSettings の値を取得する
            var snapX = EditorPrefs.GetFloat("MoveSnapX", 1f);
            var snapY = EditorPrefs.GetFloat("MoveSnapY", 1f);
            var snapZ = EditorPrefs.GetFloat("MoveSnapZ", 1f);
            snap = new Vector3(snapX, snapY, snapZ);

        }

        //自作したハンドル
        Vector3 PositionHandle(Vector3 position)
        {
            //var position = transform.position;
            var size = 0.1f;
            //var size = HandleUtility.GetHandleSize(position);

            //X 軸
            Handles.color = Handles.xAxisColor;
            position = Handles.Slider(position, Vector3.right, size, Handles.ArrowCap, snap.x);

            //Y 軸
            Handles.color = Handles.yAxisColor;
            position = Handles.Slider(position, Vector3.up, size, Handles.ArrowCap, snap.y);

            //Z 軸
            Handles.color = Handles.zAxisColor;
            position = Handles.Slider(position, Vector3.forward, size, Handles.ArrowCap, snap.z);

            return position;
        }

        protected virtual void OnSceneGUI()
        {
            Tools.current = Tool.None;
            var component = target as Route_DB;
            // var transform = component.transform;

            //transform.position = PositionHandle(transform.position);

            //transform.position = Handles.PositionHandle(transform.position, transform.rotation);
            Debug.Log("route");
            var vertexes = component.GetList();
            //Handles.DrawAAPolyLine(vertexes.ToArray());

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            reorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
        
    }
    

}
