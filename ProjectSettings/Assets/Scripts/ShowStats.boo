import UnityEngine

class ShowStats (MonoBehaviour): 
	
	public projectileMotion as ProjectileMotion
	public point as GameObject
	public precision as int = 3
	
	def Start ():
		guiText.fontSize = Screen.height / 32
		return
	
	def Update ():
		guiText.text =\
		"Current time: " + Round(projectileMotion.CurrentTime) + " seconds\n" +\
		"Delta time: " + Time.fixedDeltaTime + " seconds\n" +\
		"Current position X: " + Round(projectileMotion.Ball.transform.position.x) + " meters\n" +\
		"Current position Y: " + Round(projectileMotion.Ball.transform.position.y) + " meters\n" +\
		"Current v: " + Round(projectileMotion.V) + " meters per second\n" +\
		"Current vx: " + Round(projectileMotion.Vx) + " meters per second\n" +\
		"Current vy: " + Round(projectileMotion.Vy) + " meters per second\n" +\
		"Current angle: " + Round(projectileMotion.Angle) + " degrees\n" +\
		"Max height: " + Round(projectileMotion.MaxHeight) + " meters\n" +\
		"Time to max height: " + Round(projectileMotion.TimeToMaxHeight) + " seconds\n"
		#"\n" +\
		#"Initial Velocity: " + Round(projectileMotion.InitialVelocity) + " meters per second\n" +\
		#"Launch Angle: " + Round(projectileMotion.LaunchAngle) + " degrees\n" +\
		#"Gravity: " + Round(projectileMotion.Gravity) + " meters per square second\n"
		
		return
		
	private def Round(var as single) as string:
		return ((Mathf.Round(var * 10**precision) / 10**precision) cast single).ToString("0."+"0"*precision)