﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {

	public bool scrolling, parallax;

	public float backgroundSize;
	public float parallaxSpeed;

	private Transform cameraTransform;
	private Transform[] layers;
	private float viewzone = 10;
	private int leftIndex;
	private int rightIndex;
	private float lastCameraX;

	private void Start() {
		cameraTransform = Camera.main.transform;
		lastCameraX = cameraTransform.position.x;
		layers = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
		{
			layers[i] = transform.GetChild(i);
		}

		leftIndex = 0;
		rightIndex = layers.Length - 1;
	}

	private void Update() {

		if (parallax)
		{
			float deltaX = cameraTransform.position.x - lastCameraX;
			transform.position += Vector3.right * (deltaX * parallaxSpeed);	
		}	


		lastCameraX = cameraTransform.position.x;

		if (scrolling)
		{
			
			if (cameraTransform.position.x > layers[rightIndex].transform.position.x - viewzone)
			{
				layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize );

				float x = layers[leftIndex].localPosition.x;
				
				float z = layers[leftIndex].localPosition.z * -1;

				layers[leftIndex].localPosition = new Vector3 (x, 0, z);
				
				rightIndex = leftIndex;
				leftIndex++;
				if(leftIndex == layers.Length)
					leftIndex = 0; 
			} 
		}
	}
}
