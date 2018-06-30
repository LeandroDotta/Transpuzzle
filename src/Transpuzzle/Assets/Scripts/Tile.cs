using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Piece piece;
    public Direction orientation;
	public int gridIndex;

    private void Start()
    {
    }

    private void OnMouseDown() 
    {
        RotateRight();
    }

    public void SetPiece(Piece piece)
    {
        this.piece = piece;
        GetComponent<SpriteRenderer>().sprite = piece.sprite;
    }

    public void SetOrientation(Direction orientation)
    {
        this.orientation = orientation;

        Vector3 rotation = transform.rotation.eulerAngles;
        

        switch(orientation)
        {
            case Direction.Up:
                rotation.z = 0;
                break;

            case Direction.Right:
                rotation.z = -90;
                break;

            case Direction.Down:
                rotation.z = 180;
                break;

            case Direction.Left:
                rotation.z = 90;
                break;
        }

        transform.rotation = Quaternion.Euler(rotation);
    }    

    private void RotateRight()
    {        
        Direction[] dirs = (Direction[])System.Enum.GetValues(typeof(Direction));

        int i = System.Array.IndexOf(dirs, orientation) + 1;

        if(i >= dirs.Length)
            i = 0;

        Debug.Log(dirs[i]);
        SetOrientation(dirs[i]);
    }

    private void OnDrawGizmos()
    {
        if (piece != null)
        {
            Bounds bounds = GetComponent<SpriteRenderer>().bounds;

            Vector2 size = new Vector2(.1f, .1f);
            Vector2 up = new Vector2(bounds.center.x, bounds.max.y - size.y / 2);
            Vector2 left = new Vector2(bounds.max.x - size.x / 2, bounds.center.y);
            Vector2 down = new Vector2(bounds.center.x, bounds.min.y + size.y / 2);
            Vector2 right = new Vector2(bounds.min.x + size.x / 2, bounds.center.y);


            Color red = Color.red;
            Color green = Color.green;

            Gizmos.color = red;

            Gizmos.color = piece.connection1 ? green : red;
            Gizmos.DrawCube(up, size);

            Gizmos.color = piece.connection2 ? green : red;
            Gizmos.DrawCube(left, size);

            Gizmos.color = piece.connection3 ? green : red;
            Gizmos.DrawCube(down, size);

            Gizmos.color = piece.connection4 ? green : red;
            Gizmos.DrawCube(right, size);
        }
    }

	public bool ConnectionUp
    {
        get
        {
			switch(orientation)
			{
				case Direction.Up:
					return piece.connection1;

				case Direction.Left:
					return piece.connection2;

				case Direction.Down:
					return piece.connection3;

				case Direction.Right:
					return piece.connection4;
			}

			return false;
        }
    }
	public bool ConnectionLeft
    {
        get
        {
			switch(orientation)
			{
				case Direction.Up:
					return piece.connection2;

				case Direction.Left:
					return piece.connection3;

				case Direction.Down:
					return piece.connection4;

				case Direction.Right:
					return piece.connection1;
			}

			return false;
        }
    }
	public bool ConnectionDown
    {
        get
        {
			switch(orientation)
			{
				case Direction.Up:
					return piece.connection3;

				case Direction.Left:
					return piece.connection4;

				case Direction.Down:
					return piece.connection1;

				case Direction.Right:
					return piece.connection2;
			}

			return false;
        }
    }
	public bool ConnectionRight
    {
        get
        {
			switch(orientation)
			{
				case Direction.Up:
					return piece.connection4;

				case Direction.Left:
					return piece.connection1;

				case Direction.Down:
					return piece.connection2;

				case Direction.Right:
					return piece.connection3;
			}

			return false;
        }
    }
}
