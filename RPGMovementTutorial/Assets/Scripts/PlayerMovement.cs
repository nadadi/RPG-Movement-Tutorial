using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed = 1.5f;
	public LayerMask unwalkableMask;

	private Vector3 nextPosition;
	private bool isWalking;

	// Use this for initialization
	void Start () {
		nextPosition = transform.position;
		isWalking = false;
	}

	// Update is called once per frame
	void Update () {
		//Se obtienen los controles para verificar si se han pulsado las teclas...
		float horizontalMovement = Input.GetAxisRaw("Horizontal");
		float verticalMovement = Input.GetAxisRaw("Vertical");

		//Si el jugador esta quieto.
		if(transform.position == nextPosition){
			isWalking = false;
		}

		//Si es asi, se verifica que el personaje este en su posición exacta para poder moverse.
		if(horizontalMovement != 0 && !isWalking){
			nextPosition += Vector3.right * horizontalMovement;
		}else if(verticalMovement != 0 && !isWalking){
			nextPosition += Vector3.up * verticalMovement;
		}

		//Si la posición futura es distinta de la actual es porque el jugador quiere mover al personaje...
		if(nextPosition != transform.position){
			Vector2 dir = nextPosition - transform.position;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dir.sqrMagnitude, unwalkableMask.value);

			//Se verifica mediante un raycast que no se interponga nada al movimiento..
			if(hit.collider == null){

				/*if(!isWalking){
					//Movimiento abajo
					if(verticalMovement < 0){
						myAnim.SetBool("IsWalking", true);
						myAnim.SetInteger("Direction", 0);
					}

					//Movimiento izquierda
					if(horizontalMovement < 0){
						myAnim.SetBool("IsWalking", true);
						myAnim.SetInteger("Direction", 2);
					}

					//Movimiento derecha
					if(horizontalMovement > 0){
						myAnim.SetBool("IsWalking", true);
						myAnim.SetInteger("Direction", 1);
					}

					//Movimiento arriba
					if(verticalMovement > 0){
						myAnim.SetBool("IsWalking", true);
						myAnim.SetInteger("Direction", 3);
					}
				}*/

				//Movimiento 
				transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * speed);	
				if(!isWalking){
					isWalking = true;
				}
			}else{//Si se interpone algo entonces se resetea la posición futura a la actual y no se mueve...
				Debug.LogError("There is an obstacle, the player can't move.");
				nextPosition = transform.position;
			}
		}
	}
}
