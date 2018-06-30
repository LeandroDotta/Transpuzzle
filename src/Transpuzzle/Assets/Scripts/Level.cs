using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Transpuzzle/Level", fileName = "New Level")]

public class Level : ScriptableObject
{
	public Vector2Int size;
	public PieceType[] pieces;
}
