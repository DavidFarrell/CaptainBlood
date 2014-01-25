#pragma strict

private var facingRight: boolean = true;	
private var grounded: boolean = true;	
private var horizAxis: float;
private var vertAxis: float;
public var moveForce: float;
public var jumpForce: float;
public var maxSpeed: float;


//Other vars
private var myTransform: Transform;
private var myRigidbody2D: Rigidbody2D;

private var groundHits: Collider2D[];
private var groundHitsNumber: int;

public var playerGrounder: Transform;

private var touchsLadder: boolean = false;

function Start () {

}

function Awake () {
	myTransform = transform;
	myRigidbody2D = myTransform.rigidbody2D;
	groundHits = new Collider2D[3];
	groundHitsNumber = 0;
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
	vertAxis = Input.GetAxis("Vertical1");
	
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
	
	//The player is contacting the ladder
	if (touchsLadder){
		myRigidbody2D.velocity = Vector2(myRigidbody2D.velocity.x, myRigidbody2D.velocity.y * vertAxis);
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

function OnTriggerEnter2D (other : Collider2D) {
		//Debug.Log("Contacting the ladder");
		if (other.gameObject.name == "ladder"){
			touchsLadder = true;
			Debug.Log("Contacting the ladder");
		}
}

function OnTriggerExit2D (other : Collider2D) {
		//Debug.Log("Contacting the ladder");
		if (other.gameObject.name == "ladder"){
			touchsLadder = false;
			Debug.Log("Contacting the ladder");
		}
}

function FixedUpdate(){
	/*groundHitsNumber = Physics2D.OverlapPointNonAlloc(myTransform.position, groundHits, 1 << 3 );	//The water layer then
    Debug.Log("groundHitsNumber: " + groundHitsNumber + "Pos: " + myTransform.position);
    if (groundHitsNumber != 0){
		Debug.Log("Over the larder");
	}*/
}