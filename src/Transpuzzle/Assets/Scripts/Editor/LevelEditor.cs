using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    private Level level;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        level = (Level)target;

        EditorGUILayout.LabelField("Grid");

        if (level.pieces != null)
        {
            for (int x = 0; x < level.pieces.GetLength(0); x++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int y = 0; y < level.pieces.GetLength(1); y++)
                {
                    level.pieces[x, y] = (PieceType)EditorGUILayout.EnumPopup(level.pieces[x, y]);
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Random Tiles"))
        {
            level.pieces = GenerateGrid();
        }
    }

    private PieceType[,] GenerateGrid()
    {
        PieceType[,] g = new PieceType[5, 5];

        for (int i = 0; i < g.GetLength(0); i++)
        {
            for (int j = 0; j < g.GetLength(1); j++)
            {
                PieceType[] items = (PieceType[])System.Enum.GetValues(typeof(PieceType));
                g[j, i] = items[Random.Range(0, items.Length)];
            }
        }

        return g;
    }
}

public class LevelEditorWindow : EditorWindow
{
    [MenuItem("Transpuzzle/Leve Editor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelEditorWindow), false, "Level Editor");
    }

    private void OnGUI()
    {
        Level level;

        if (Selection.activeObject != null)
        {
            Debug.Log(Selection.activeObject.name);
			Focus();

            System.Type type = Selection.activeObject.GetType();

            if (type == typeof(Level))
            {
				level = (Level)Selection.activeObject;

                if (level.pieces != null)
                {
                    for (int x = 0; x < level.pieces.GetLength(0); x++)
                    {
                        EditorGUILayout.BeginHorizontal();

                        for (int y = 0; y < level.pieces.GetLength(1); y++)
                        {
                            level.pieces[x, y] = (PieceType)EditorGUILayout.EnumPopup(level.pieces[x, y]);
                        }

                        EditorGUILayout.EndHorizontal();
                    }
                }

                EditorGUILayout.Space();

                if (GUILayout.Button("Random Tiles"))
                {
                    level.pieces = GenerateGrid();
                }
            }
        }
    }

	private PieceType[,] GenerateGrid()
    {
        PieceType[,] g = new PieceType[5, 5];

        for (int i = 0; i < g.GetLength(0); i++)
        {
            for (int j = 0; j < g.GetLength(1); j++)
            {
                PieceType[] items = (PieceType[])System.Enum.GetValues(typeof(PieceType));
                g[j, i] = items[Random.Range(0, items.Length)];
            }
        }

        return g;
    }
}
