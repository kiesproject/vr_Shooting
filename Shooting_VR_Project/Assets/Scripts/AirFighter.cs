using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif


public  class AirFighter : MonoBehaviour
{
    //プロパティ用のフラグ
    [Flags]
    public enum Property
    {
        isInvulnerable  = 1<<0,  // 1のとき不死身
        isIgnore        = 1<<1,  // 1のとき飛行命令を受け付けない
        isFring         = 1<<2,  // 1のとき飛んでいるのかどうか
    }

    //プロパティ
    [HideInInspector]
    public Property property = 0;

    //--- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---

    //飛行ルート
    [HideInInspector]
    public List<Vector3> Route_List;

    //飛行スピード
    [SerializeField]
    protected float airFighter_speed = 1.0f;

    //目標座標
    protected List<Vector3> target_vector3s;

    //--- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---

    //戦闘機のHP
    protected float hp = 10;

    //--- エディター用のフィールド --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --
    [HideInInspector] protected bool onGizmo = false;
    [HideInInspector] protected bool onHandle = false;
    
    protected virtual void Start()
    {
        

    }

    protected virtual void Update()
    {
        Down_Chack(); //撃墜判定


    }

    protected virtual void FixedUpdate()
    {

    }

    //死亡したかどうか
    protected void Down_Chack()
    {
       if (this.hp <= 0)
        {
            hp = 0;
            Shooting_down();
        }
    }

    //死亡
    protected virtual void Shooting_down()
    {
        //HPがゼロになった時の処理
    }

    //ダメージを与える(敵からの攻撃)
    public void Damage(float damage)
    {
        if ((property & Property.isInvulnerable) == Property.isInvulnerable) return;

        //HPからダメージ分減らす
        hp -= damage;

        //撃墜判定
        Down_Chack();
    }

    //戦闘機を飛ばす
    public void Launch_AriFighter()
    {
        
        int RouteCount = Route_List.Count; //データ数
        //ルートのデータのチェック
        if (RouteCount <= 0) return;

        if ((property & Property.isFring) != Property.isFring) //飛ぶ命令を受けているかどうか
        {
            Debug.Log("ルート");
            property |= Property.isFring;
            First_Set_TargetList();
            //Fly_AriFighter();
            StartCoroutine(Fly_AriFighter(Conversion_RouteList(target_vector3s))); //コルーチン

            return;
        }
    }

    protected virtual IEnumerator Fly_AriFighter2()
    {
        yield return null;
    }

    //スプライン曲線
    private Vector3 CalcSpline(Vector3 p1, Vector3 p2, Vector3 v1, Vector3 v2, float t)
    {
        Matrix4x4 T = new Matrix4x4();
        Matrix4x4 H = new Matrix4x4();
        Matrix4x4 G = new Matrix4x4();

        T.m00 = t * t * t; T.m01 = t * t; T.m02 = t; T.m03 = 1;

        H.m00 = 2; H.m01 = -2; H.m02 = 1; H.m03 = 1;
        H.m10 = -3; H.m11 = 3; H.m12 = -2; H.m13 = -1;
        H.m20 = 0; H.m21 = 0; H.m22 = 1; H.m23 = 0;
        H.m30 = 1; H.m31 = 0; H.m32 = 0; H.m33 = 0;

        G.m00 = p1.x; G.m01 = p1.y; G.m02 = p1.z; G.m03 = 1;
        G.m10 = p2.x; G.m11 = p2.y; G.m12 = p2.z; G.m13 = 1;
        G.m20 = v1.x; G.m21 = v1.y; G.m22 = v1.z; G.m23 = 1;
        G.m30 = v2.x; G.m31 = v2.y; G.m32 = v2.z; G.m33 = 1;

        return (T * H * G).GetRow(0);
    }

    //飛行メソッド
    protected virtual IEnumerator Fly_AriFighter(List<Vector3> con_RoutList)
    {
        bool three_v3_Apply = true; //trueの時、firstv3の中身を更新させるフラグ

        int index = 1; //アクセスする要素の先頭
        float t = 0.0f;
        Vector3[] firstv3 = new Vector3[3]; //ベジェ曲線に使う変数
        List<Vector3> target_vector3s = con_RoutList;

        while (true)
        {

            if (three_v3_Apply)
            {
                firstv3[0] = transform.position;
                for (int i = 0; i < firstv3.Length - 1 ; i++) //リスト先頭３つを代入
                {
                    firstv3[i + index] = target_vector3s[i];
                }
                three_v3_Apply = false;
                t = 0.0f;
            }
            //--- --- --- --- --- --- --- --- --- -- --- --- -- --- --- -- --- --- --- --- --- --- --- --- -- --- --- -- --- --- --    

            if (t > 0.5 && target_vector3s.Count >= 3)
            {
                Debug.Log("hoge");
                target_vector3s.RemoveAt(0); //0番目を除外する。
                three_v3_Apply = true;
            }


            t += Time.deltaTime * airFighter_speed;
            var point = GetMovePoint(firstv3[0], firstv3[1], firstv3[2], t); //ベジェ曲線
            Debug.Log(firstv3[0]+"|"+firstv3[1]+"|"+ firstv3[2]);
            transform.position = point; //移動
            transform.LookAt(GetMovePoint(firstv3[0], firstv3[1], firstv3[2], t + 0.01f));



            if (t > 1) break; //ループの脱出

            yield return null;
        }
    }


