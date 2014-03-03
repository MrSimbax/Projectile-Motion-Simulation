import UnityEngine

class SimulationGUI (MonoBehaviour):

	public projectileMotion as ProjectileMotion
	public cameraController as CameraController
	public vectorXController as VectorXController
	public vectorYController as VectorYController
	public vectorVController as VectorVController
	public precision as single = 3
	
	# controls window
	private controlsWHeight as int = 50
	private controlsWWidth as int = Screen.width
	private controlsWLeft as int = 0
	private controlsWTop as int = Screen.height - controlsWHeight
	private controlsW as Rect = Rect(controlsWLeft, controlsWTop, controlsWWidth, controlsWHeight)
	
	# helping
	private pause as string = "Pause"
	
	# values window
	private valuesWHeight as int = 200
	private valuesWWidth as int = Screen.width * 0.5
	private valuesWLeft as int = Screen.width * 0.5
	private valuesWTop as int = 0
	private valuesW as Rect = Rect(valuesWLeft, valuesWTop, valuesWWidth, valuesWHeight)
	
	# values window - sliders
	private initialVelocity as single
	private launchAngle as single
	private gravity as single
	private simulationSpeed as single
	private height as single
			#private slidersWidth as int = 200
	
	# values window - text fields
	private initialVelocityString as string = "0.0"
	private launchAngleString as string = "0.0"
	private gravityString as string = "0.0"
	private simulationSpeedString as string = "0.0"
	private heightString as string = "0.0"
	private textFieldsWidth as int = 45
	
	# about window
	private showAboutWindow as bool = false
	private aboutWHeight as int = 300
	private aboutWWidth as int = 300
	private aboutWLeft as int = (Screen.width - aboutWWidth) / 2
	private aboutWTop as int = (Screen.height - aboutWHeight) / 2
	private aboutW as Rect = Rect(aboutWLeft, aboutWTop, aboutWWidth, aboutWHeight)
	
	# ---
	private boxesWidth as int = 300
	
	# checkboxes
	private ballCamera as bool
	private airResistance as bool
	private showVectorX as bool = false
	private showVectorY as bool = false
	private showVectorV as bool = false
	private showTrajectory as bool = false
	
	def Start ():
		initialVelocity = projectileMotion.InitialVelocity
		launchAngle = projectileMotion.LaunchAngle
		gravity = projectileMotion.Gravity
		simulationSpeed = projectileMotion.SimulationSpeed
		height = projectileMotion.Height
		
		initialVelocityString = Round(initialVelocity)
		launchAngleString = Round(launchAngle)
		gravityString = Round(gravity)
		simulationSpeedString = Round(simulationSpeed)
		heightString = Round(height)
		
		ballCamera = cameraController.BallCam
		airResistance = false
		return
	
	def Update ():
		pass
	
	def OnGUI():
		controlsW = GUI.Window(0, controlsW, Controls, "Controls")
		valuesW = GUI.Window(1, valuesW, Values, "Values (cannot be changed during simulation)")
		if showAboutWindow:
			aboutW = GUI.Window(2, aboutW, About, "About")
		return
	
	private def Controls(id as int):
		GUILayout.BeginArea(Rect(5, 20, controlsWWidth - 10, controlsWHeight - 5))
		GUILayout.BeginHorizontal()
		if GUILayout.Button("Start"):
			projectileMotion.StartSimulation()
		if GUILayout.Button(pause):
			if pause == "Pause" and projectileMotion.Started:
				projectileMotion.Pause()
				pause = "Resume"
			elif pause == "Resume":
				projectileMotion.Resume()
				pause = "Pause"
		if GUILayout.Button("Reset"):
			projectileMotion.Reset()
			pause = "Pause"
		if GUILayout.Button("Default"):
			projectileMotion.SetDefaultSettings()
			pause = "Pause"
			Start()
		if GUILayout.Button("About"):
			showAboutWindow = true
		if GUILayout.Button("Exit"):
			Application.Quit()
		GUILayout.EndHorizontal()
		GUILayout.EndArea()
		return
	
	private def Values(id as int):
		GUILayout.BeginArea(Rect(5, 20, valuesWWidth - 10, valuesWHeight - 5))
		GUILayout.BeginVertical()
		
		GUILayout.BeginHorizontal()
		# INITIAL VELOCITY
		GUILayout.Box("Initial Velocity [meters per seconds]", GUILayout.Width(boxesWidth))
		initialVelocity2 as single = GUILayout.HorizontalSlider(initialVelocity, 5.0, 100.0)
		initialVelocityString = GUILayout.TextField(initialVelocityString, 6, GUILayout.Width(textFieldsWidth))
		ChangeValueAndTextFieldAndSlider("InitialVelocity", initialVelocity, initialVelocityString, initialVelocity2)
		GUILayout.EndHorizontal()
		
		GUILayout.BeginHorizontal()
		# LAUNCH ANGLE
		GUILayout.Box("Launch Angle [degrees]", GUILayout.Width(boxesWidth))
		launchAngle2 as single = GUILayout.HorizontalSlider(launchAngle, 0.0, 90.0)
		launchAngleString = GUILayout.TextField(launchAngleString, 6, GUILayout.Width(textFieldsWidth))
		ChangeValueAndTextFieldAndSlider("LaunchAngle", launchAngle, launchAngleString, launchAngle2)
		GUILayout.EndHorizontal()
		
		GUILayout.BeginHorizontal()
		# GRAVITY ACCELERATION
		GUILayout.Box("Gravity Acceleration [meters per square seconds]", GUILayout.Width(boxesWidth))		
		gravity2 as single = GUILayout.HorizontalSlider(gravity, 0.0, 10.0)
		gravityString = GUILayout.TextField(gravityString, 6, GUILayout.Width(textFieldsWidth))
		ChangeValueAndTextFieldAndSlider("Gravity", gravity, gravityString, gravity2)
		GUILayout.EndHorizontal()
		
		GUILayout.BeginHorizontal()
		# SIMULATION SPEED
		GUILayout.Box("Simulation Speed (fraction)", GUILayout.Width(boxesWidth))
		simulationSpeed2 as single = GUILayout.HorizontalSlider(simulationSpeed, 0.25, 8.0)
		simulationSpeedString = GUILayout.TextField(simulationSpeedString, 6, GUILayout.Width(textFieldsWidth))
		ChangeValueAndTextFieldAndSlider("SimulationSpeed", simulationSpeed, simulationSpeedString, simulationSpeed2)
		GUILayout.EndHorizontal()
		
		GUILayout.BeginHorizontal()
		# HEIGHT
		GUILayout.Box("Height [meters]", GUILayout.Width(boxesWidth))
		height2 as single = GUILayout.HorizontalSlider(height, 0.0, 10.0)	
		heightString = GUILayout.TextField(heightString, 6, GUILayout.Width(textFieldsWidth))
		ChangeValueAndTextFieldAndSlider("Height", height, heightString, height2)
		GUILayout.EndHorizontal()
		
		# CHECKBOXES
		GUILayout.BeginHorizontal()
		ballCamera = GUILayout.Toggle(ballCamera, "Camera on point")
		airResistance = GUILayout.Toggle(airResistance, "Air resistance (not implemented yet)")
		showTrajectory = GUILayout.Toggle(showTrajectory, "Show trajectory")
		if ballCamera != cameraController.BallCam:
			cameraController.Switch()
		projectileMotion.ShowTrajectory = showTrajectory
		GUILayout.EndHorizontal()
		
		GUILayout.BeginHorizontal()
		showVectorV = GUILayout.Toggle(showVectorV, "Show V")
		showVectorX = GUILayout.Toggle(showVectorX, "Show Vx")
		showVectorY = GUILayout.Toggle(showVectorY, "Show Vy")
		vectorVController.SetShow(showVectorV)
		vectorXController.SetShow(showVectorX)
		vectorYController.SetShow(showVectorY)
		GUILayout.EndHorizontal()
		
		GUILayout.EndVertical()
		GUILayout.EndArea()
		return
	
	private def About(id as int):
		GUILayout.BeginArea(Rect(5, 20, aboutWWidth - 10, aboutWHeight - 5))
		
		GUILayout.BeginVertical()
		
		GUILayout.Label("Projectile Motion Simulation\n\n" +\
		"Authors:\nProgrammer - Mateusz Przybył\nDesigner - Piotr Klemczak\n\n" +\
		"Runs on Unity3D engine (Free version), 3D models were made in Blender and are on CC BY-NC 3.0 license.\n\n" +\
		"Unity project and source code are on GPLv3 license and will be available on github.com/mrsimbax/projectilemotionunity\n\n" +\
		"This software is part of the educational project for school I Liceum Ogólnokształcące w Lesznie.")
		
		if GUILayout.RepeatButton("Ok"):
			showAboutWindow = false
		
		GUILayout.EndVertical()
		
		GUILayout.EndArea()
	
	private def Round(var as single) as string:
		return ((Mathf.Round(var * 10**precision) / 10**precision) cast single).ToString("0."+"0"*precision)
	
	private def ChangeValueAndTextFieldAndSlider(name as string, ref originalValue as single, ref stringValue as string, changedValue as single):
		if changedValue != originalValue or\
			   stringValue != originalValue.ToString() and\
			   Event.current.keyCode == KeyCode.Return and\
			   single.TryParse(stringValue, changedValue):
	   		originalValue =	Change(changedValue, name)
		   	stringValue = Round(originalValue)

	private def Change(var as single, what as string) as single:
		if what == "InitialVelocity":
			projectileMotion.InitialVelocity = var
			return projectileMotion.InitialVelocity
		elif what == "LaunchAngle":
			projectileMotion.LaunchAngle = var
			return projectileMotion.LaunchAngle
		elif what == "Gravity":
			projectileMotion.Gravity = var
			return projectileMotion.Gravity
		elif what == "SimulationSpeed":
			projectileMotion.SimulationSpeed = var
			return projectileMotion.SimulationSpeed
		elif what == "Height":
			projectileMotion.Height = var
			return projectileMotion.Height
		else:
			return 0