        
	        Requirements List:
 ***************************************
- EVERY SCRIPT you add must have the same unique namespace:
	
	namespace MyTeam
	{
		public class MyClass : monobehavior
		{
		// Your code here //

		}
	}

	- ^ If you don't do this, there may be naming conflicts with other team's projects
	- Give the folder you submit this same name, for the same reason
	
	
	        Constraints List:
 ***************************************
- Don't change ANYTHING in project settings
    - There are a few added collision and rendering layers for you to work with. Hopefully these will be enough with how simple the games are
    - You can check the collision matrix in the physics2D section, but again, don't change them
	
 *****************************************
 - Keep sounds to the Minigame scriptable object
    - This is so your sound will be faded out on transition rather than cut off or continue playing
    
 ***************************************** 
- Stick to WASD/arrow keys and space for your games controls. 
    - You can technically still call Input.GetKeyDown, but this may confuse the player. Keeping the controls simple makes the snappy-ness of these games possible
    
    
        Tips List:
 *****************************************
 - The name of your scene is made into text that flashes before your game starts
    - Use this text to give some direction to the player
    
 *****************************************
 - On the minigame manager object in the inspector, theres a bool called "debug game only". Make this true if you want to test the game alone
 
 *****************************************
 - Music Making Guidelines: 
    - 141 BPM
    - Short games are 2 measures (~3.4 seconds)
    - Long games are 4 measure (~6.8 seconds)

        SUBMISSION CHECKLIST:
 *****************************************
 - Did you delete the example script and the example sound and music clips?
 - Did you rename the folder that has all of your assets from "MyTeam"?
 - Did you put all of your scripts in the same namespace?
 - Did you remove all of your debug.log and print statements? (so that we can debug easier)
 - Did you ONLY zip up YOUR scene and assets in your submission file?



