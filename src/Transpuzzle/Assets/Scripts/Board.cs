using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Level level;

    public Tile[] grid;

    private void OnEnable() 
    {
        Tile.OnTileChange += (tile) => 
        {
            ClearBoard();
            CheckPathFrom(0);          
        };    
    }

    private void Start()
    {
        CheckPathFrom(0);
    }

    public void CheckPathFrom(int index)
    {
        Tile tile = grid[index];
        tile.SetOn(true);

        CheckConnections(tile, null);
    }

    public void CheckConnections(Tile tile, Direction? back)
    {
        // UP
        if(tile.ConnectionUp && back != Direction.Up)
        {
            Tile tileUp = GetNeighbor(tile.gridIndex, Direction.Up);
            if(tileUp != null && tileUp.ConnectionDown)
            {
                tileUp.SetOn(true);
                CheckConnections(tileUp, Direction.Down);
            }
        }

        // RIGHT
        if(tile.ConnectionRight && back != Direction.Right)
        {
            Tile tileRight = GetNeighbor(tile.gridIndex, Direction.Right);
            if(tileRight != null && tileRight.ConnectionLeft)
            {
                tileRight.SetOn(true);
                CheckConnections(tileRight, Direction.Left);
            }
        }

        // DOWN
        if(tile.ConnectionDown && back != Direction.Down)
        {
            Tile tileDown = GetNeighbor(tile.gridIndex, Direction.Down);
            if(tileDown != null && tileDown.ConnectionUp)
            {
                tileDown.SetOn(true);
                CheckConnections(tileDown, Direction.Up);
            }
        }

        // LEFT
        if(tile.ConnectionLeft && back != Direction.Left)
        {
            Tile tileLeft = GetNeighbor(tile.gridIndex, Direction.Left);
            if(tileLeft != null && tileLeft.ConnectionRight)
            {
                tileLeft.SetOn(true);
                CheckConnections(tileLeft, Direction.Right);
            }
        }
    }

    private Tile GetNeighbor(int tileIndex, Direction direction)
    {
        Tile tile = null;
        int neighborIndex = -1;

        switch (direction)
        {
            case Direction.Up:
                neighborIndex = tileIndex - level.size.x;
                break;
            case Direction.Right:
                neighborIndex = tileIndex + 1;
                break;
            case Direction.Down:
                neighborIndex = tileIndex + level.size.x;
                break;
            case Direction.Left:
                neighborIndex = tileIndex - 1;
                break;
        }

        if (neighborIndex >= 0 && neighborIndex < grid.Length)
        {
            tile = grid[neighborIndex];
        }

        return tile;
    }

    private void ClearBoard()
    {
        foreach(Tile t in grid)
        {
            t.SetOn(false);
        }
    }
}