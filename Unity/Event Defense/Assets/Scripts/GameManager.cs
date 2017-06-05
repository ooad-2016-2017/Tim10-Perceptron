﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus {
	Next, Play, Gameover, Win
}

public class GameManager : Singleton<GameManager> {
	
	[SerializeField]
	private GameObject _spawnPoint;

	[SerializeField]
	private GameObject[] _enemies;

	[SerializeField]
	private int _totalEnemies = 3;

	[SerializeField]
	private int _enemiesPerSpawn;

	[SerializeField]
	private float _spawnDelay;

	[SerializeField]
	private int _totalWaves = 10;

	[SerializeField]
	private Text _totalMoneyLabel;

	[SerializeField]
	private Text _currentWaveLabel;

	[SerializeField]
	private Text _totalEscapedLabel;

	[SerializeField]
	private Text _gameStatusLabel;

	[SerializeField]
	private Button _gameStatusButton;

	private int _enemiesOnScreen = 0;
	private int _waveNumber = 0;
	private int _totalMoney = 10;
	private int _totalEscaped = 0;
	private int _roundEscaped = 0;
	private int _totalKilled = 0;
	private int _enemiesToSpawn = 8;
	private GameStatus _currentState = GameStatus.Play;

	[HideInInspector]
	public List<Enemy> Enemies = new List<Enemy> ();

	public int TotalMoney{
		get{
			return _totalMoney;
		}
		set{
			_totalMoney = value;
			_totalMoneyLabel.text = _totalMoney.ToString();
		}
	}

	public int TotalEscaped{
		get{ return _totalEscaped; }
		set{ _totalEscaped = value; }
	}

	public int RoundEscaped{
		get{ return _roundEscaped; }
		set{ _roundEscaped = value; }
	}

	public int TotalKilled{
		get{ return _totalKilled; }
		set{ _totalKilled = value; }
	}

	void Start () {
		//sakrij NextWave button
		_gameStatusButton.gameObject.SetActive(false);
		ShowMenu ();
	}

	void Update(){
		HandleEscape ();
	}

	IEnumerator SpawnEnemy(){
		if (_enemiesPerSpawn > 0 && Enemies.Count < _totalEnemies) {	
			for (int i = 0; i < _enemiesPerSpawn; i++) {
				if (Enemies.Count < _totalEnemies) {					
					GameObject newEnemy = Instantiate (_enemies [0]) as GameObject;
					newEnemy.transform.position = _spawnPoint.transform.position;
				}
			}
			yield return new WaitForSeconds(_spawnDelay);
			StartCoroutine (SpawnEnemy());
		}
	}

	public void RegisterEnemy(Enemy enemy){
		Enemies.Add (enemy);
	}
		
	public void UnregisterEnemy(Enemy enemy){
		Enemies.Remove (enemy);
		Destroy (enemy.gameObject);
		IsWaveOver ();
	}
		
	public void DestroyAllEnemies(){
		foreach (Enemy enemy in Enemies) {
			Destroy (enemy.gameObject);
		}
		Enemies.Clear ();
	}

	public void AddMoney(int amount){
		TotalMoney += amount;
	}

	public void SubtractMoney(int amount){
		TotalMoney -= amount;
	}

	public void IsWaveOver(){
		_totalEscapedLabel.text = "Escaped " + _totalEscaped + "/10";
		if ((_roundEscaped + TotalKilled) == _totalEnemies) {
			SetCurrentGameState ();
			ShowMenu ();
		}
	}

	public void SetCurrentGameState(){
		if (_totalEscaped >= 10) {
			_currentState = GameStatus.Gameover;
		} else if (_waveNumber == 0 && (TotalKilled + RoundEscaped) == 0) {
			_currentState = GameStatus.Play;
		} else if (_waveNumber >= _totalWaves) {
			_currentState = GameStatus.Win;
		} else {
			_currentState = GameStatus.Next;
		}
	}

	public void ShowMenu(){
		switch (_currentState) {
		case GameStatus.Gameover:
			_gameStatusLabel.text = "Play Again!";
			break;
		case GameStatus.Next:
			_gameStatusLabel.text = "Next Wave";
			break;
		case GameStatus.Play:
			_gameStatusLabel.text = "Play";
			break;
		case GameStatus.Win:
			_gameStatusLabel.text = "Play";
			break;
		}
		_gameStatusButton.gameObject.SetActive (true);
	}

	public void PlayButtonPressed(){
		switch(_currentState){
		case GameStatus.Next:
			_waveNumber++;
			_totalEnemies += _waveNumber;
			break;
		default:
			_totalEnemies = 3;
			TotalEscaped = 0;
			TotalMoney = 10;
			_totalMoneyLabel.text = TotalMoney.ToString ();
			TowerManager.Instance.DestroyAllTowers();
			TowerManager.Instance.RenameTagsBuildSites();
			_totalEscapedLabel.text = "Escaped " + TotalEscaped + "/10";
			TotalEscaped = 0;
			break;
		}
		DestroyAllEnemies ();
		TotalKilled = 0;
		_roundEscaped = 0;
		_currentWaveLabel.text = "Wave " + (_waveNumber + 1);
		StartCoroutine (SpawnEnemy ());
		_gameStatusButton.gameObject.SetActive (false);
	}

	private void HandleEscape(){
		if (Input.GetMouseButton (1) || Input.GetKeyDown (KeyCode.Escape)) {
			TowerManager.Instance.DisableDragSprite ();
			TowerManager.Instance.TowerBtnPressed = null;
		}
	}

}