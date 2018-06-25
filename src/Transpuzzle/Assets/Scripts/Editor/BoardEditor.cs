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

            pieceEmpty = allPieces.Where(p => p.type == PieceType.Empty).First();
            pieceStart = allPieces.Where(p => p.type == PieceType.Start).First();
            pieceEnd = allPieces.Where(p => p.type == PieceType.End).First();
            pieceStraight = allPieces.Where(p => p.type == PieceType.Straight).First();
            pieceTurn = allPieces.Where(p => p.type == PieceType.Turn).First();
            pieceIntersection = allPieces.Where(p => p.type == PieceType.Intersection).First();
            pieceBridge = allPieces.Where(p => p.type == PieceType.Bridge).First();
            pieceBridgeBase = allPieces.Where(p => p.type == PieceType.BridgeBase).First();
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

        board.grid = new PieceType[board.level.pieces.GetLength(0), board.level.pieces.GetLength(1)];

        for (int x = 0; x < board.level.pieces.GetLength(0); x++)
        {
            for (int y = 0; y < board.level.pieces.GetLength(1); y++)
            {
                PieceType item = board.level.pieces[x, y];

                Vector2 boardPosition = board.transform.position;
                Vector2 tilePosition = new Vector2(boardPosition.x + x, boardPosition.y - y);

                GameObject tile = PrefabUtility.InstantiatePrefab(board.tilePrefab) as GameObject;
                tile.transform.position = tilePosition;
                tile.transform.SetParent(board.transform);
                SetPiece(item, ref tile);

				board.grid[x, y] = item;
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

	// Atribui uma peça a um tile
    private void SetPiece(PieceType type, ref GameObject tile)
    {
        Tile t = tile.GetComponent<Tile>();

        switch (type)
        {
            case PieceType.Empty:
                t.SetPiece(pieceEmpty);
                break;
            case PieceType.Start:
                t.SetPiece(pieceStart);
                break;
            case PieceType.End:
                t.SetPiece(pieceEnd);
                break;
            case PieceType.Straight:
                t.SetPiece(pieceStraight);
                break;
            case PieceType.Turn:
                t.SetPiece(pieceTurn);
                break;
            case PieceType.Intersection:
                t.SetPiece(pieceIntersection);
                break;
            case PieceType.Bridge:
                t.SetPiece(pieceBridge);
                break;
            case PieceType.BridgeBase:
                t.SetPiece(pieceBridgeBase);
                break;
        }
    }
}
