#pragma strict

private var facingRight: boolean = true;	
private var grounded: boolean = true;	
private var horizAxis: float;
public var moveForce: float;
public var jumpForce: float;
public var maxSpeed: float;


//Other vars
private var myTransform: Transform;
private var myRigidbody2D: Rigidbody2D;


public var playerGrounder: Transform;

function Start () {

}

function Awake () {
	myTransform = transform;
	myRigidbody2D = myTransform.rigidbody2D;
}

function LineCasting() {
	Debug.DrawLine(myTransform.position, playerGrounder.position, Color.cyan);
	grounded = Physics2D.Linecast(myTransform.position, playerGrounder.position, 1<< LayerMask.NameToLayer("Middleground"));

}

function Flip(){
	// Switch the way the player is labelled as facing.
	facingRight = !facingRight;
	
	// Multiply the player's x local scale by -1.
	var theScale: Vector3 = myTransform.localScale;
	theScale.x *= -1;
	myTransform.localScale = theScale;
}


function Update () {

	LineCasting();
	//retrieve axis info
	horizAxis = Input.GetAxis("Horizontal1");
	
	if(Mathf.Abs(myRigidbody2D.velocity.x) > 0.6 ){
		if (Mathf.Abs(myRigidbody2D.velocity.x) >= maxSpeed){
			myRigidbody2D.velocity = Vector2(Mathf.Sign(myRigidbody2D.velocity.x) * maxSpeed, myRigidbody2D.velocity.y);	// ... set the player's velocity to the maxSpeed in the x axis.
		}
		else{						// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
			myRigidbody2D.AddForce(Vector2.right * horizAxis * moveForce);				// ... add a force to the player.
		}
	}
	else{
		if(horizAxis * myRigidbody2D.velocity.x < maxSpeed)
			myRigidbody2D.AddForce(Vector2.right * horizAxis * moveForce);				// ... add a force to the player.
	}
	
	if (Input.GetButtonDown ("Jump1") && grounded == true ){
			myRigidbody2D.AddForce (Vector2.up * jumpForce);
	}
	
	// If the input is moving the player right and the player is facing left...
	if(horizAxis > 0 && !facingRight){
		// ... flip the player.
		Flip();
	}
	// Otherwise if the input is moving the player left and the player is facing right...
	else if(horizAxis < 0 && facingRight){
		// ... flip the player.
		Flip();
	}
	
	
	
	

}