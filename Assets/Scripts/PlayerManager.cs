using UnityEngine;
using XboxCtrlrInput;
using System.Collections;
using System;

public class PlayerManager : MonoBehaviour {
	public GameObject Player;
	public GameObject Turret;
	public Transform Player1Spawn;
	public Transform Player2Spawn;
	public Transform Turret1Spawn;
	public Transform Turret2Spawn;

	void Awake() {
		for(int i = 1; i <= XCI.GetNumPluggedCtrlrs(); i++) {
			CreatePlayer(i);
		}
	}

	private void CreatePlayer(int playerNum) {
		if (playerNum % 2 == 0) { // Even players are turrets.
			GameObject newTurret = Instantiate(Turret) as GameObject;
			newTurret.transform.position = getPlayerPosition(playerNum);
			Turret turret = newTurret.gameObject.GetComponent<Turret>();
			turret.SetPlayerNum(playerNum);
		}
		else {
			GameObject newPlayer = Instantiate(Player) as GameObject;
			newPlayer.transform.position = getPlayerPosition(playerNum);
			newPlayer.tag = "Player";
			Player player = newPlayer.gameObject.GetComponent<Player>();
			player.SetPlayerNum(playerNum);
		}
	}

	private Vector3 getPlayerPosition(int playerNum){
		switch (playerNum) {
		case (1): {return Player1Spawn.position;}
		case (2): {return Turret1Spawn.position;}
		case (3): {return Player2Spawn.position;}
		case (4): {return Turret2Spawn.position;}
		default: {return Vector3.zero;}
		}
	}
}
