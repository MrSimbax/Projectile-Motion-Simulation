import UnityEngine

class VectorVController (MonoBehaviour): 
	
	public projectileMotion as ProjectileMotion
	public cone as GameObject
	
	public scale as single = 1
	
	private show as bool
	
	def Start ():
		pass
	
	def Update ():
		if not show or (projectileMotion.V > -0.01 and projectileMotion.V < 0.01):
			gameObject.renderer.enabled = false
			cone.renderer.enabled = false
		else:
			gameObject.renderer.enabled = true
			cone.renderer.enabled = true
		transform.localScale = Vector3(0.75, projectileMotion.V * scale, 0.75)
		transform.localPosition = Vector3(projectileMotion.Vx * scale, projectileMotion.Vy * scale, 0)
		transform.localEulerAngles = Vector3(0, 0, -90 + projectileMotion.Angle)
		cone.transform.localEulerAngles = Vector3(-projectileMotion.Angle, 90, 90)
		cone.transform.localPosition = Vector3(transform.localPosition.x * 2,
												transform.localPosition.y * 2,
												0)
	
	def SetShow(b as bool):
		show = b