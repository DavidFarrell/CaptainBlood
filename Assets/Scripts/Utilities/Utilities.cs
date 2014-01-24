using UnityEngine;
using System.Collections;

//UTILITY FUNCTIONS

public class Utilities {
	
	/// <summary>
	/// Resizes the GUI based on the games display resolution
	/// </summary>
	public static Rect ResizeGUI( Rect rect ){
		float displayWidth = rect.width/1280;
		float rectWidth = displayWidth*Screen.width;
		float displayHeight = rect.height/720;
		float rectHeight = displayHeight*Screen.height;
		float rectX = ( rect.x / 1280 * Screen.width );
		float rectY = ( rect.y / 720 * Screen.height );
		return new Rect ( rectX, rectY, rectWidth, rectHeight ) ;
	}
	
	public static int RollDice( int noRolls, int DiceSize ){
		int score = 0;
		for ( int i = 0; i < noRolls; i++ ){
			score += Random.Range( 1, DiceSize );
		}
		return score;
	}
}