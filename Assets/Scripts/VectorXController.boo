import UnityEngine

class VectorXController (MonoBehaviour): 
	
	public projectileMotion as ProjectileMotion
	public cone as GameObject
	
	public scale as single = 1
	
	private show as bool
	
	def Start ():
		pass
	
	def Update ():
		if not show or (projectileMotion.Vx > -0.01 and projectileMotion.Vx < 0.01):
			gameObject.renderer.enabled = false
			cone.renderer.enabled = false
		else:
			gameObject.renderer.enabled = true
			cone.renderer.enabled = true
		gameObject.transform.localScale = Vector3(0.75, projectileMotion.Vx * scale, 0.75)
		gameObject.transform.localPosition = Vector3(gameObject.transform.localScale.y, 0, 0)
		if projectileMotion.Vx < 0:
			cone.transform.localEulerAngles = Vector3(-180, 90, 90)
		else:
			cone.transform.localEulerAngles = Vector3(0, 90, 90)
		cone.transform.localPosition = Vector3(transform.localPosition.x * 2, 0, 0)
	
	def SetShow(b as bool):
		show = b