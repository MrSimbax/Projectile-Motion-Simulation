import UnityEngine

class CatapultAnimationController (MonoBehaviour): 
	
	private anim as Animator
	
	private naciaganieHash as int = Animator.StringToHash("Naciagnij")
	private wyrzutHash as int = Animator.StringToHash("Wyrzuc")
	
	private idle1StateHash as int = Animator.StringToHash("Base Layer.Idle")
	private idle2StateHash as int = Animator.StringToHash("Base Layer.Idle 2")
	
	private naciagnij as bool
	private wyrzuc as bool
	private powroc as bool
	
	def Start ():
		anim = GetComponent(Animator)
		naciagnij = false
		wyrzuc = false
		powroc = false
	
	def Update ():
		stateInfo as AnimatorStateInfo = anim.GetCurrentAnimatorStateInfo(0)
		if naciagnij and stateInfo.nameHash == idle1StateHash:
			anim.SetTrigger(naciaganieHash)
			naciagnij = false
		if wyrzuc and stateInfo.nameHash == idle1StateHash:
			anim.SetTrigger(wyrzutHash)
			wyrzuc = false
		if powroc and stateInfo.nameHash == idle2StateHash:
			anim.SetTrigger(naciaganieHash)
			powroc = false
	
	def Naciagnij():
		naciagnij = true
	
	def Wyrzuc():
		wyrzuc = true
	
	def Powroc():
		powroc = true
		