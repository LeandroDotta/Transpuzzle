using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour 
{
	private Tile tile;

	private void OnTriggerEnter(Collider other) 
	{
		if(other.CompareTag("Tile"))
		{
			tile = other.GetComponent<Tile>();
		}
	}
}
