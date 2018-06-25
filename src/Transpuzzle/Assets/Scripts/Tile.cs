using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour 
{
	public Piece piece;

	private void Start()
	{
		// spriteRenderer = GetComponent<SpriteRenderer>();
		// spriteRenderer.sprite = piece.sprite;	
	}

	public void SetPiece(Piece piece)
	{
		this.piece = piece;
		GetComponent<SpriteRenderer>().sprite = piece.sprite;
	}
}
