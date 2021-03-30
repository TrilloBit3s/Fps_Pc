using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour 
{
	float life = 4f;
	float timer;
	Vector3 originalLocalScale;

	void Start() 
	{
		originalLocalScale = transform.localScale;
	}

	void Update() 
	{
		timer += Time.deltaTime;
		if (timer >= life) 
		{
			Destroy(gameObject);
		}
		transform.localScale = Vector3.Lerp(originalLocalScale, Vector3.zero, timer / life);
	}
}