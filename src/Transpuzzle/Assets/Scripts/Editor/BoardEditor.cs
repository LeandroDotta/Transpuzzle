using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Board))]
public class BoardEditor : Editor
{
    private Board board;

    private Piece pieceEmpty;
    private Piece pieceStart;
    private Piece pieceEnd;
    private Piece pieceStraight;
    private Piece pieceTurn;
    private Piece pieceIntersection;
    private Piece pieceBridge;
    private Piece pieceBridgeBase;

    List<Piece> allPieces;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        board = (Board)target;

		// Carrega as peças existentes
        if (allPieces == null)
        {
            allPieces = new List<Piece>();

            foreach (string guid in AssetDatabase.FindAssets("t:Piece"))
            {
				string path = AssetDatabase.GUIDToAssetPath(guid);
                allPieces.Add(AssetDatabase.LoadAssetAtPath<Piece>(path));
            }

            // pieceEmpty = allPieces.Where(p => p.type == PieceType.Empty).First();
            // pieceStart = allPieces.Where(p => p.type == PieceType.Start).First();
            // pieceEnd = allPieces.Where(p => p.type == PieceType.End).First();
            // pieceStraight = allPieces.Where(p => p.type == PieceType.Straight).First();
            // pieceTurn = allPieces.Where(p => p.type == PieceType.Turn).First();
            // pieceIntersection = allPieces.Where(p => p.type == PieceType.Intersection).First();
            // pieceBridge = allPieces.Where(p => p.type == PieceType.Bridge).First();
            // pieceBridgeBase = allPieces.Where(p => p.type == PieceType.BridgeBase).First();
        }

        if (GUILayout.Button("Render Board"))
        {
            ClearBoard();
            RenderBoard();
        }
    }

	// Insere os tiles do level no tabuleiro
    private void RenderBoard()
    {
        if (board.level == null || board.level.pieces == null || board.level.pieces.Length == 0)
            return;

        board.grid = new Tile[board.level.size.x * board.level.size.y];
        float boardScale = board.transform.localScale.x;

        for (int row = 0; row < board.level.size.x; row++)
        {
            for (int col = 0; col < board.level.size.y; col++)
            {
                int index = col * board.level.size.x + row;
                Piece piece = GetPiece(board.level.pieces[index]);

                Vector3 boardPosition = board.transform.position;
                Vector3 tilePosition = new Vector3(boardPosition.x + (row * boardScale), boardPosition.y, boardPosition.z - (col * boardScale));

                GameObject tileObj = PrefabUtility.InstantiatePrefab(piece.tilePrefab) as GameObject;
                tileObj.transform.SetParent(board.transform);
                tileObj.transform.position = tilePosition;
                
                tileObj.transform.localScale = Vector3.one;
                Tile tile = tileObj.GetComponent<Tile>();
                tile.gridIndex = index;
                
				board.grid[index] = tile;
            }
        }
    }

	// Limpa o tabuleiro
    private void ClearBoard()
    {
        while (board.transform.childCount > 0)
        {
            GameObject.DestroyImmediate(board.transform.GetChild(0).gameObject);
        }
    }

    private Piece GetPiece(PieceType type)
    {
        return allPieces.Where(p => p.type == type).First();
    }
}
