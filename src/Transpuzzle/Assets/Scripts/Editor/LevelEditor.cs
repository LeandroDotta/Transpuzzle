using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditorWindow : EditorWindow
{
    private Level level;

    [MenuItem("Transpuzzle/Leve Editor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelEditorWindow), false, "Level Editor");
    }

    void OnSelectionChange()
    {
        if (Selection.activeObject != null)
        {
            object lvlObj = Selection.activeObject;
            if (lvlObj.GetType() == typeof(Level))
            {
                Focus();

                level = (Level)lvlObj;
            }
        }
    }

    // private void OnGUI()
    // {
    //     if (level == null)
    //     {
    //         EditorGUILayout.LabelField("No Level Selected", EditorStyles.boldLabel);
    //     }
    //     else
    //     {
    //         EditorGUILayout.LabelField("Editing " + level.name, EditorStyles.boldLabel);

    //         EditorGUI.BeginChangeCheck();

    //         level.size = EditorGUILayout.Vector2IntField("Size", level.size);

    //         if (EditorGUI.EndChangeCheck())
    //         {
    //             // Resize Grid
    //             level.pieces = ResizeGrid(level.size);
    //         }

    //         if (level.pieces != null && level.pieces.Length > 0)
    //         {
    //             for (int row = 0; row < level.size.x; row++)
    //             {
    //                 EditorGUILayout.BeginHorizontal();

    //                 for (int col = 0; col < level.size.y; col++)
    //                 {
    //                     int index = col * level.size.x + row;
    //                     level.pieces[index] = (PieceType)EditorGUILayout.EnumPopup(level.pieces[index]);
    //                 }

    //                 EditorGUILayout.EndHorizontal();
    //             }

    //             if (GUILayout.Button("Clear Tiles"))
    //             {
    //                 for (int row = 0; row < level.size.x; row++)
    //                 {
    //                     for (int col = 0; col < level.size.y; col++)
    //                     {
    //                         int index = col * level.size.x + row;
    //                         level.pieces[index] = PieceType.Empty;
    //                     }
    //                 }
    //             }
    //         }

    //         EditorGUILayout.Space();

    //         if (GUILayout.Button("Random Tiles"))
    //         {
    //             level.pieces = GenerateGrid();
    //         }
    //     }

    //     // if (Selection.activeObject != null)
    //     // {
    //     //     Debug.Log(Selection.activeObject.name);
    //     // 	Focus();

    //     //     System.Type type = Selection.activeObject.GetType();

    //     //     if (type == typeof(Level))
    //     //     {
    //     // 		level = (Level)Selection.activeObject;

    //     //         if (level.pieces != null)
    //     //         {
    //     //             for (int x = 0; x < level.pieces.GetLength(0); x++)
    //     //             {
    //     //                 EditorGUILayout.BeginHorizontal();

    //     //                 for (int y = 0; y < level.pieces.GetLength(1); y++)
    //     //                 {
    //     //                     level.pieces[x, y] = (PieceType)EditorGUILayout.EnumPopup(level.pieces[x, y]);
    //     //                 }

    //     //                 EditorGUILayout.EndHorizontal();
    //     //             }
    //     //         }

    //     //         EditorGUILayout.Space();

    //     //         if (GUILayout.Button("Random Tiles"))
    //     //         {
    //     //             level.pieces = GenerateGrid();
    //     //         }
    //     //     }
    //     // }
    // }

    private PieceType[] GenerateGrid()
    {
        if (level.size.x == 0 || level.size.y == 0)
            return null;

        PieceType[] g = new PieceType[level.size.x * level.size.y];

        for (int row = 0; row < level.size.x; row++)
        {
            for (int col = 0; col < level.size.y; col++)
            {
                PieceType[] items = (PieceType[])System.Enum.GetValues(typeof(PieceType));
                g[col * level.size.x + row] = items[Random.Range(0, items.Length)];
            }
        }

        return g;
    }

    private PieceType[] ResizeGrid(Vector2Int newSize)
    {
        PieceType[] result = new PieceType[newSize.x * newSize.y];

        for (int row = 0; row < newSize.x; row++)
        {
            for (int col = 0; col < newSize.y; col++)
            {
                int index = col * newSize.x + row;

                if(level.pieces != null && index < level.pieces.Length)
                    result[index] = level.pieces[index];
                else
                    result[index] = PieceType.Empty;
            }
        }

        return result;
    }
}
