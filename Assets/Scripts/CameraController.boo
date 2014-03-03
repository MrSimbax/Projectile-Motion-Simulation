import UnityEngine

class CameraController (MonoBehaviour): 
	
	public projectileMotion as ProjectileMotion
	public ballCam as Camera
	public fullCam as Camera
	
	private range as single
	private x as single
	private y as single
	private z as single
	
	public BallCam as bool:
		set:
			pass
		get:
			if ballCam != null:
				return ballCam.enabled
			else:
				Debug.LogError("ballCam is null!")
	
	def Start():
		range = -1
		ballCam.enabled = false
		fullCam.enabled = true
		return
	
	def Update ():
		if projectileMotion.Gravity == 0:
			fullCam.enabled = false
			ballCam.enabled = true
		if fullCam.enabled == true:
			range = projectileMotion.Range
			x = range / 2
			y = projectileMotion.TMaxHeight * 1
			z = - Mathf.Max(range * 1 / Mathf.Tan(60*Mathf.Deg2Rad),
							projectileMotion.InitialVelocity * 4 / Mathf.Tan(60*Mathf.Deg2Rad),
							projectileMotion.TMaxHeight * 4 / Mathf.Tan(60*Mathf.Deg2Rad))
			gameObject.transform.localPosition = Vector3(x, y, z)
		return
	
	public def Switch():
		ballCam.enabled = not ballCam.enabled
		fullCam.enabled = not fullCam.enabled
		return