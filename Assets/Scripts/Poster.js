#pragma strict


public class Poster extends MonoBehaviour{
	
	public var post1: Sprite;
	public var post2: Sprite;
	public var post3: Sprite;
	
	private var state: int = 0;
	
	private var mySR: SpriteRenderer;
	
	function Start () {
		mySR = gameObject.GetComponent(SpriteRenderer);
		testPoster();
	}
	
	function Update () {
	
	}
	
	function ChangePoster(poster: int){
		//Changes the poster to display to the one specified on the parameter 'poster'
		switch (poster){
		case 0:
			mySR.sprite = post1;
			state = 0;
		break;
		case 1: 
			mySR.sprite = post2;
			state = 1;
		break;
		case 2: 
			mySR.sprite = post3;
			state = 2;
		break;
		default: 
			Debug.Log("There's only 3 posters, from 0 to 2. Tried to access " + poster);
		}
	}
	
	public function GetState(){
	//Returns the actual state of the poster
		return state;
	}
	
	function testPoster(){
	//Just to check the ChangePoster function
		var i: int = 0;
		for(;;){
			yield new WaitForSeconds(1);
			Debug.Log("Changing to poster " + i);
			ChangePoster(i);
			i++;
			if (i == 3) i = 0;
		}
	}
}