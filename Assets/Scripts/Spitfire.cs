using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitfire : MonoBehaviour
{
	public float speed = 20;
	
	public float maxSpeed = 100;
	public float minSpeed = 5;
	
	public float rootSpeed1 = 50;
	public float rootSpeed2 = 50;
	
	void Update()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
		
		//Aumenta a velocidade do aviao
		if(Input.GetKeyDown(KeyCode.Q))
		{
			if(speed <= maxSpeed)
			{
				speed += 2; 
			}
		}
		
		//Diminuir a velocidade
		if(Input.GetKeyDown(KeyCode.E))
		{
			if(speed >= minSpeed)
			{
				speed-- ;
			}
		}
		
		//Movimentar o Aviao
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * rootSpeed1 * Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.back * rootSpeed2 * Time.deltaTime);
		}	
		
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			transform.Rotate(Vector3.left * rootSpeed1 * Time.deltaTime);
		}	
		
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			transform.Rotate(Vector3.right * rootSpeed1 * Time.deltaTime);
		}	
	}
}