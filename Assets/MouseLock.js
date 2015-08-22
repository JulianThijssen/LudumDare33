#pragma strict

function Start() {
}

// Called when the cursor is actually being locked

function DidLockCursor () {
	Debug.Log("Locking cursor");
}

// Called when the cursor is being unlocked
// or by a script calling Screen.lockCursor = false;
function DidUnlockCursor () {
	Debug.Log("Unlocking cursor");
}

function OnMouseDown () {
	// Lock the cursor
	Cursor.lockState = CursorLockMode.Confined;
}

function Update () {
	// In standalone player we have to provide our own key
	// input for unlocking the cursor	
	if(Input.GetMouseButtonDown(0)){
		if (Cursor.lockState != CursorLockMode.Confined || Cursor.lockState != CursorLockMode.Locked){
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
	else if (Input.GetKeyDown ("escape") || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)){
		Cursor.lockState =  CursorLockMode.None;
		Cursor.visible = true;
	}
}
