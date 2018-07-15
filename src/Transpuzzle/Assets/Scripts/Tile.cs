using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Piece piece;
    public Direction orientation;
	public int gridIndex;
    public bool canRotate = true;
    
    private bool on;
    private GameObject highlight;

    // EVENTS
    public delegate void TileChangeAction(Tile tile);
    public static event TileChangeAction OnTileChange;

    private void Awake() 
    {
        highlight = transform.Find("Highlight").gameObject;
    }
    
    private void Start() 
    {
        SetOn(on);
    }

    private void OnMouseDown() 
    {
        if(canRotate)
            RotateRight();
    }

    public void SetOrientation(Direction orientation)
    {
        this.orientation = orientation;

        Vector3 rotation = transform.rotation.eulerAngles;
        
        switch(orientation)
        {
            case Direction.Up:
                rotation.y = 0;
                break;

            case Direction.Right:
                rotation.y = 90;
                break;

            case Direction.Down:
                rotation.y = 180;
                break;

            case Direction.Left:
                rotation.y = -90;
                break;
        }

        transform.rotation = Quaternion.Euler(rotation);
    }

    public void SetOn(bool isOn)
    {
        on = isOn;
        highlight.SetActive(on);
    }

    public bool IsOn()
    {
        return on;
    }

    private void RotateRight()
    {        
        Direction[] dirs = (Direction[])System.Enum.GetValues(typeof(Direction));

        int i = System.Array.IndexOf(dirs, orientation) + 1;

        if(i >= dirs.Length)
            i = 0;

        SetOrientation(dirs[i]);

        if(OnTileChange != null)
            OnTileChange.Invoke(this);
    }

	public bool ConnectionUp
    {
        get
        {
			switch(orientation)
			{
				case Direction.Up:
					return piece.connection1;

				case Direction.Right:
					return piece.connection4;

				case Direction.Down:
					return piece.connection3;

				case Direction.Left:
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
					return piece.connection2;

				case Direction.Right:
					return piece.connection1;

				case Direction.Down:
					return piece.connection4;

				case Direction.Left:
					return piece.connection3;
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

				case Direction.Right:
					return piece.connection2;

				case Direction.Down:
					return piece.connection1;

				case Direction.Left:
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
					return piece.connection4;

				case Direction.Right:
					return piece.connection3;

				case Direction.Down:
					return piece.connection2;

				case Direction.Left:
					return piece.connection1;
			}

			return false;
        }
    }

    private void OnDrawGizmos()
    {
        if (piece != null)
        {
            Bounds bounds = GetComponent<BoxCollider>().bounds;

            float size = .25f;
            Vector3 up = new Vector3(bounds.center.x, bounds.max.y, bounds.max.z - size);
            Vector3 right = new Vector3(bounds.max.x - size, bounds.max.y, bounds.center.z);
            Vector3 down = new Vector3(bounds.center.x, bounds.max.y, bounds.min.z + size);
            Vector3 left = new Vector3(bounds.min.x + size, bounds.max.y, bounds.center.z);

            Color red = Color.red;
            Color green = Color.green;

            Gizmos.color = red;

            Gizmos.color = ConnectionUp ? green : red;
            Gizmos.DrawSphere(up, size);

            Gizmos.color = ConnectionRight ? green : red;
            Gizmos.DrawSphere(right, size);

            Gizmos.color = ConnectionDown ? green : red;
            Gizmos.DrawSphere(down, size);

            Gizmos.color = ConnectionLeft ? green : red;
            Gizmos.DrawSphere(left, size);
        }
    }
}
