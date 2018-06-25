using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PieceType
{
	Empty, Start, End, Straight, Turn, Intersection, Bridge, BridgeBase 
}

[CreateAssetMenu(menuName = "Transpuzzle/Piece", fileName = "New Piece")]
public class Piece : ScriptableObject 
{
	public PieceType type;

	public bool connection1;
	public bool connection2;
	public bool connection3;
	public bool connection4;

	public Sprite sprite;
}
