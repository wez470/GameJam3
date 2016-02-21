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

	private bool evenPlayersTurrets = true;
	private bool[] playersAlive = new bool[4];

	void CreatePlayers() {
		for (int i = 1; i <= XCI.GetNumPluggedCtrlrs (); i++) {
			CreatePlayer(i);
		}
		for (int i = 0; i < 4; i++) {
			playersAlive[i] = true;
		}
		evenPlayersTurrets = !evenPlayersTurrets;
	}

	void Awake() {
		DontDestroyOnLoad(this.gameObject);
		CreatePlayers();
	}

	private void CreatePlayer(int playerNum) {
		if (evenPlayersTurrets) {
			if (playerNum % 2 == 0) { // Even players are turrets.
				GameObject newTurret = Instantiate(Turret) as GameObject;
				newTurret.transform.position = getPlayerPosition(playerNum);
				newTurret.tag = "Turret";
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
		else {
			if (playerNum % 2 == 0) { // Even players are humans.
				GameObject newPlayer = Instantiate(Player) as GameObject;
				newPlayer.transform.position = getPlayerPosition(playerNum);
				newPlayer.tag = "Player";
				Player player = newPlayer.gameObject.GetComponent<Player>();
				player.SetPlayerNum(playerNum);
			}
			else {
				GameObject newTurret = Instantiate(Turret) as GameObject;
				newTurret.transform.position = getPlayerPosition(playerNum);
				newTurret.tag = "Turret";
				Turret turret = newTurret.gameObject.GetComponent<Turret>();
				turret.SetPlayerNum(playerNum);
			}
		}
	}

	private Vector3 getPlayerPosition(int playerNum){
		if (evenPlayersTurrets) {
			switch (playerNum) {
			case (1): {return Player1Spawn.position;}
			case (2): {return Turret1Spawn.position;}
			case (3): {return Player2Spawn.position;}
			case (4): {return Turret2Spawn.position;}
			default: {return Vector3.zero;}
			}
		}
		else {
			switch (playerNum) {
			case (1): {return Turret1Spawn.position;}
			case (2): {return Player1Spawn.position;}
			case (3): {return Turret2Spawn.position;}
			case (4): {return Player2Spawn.position;}
			default: {return Vector3.zero;}
			}
		}
	}

	public void PlayerDied(int playerNum) {
		playersAlive[playerNum - 1] = false;

		// players 1 and 3 are dead
		if (!playersAlive[0] && !playersAlive[2]) {
			if (evenPlayersTurrets) {
				//ROBOTS WIN
			}
			else {
				//HUMANS WIN
			}
		}
		else if (!playersAlive[1] && !playersAlive[3]) { // players 2 and 4 are dead
			if (evenPlayersTurrets) {
				//HUMANS WIN
			}
			else {
				//ROBOTS WIN
			}
		}
	}
}
