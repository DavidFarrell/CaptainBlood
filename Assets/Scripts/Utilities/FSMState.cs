using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// FSM 	: Finite State Machine System Class : Version 1b
/// 
/// This is a Deterministic Finite State Machine based on chapter 3.1 of Game Programming Gems 1 by Eric Dybsend.
/// 
/// Daniel Gallagher
/// Lost Zombie Studios Ltd.
/// 10/04/2012
/// 
/// Revision 1b : Adding object handling to entering and leaving states
/// Daniel Gallagher
/// 17/04/2012
/// </summary>

public class FSMState {
	
	// This method is called before the state is made the current state (see: OnExit())
	public virtual void OnEnter() {}
	
	// Overloaded Method
	// This method is called before the state is made the current state (see: OnExit())
	public virtual void OnEnter(object userData) {}
	
	// This method is used to determine if the current state is the correct state to be in
	// If the current state is not the correct state to be in it can be used to change state
	public virtual void Reason() {}
	
	// This method is called on every Update by the FSM
	public virtual void OnUpdate() {}
	
	// This method is called on every FixedUpdate by the FSM
	public virtual void OnFixedUpdate() {}
	
	// This method is called on every LateUpdate by the FSM
	public virtual void OnLateUpdate() {}
	
	// This method is called before leaving the current State by the FSM
	public virtual void OnExit() {}
	
	public virtual void OnGUI() {}
	
	public virtual void OnPostRender() {}
}
