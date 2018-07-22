using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Tile tile;
    Animator anim;
    float Y;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile"))
        {

            tile = other.GetComponent<Tile>();
            if (tile.IsOn())
            {

                switch (tile.piece.type)
                {
                    case PieceType.Turn:
                        anim.Play("Move_TurnN");
                        switch (tile.orientation) {
                            case Direction.Right: {

                                    Y = 90;
                                  
                                    break;
                                
                                }
                            case Direction.Up:
                                {

                                    Y= 90;


                                    break;
                                }

                            case Direction.Down:
                                {

                                    Y = 180;


                                    break;
                                }

                            case Direction.Left:
                                {

                                    Y = -90;


                                    break;
                                }






                        }
                        break;

                        case PieceType.Intersection:
                        /** Comparar quais Tiles estão fazendo Conexão e quais não estão. Entretanto, existe
                         * a necessidade de comparar por onde passou o carro. Em progresso**/



                        break;

                    case PieceType.Straight:

                        Rotate();
                        break;
                }
            
        }

        }
    }
 public void Rotate() {

        anim.rootRotation = Quaternion.Euler(0,Y,0);
        anim.Play("Move_Straight");

    }

    void Start()
    {
        anim = GetComponent<Animator>();


    }
}