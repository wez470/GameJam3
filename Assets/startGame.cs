using UnityEngine;
using XboxCtrlrInput;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    public AudioSource buttonSound;
	public Animator MenuAnimator;


    void Update()
	{

		for (int i = 1; i <= XCI.GetNumPluggedCtrlrs(); i++)
		{
			if (XCI.GetButtonDown(XboxButton.Start, i))
			{
				buttonSound.Play ();
				MenuAnimator.SetBool("canplayMenuAnimation", true);
				Invoke ("startGameNow", 1.4f);
			}
		}


		if (Input.GetKeyDown ("return")) {
			buttonSound.Play ();
			MenuAnimator.SetBool("canplayMenuAnimation", true);
			Invoke ("startGameNow", 1.4f);

		}
	}

	void startGameNow(){
			SceneManager.LoadScene(1);
	}


       /* 
    }*/

}

