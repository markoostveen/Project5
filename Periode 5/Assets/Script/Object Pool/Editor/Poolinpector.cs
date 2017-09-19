using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(ObjectPool))]
public class Poolinpector : Editor
{

    private List<string> m_TagList;

    private ObjectPool m_target;
    private List<Poolobj> m_ObjList;

    protected void OnEnable()
    {
        m_target = (ObjectPool)target;
        m_ObjList = new List<Poolobj>();
        for (int i = 0; i < m_target.ObjectList.Length; i++)
            m_ObjList.Add(m_target.ObjectList[i]);
        m_TagList = new List<string>();
        for (int i = 0; i < m_target.m_Tags.Length; i++)
            m_TagList.Add(m_target.m_Tags[i]);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (Application.isPlaying)
            GUI.enabled = false;

            ObjectPool();
            TagForSpawners();


        EditorUtility.SetDirty(m_target);
        Undo.RecordObject(m_target, "Undo");

        if (Application.isPlaying)
            GUI.enabled = true;

        serializedObject.ApplyModifiedProperties();
    }

    private void TagForSpawners()
    {
        GUILayout.Label("Tags associated with Spawners");

        for (int i = 0; i < m_TagList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            m_TagList[i] = EditorGUILayout.TextField(m_TagList[i]);
            if (GUILayout.Button("Remove"))
                m_TagList.Remove(m_TagList[i]);

            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Tag"))
            m_TagList.Add("");
        m_target.m_Tags = m_TagList.ToArray();
    }

    protected virtual void ObjectPool()
    {
        GUILayout.Label("Default objects");
        if (m_ObjList.Count > 0)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Prefab");
            GUILayout.Label("Amount");
            EditorGUILayout.EndHorizontal();
        }

        for (int i = 0; i < m_ObjList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            m_ObjList[i].m_Prefab = (GameObject)EditorGUILayout.ObjectField(m_ObjList[i].m_Prefab, typeof(GameObject), true);
            m_ObjList[i].m_Amount = EditorGUILayout.IntField(m_ObjList[i].m_Amount);
            if (GUILayout.Button("Remove"))
                m_ObjList.Remove(m_ObjList[i]);

            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Object"))
            m_ObjList.Add(new Poolobj());

        m_target.ObjectList = m_ObjList.ToArray();
    }
}
