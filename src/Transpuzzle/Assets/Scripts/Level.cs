using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Transpuzzle/Level", fileName = "New Level")]
public class Level : ScriptableObject
{
	public Vector2 size;

	public PieceType[,] pieces;
}
