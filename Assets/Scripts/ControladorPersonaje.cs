﻿using UnityEngine;
using System.Collections;

public class ControladorPersonaje : MonoBehaviour {

	public float fuerzaSalto = 100f;
	public bool enSuelo = true;
	public Transform comprobadorSuelo;
	public float comprobadorRadio = 0.8f;
	public LayerMask mascaraSuelo;
	private Animator animator;
	private bool dobleSalto = false;

	private bool corriendo = false;
	public float velocidad = 1f;

	void Awake (){
		animator = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		if (corriendo) {
			rigidbody2D.velocity = new Vector2(velocidad, rigidbody2D.velocity.y);
		}
		animator.SetFloat ("VelX", rigidbody2D.velocity.x);
		enSuelo = Physics2D.OverlapCircle (comprobadorSuelo.position, comprobadorRadio, mascaraSuelo);
		animator.SetBool ("isGrounded", enSuelo);
		if(enSuelo){
			dobleSalto = false; 
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if(corriendo){
				if ((enSuelo || !dobleSalto)) {
					rigidbody2D.AddForce(new Vector2(0, fuerzaSalto));
					if(!dobleSalto && !enSuelo){
						dobleSalto = true;
					}
				}
			}else{
				corriendo = true;
				NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeEmpezoACorrer");	
			}
		}
	}
}