    //飛び始める時にリストを用意する
    protected void First_Set_TargetList()
    {
        target_vector3s = new List<Vector3>(); //生成
        foreach(Vector3 v in Route_List)
            //設定されている座標をすべてリストにいれる。
            target_vector3s.Add(v);
    }

    //ルートリストのローカル座標をワールド座標に置き換える(動き始める前に実行すること)
    protected List<Vector3> Conversion_RouteList(List<Vector3> rl)
    {
        var outList = new List<Vector3>();

        foreach(Vector3 v in rl)
        {
            outList.Add(v + transform.position);
        }
        return outList;
    }

    //ベジェ曲線を用いた飛行座標取得
    protected Vector3 GetMovePoint(Vector3 p0, Vector3 p1, Vector3 p2,float t)
    {
        var a = Vector3.Lerp(p0, p1, t);
        var b = Vector3.Lerp(p1, p2, t);

        return Vector3.Lerp(a, b, t);
        
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(AirFighter))]
    //[CustomEditor(typeof(PlayerBase))]
    public class AirFighter_Inspector : Editor
    {
        ReorderableList reorderableList;
        Vector3 snap;

        void OnEnable()
        {
            var prop = serializedObject.FindProperty("Route_List");

            reorderableList = new ReorderableList(serializedObject, prop);

            reorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
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
            //var size = 0.1f;
            var size = HandleUtility.GetHandleSize(position);

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
            var component = target as AirFighter;
            var transform = component.transform;
            
            transform.position = Handles.PositionHandle(transform.position, transform.rotation);

            if (!component.onHandle) return; //ハンドルがオフになっている
            if (EditorApplication.isPlaying) return; //実行されている
            for (int i=0; i < component.Route_List.Count; i++)
            {
                component.Route_List[i] = PositionHandle(component.Route_List[i] + transform.position) - transform.position;
            }

        }

        bool foldout = false;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var component = target as AirFighter;

            component.onGizmo = EditorGUILayout.Toggle("ギズモを表示する", component.onGizmo);
            component.onHandle = EditorGUILayout.Toggle("ハンドルを表示する", component.onHandle);

            if (foldout = EditorGUILayout.Foldout(foldout, "ルートの設定"))
            {
                serializedObject.Update();
                reorderableList.DoLayoutList();
                serializedObject.ApplyModifiedProperties();
            }
        }

    }

    //ギズモ
    public class EditorGizmo_AirFighter
    {

        [DrawGizmo(GizmoType.Active | GizmoType.NonSelected)]
        static void DrawExampleGizmos(AirFighter airFighter, GizmoType gizmoType)
        {
            if (airFighter.Route_List == null) return; //ルートが設定されていない
            if (!airFighter.onGizmo) return; //ギズモがオフになっている

            var vertexes = airFighter.Route_List;
            var basePoss = airFighter.transform.position;

            Gizmos.color = new Color32(200, 200, 0, 210);
            Gizmos.DrawWireSphere(airFighter.transform.position, 0.1f);
            if (!EditorApplication.isPlaying)
                Gizmos.DrawLine(basePoss, vertexes[0]+basePoss);
            //--- --- --- --- --- --- --- --- --- --- ---- --- ---- --- --- --- ---
            Gizmos.color = new Color32(145, 139, 244, 210);
            //GizmoType.Active の時は色を変える
            if ((gizmoType & GizmoType.Active) == GizmoType.Active)
                Gizmos.color = new Color32(45, 30, 244, 255);

            //ポイント

            foreach (Vector3 v3 in vertexes)
            {
                if (!EditorApplication.isPlaying)
                { Gizmos.DrawWireSphere(v3 + basePoss, 0.1f); }
                else
                { Gizmos.DrawWireSphere(v3, 0.1f); }
            }
            
            //線を引く
            for (int i = 0; i < vertexes.Count - 1; i++)
            {
                if (!EditorApplication.isPlaying)
                { Gizmos.DrawLine(vertexes[i] + basePoss, vertexes[i + 1] + basePoss); }
                else
                { Gizmos.DrawLine(vertexes[i], vertexes[i + 1]); }
            }

        }

    }

#endif



}

