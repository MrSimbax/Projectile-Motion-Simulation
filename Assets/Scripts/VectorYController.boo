import UnityEngine

class VectorYController (MonoBehaviour): 
	
	public projectileMotion as ProjectileMotion
	public cone as GameObject
	
	public scale as single = 1
	
	private show as bool
	
	def Start ():
		pass
	
	def Update ():
		if not show or (projectileMotion.Vy > -0.01 and projectileMotion.Vy < 0.01):
			gameObject.renderer.enabled = false
			cone.renderer.enabled = false
		else:
			gameObject.renderer.enabled = true
			cone.renderer.enabled = true
		gameObject.transform.localScale = Vector3(0.75, projectileMotion.Vy * scale, 0.75)
		gameObject.transform.localPosition = Vector3(0, gameObject.transform.localScale.y, 0)
		if projectileMotion.Vy < 0:
			cone.transform.localEulerAngles = Vector3(90, 0, 0)
		else:
			cone.transform.localEulerAngles = Vector3(-90, 0, 0)
		cone.transform.localPosition = Vector3(0, transform.localPosition.y * 2, 0)
		
	def SetShow(b as bool):
		show = b