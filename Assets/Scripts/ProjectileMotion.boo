import UnityEngine

class ProjectileMotion (MonoBehaviour): 
	# This script should be put on the bullet
	
	public catapultAnimationController as CatapultAnimationController
	
	public ballParent as Transform
	public catapultParent as Transform
	public catapultObject as GameObject
	
	#public catapultBase as GameObject
	
	# public:
	public Ball as GameObject:
		set:
			pass
		get:
			return gameObject
	
	public V as single:
		set:
			pass
		get:
			return v
	
	public Vx as single:
		set:
			pass
		get:
			return vx
	
	public Vy as single:
		set:
			pass
		get:
			return vy
	
	public Angle as single:
		set:
			pass
		get:
			return angle
	
	public InitialVelocity as single:
		set:
			if value > 100 or value < 5:
				pass
			elif not running:
				v0 = v = value
				Reset()
				#Debug.Log("v0 changed to "+v0)
		get:
			return v0
		
	public Gravity as single:
		set:
			if value > 10 or value < 0:
				pass
			elif not running:
				g = value
				Reset()
		get:
			return g
			
	public LaunchAngle as single:
		set:
			if value > 90 or value < 0:
				pass
			elif not running:
				alpha = angle = value
				alphaRad = alpha * Mathf.Deg2Rad
				Reset()
		get:
			return alpha
			
	public SimulationSpeed as single:
		set:
			if value > 8 or value < 0:
				pass
			elif not running:
				simulationSpeed = value
				Reset()
		get:
			return simulationSpeed
	
	public Base as GameObject:
		set:
			pass
		get:
			return base
	
	public Height as single:
		set:
			if value < 0 or value > 10:
				return
			if value <= transform.localScale.y / 2:
				base.renderer.enabled = false
			else:
				base.renderer.enabled = true
			if not running:
				Reset()
				#delta as single = value - y0
				h = value - 0.5
				base.transform.localPosition = Vector3(0, h / 2, 0)
				base.transform.localScale = Vector3(1, h / 2, 1)
				y0 = value
				
				#catapultBase.transform.localPosition += Vector3(0, delta, 0)
				#catapultBase.transform.localScale += Vector3(0, delta, 0)
				Reset()
		get:
			return y0
	
	public TimeToMaxHeight as single:
		set:
			pass
		get:
			return timeTohmax
	
	public MaxHeight as single:
		set:
			pass
		get:
			return hmax
	
	public TMaxHeight as single:
		set:
			pass
		get:
			if g != 0:
				return y0 + (v0 * Mathf.Sin(alphaRad))**2 / (2 * g)
			else:
				return Mathf.Infinity
	
	public CurrentTime as single:
		set:
			pass
		get:
			return t
	
	public Range as single:
		set:
			pass
		get:
			if g != 0:
				return (v0 * Mathf.Cos(alphaRad) / g) * (v0 * Mathf.Sin(alphaRad) + ((v0 * Mathf.Sin(alphaRad))**2 + 2.0f * g * y0)**(0.5))
			else:
				return Mathf.Infinity
	
	public ShowTrajectory as bool:
		set:
			showTrajectory = value
		get:
			return showTrajectory
	
	public Started as bool:
		set:
			pass
		get:
			return started
	
	# private:
	
	# initial values
	private v0 as single = 50.0  # m/s
	private g as single = 9.81  # m/s**2
	private alpha as single = 45.0  # degrees
	private simulationSpeed as single = 1.0 # fraction
	private z0 as single # height # meters
	
	# changing values
	private v as single = 0.0 # velocity
	private vx as single = 0.0
	private vy as single = 0.0
	private angle as single
	private t as single # actual time
	private x as single # position
	private y as single
	private z as single
	private hmax as single = 0.0 # max height
	private timeTohmax as single = 0.0
	
	# helping values
	private alphaRad as single
	private x0 as single
	private y0 as single
	private base as GameObject # where is the catapult?
	private trajectory as TrailRenderer # trail
	private started as bool
	private running as bool
	private done as bool
	private trailOn as bool
	private prevTrailOn as bool # for performance reasons
	private showTrajectory as bool
	
	# methods
	public def Start():
		catapultAnimationController.Naciagnij()
		x0 = gameObject.transform.localPosition.x
		y0 = gameObject.transform.localPosition.y
		z0 = gameObject.transform.localPosition.z
		alphaRad = alpha * Mathf.Deg2Rad
		t = 0.0
		v = v0
		vx = v0 * Mathf.Cos(alphaRad)
		vy = v0 * Mathf.Sin(alphaRad)
		angle = alpha
		done = false
		running = false
		base = GameObject.Find("Base")
		trajectory = GetComponent(TrailRenderer)
		trailOn = false
		prevTrailOn = false
		return
	
	public def Update ():
		if trailOn != prevTrailOn:
			if trailOn:
				trajectory.time = Mathf.Infinity
				prevTrailOn = true
			else:
				trajectory.time = 0
				prevTrailOn = false
		#trajectory.renderer.enabled = showTrajectory
		if showTrajectory:
			trajectory.endWidth = 1
			trajectory.startWidth = 1
		else:
			trajectory.endWidth = 0
			trajectory.startWidth = 0
	
	public def FixedUpdate():
		if not running:
			return
		x = x0 + v0 * Mathf.Cos(alphaRad) * t
		y = y0 + v0 * Mathf.Sin(alphaRad) * t - g * t * t / 2
		z = z0
		#vx = const
		vy = v0 * Mathf.Sin(alphaRad) - g * t
		v = (vx**2 + vy**2)**(0.5)
		if not (vx > -0.01 and vx < 0.01):
			angle = Mathf.Atan(vy/vx) * Mathf.Rad2Deg
		else:
			if vy < 0:
				angle = -90
			else:
				angle = 90
		if not done and y >= 0.0:
			gameObject.transform.localPosition = Vector3(x,y,z)
			t += Time.fixedDeltaTime * simulationSpeed
		elif not done and vy < 0:
			# y = 0 # cheat
			gameObject.transform.localPosition = Vector3(x, y, z)
			t += Time.fixedDeltaTime * simulationSpeed
			done = true
			running = false
		if y > hmax:
			hmax = y
			timeTohmax = t
		return
	
	public def Reset():
		gameObject.transform.localPosition.x = x0
		gameObject.transform.localPosition.y = y0
		gameObject.transform.localPosition.z = z0
		catapultObject.transform.parent = ballParent.transform;
		catapultAnimationController.Powroc()
		t = 0.0
		v = v0
		vx = v0 * Mathf.Cos(alphaRad)
		vy = v0 * Mathf.Sin(alphaRad)
		angle = alpha
		hmax = 0
		timeTohmax = 0
		done = false
		running = false
		started = false
		trailOn = false
		return
	
	public def Pause():
		if started:
			running = false
		return
	
	public def StartSimulation():
		if not started:
			catapultObject.transform.parent = catapultParent.transform;
			catapultAnimationController.Wyrzuc()
			started = true
			Resume()
		return
	
	public def Resume():
		if started:
			trailOn = true
			running = true
		return
	
	public def SetDefaultSettings():
		if not running:
			v0 = 50.0
			g = 9.81
			alpha = angle = 45.0
			alphaRad = alpha * Mathf.Deg2Rad
			simulationSpeed = 1.0
			Height = 2.5
			Reset()
		return
	
	public def isRunning():
		return running