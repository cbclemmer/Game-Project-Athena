using System;
using System.Collections;
using System.Collections.Generic;
using PolyNav;
using UnityEngine;

public class Pokemon_Controller : MonoBehaviour
{
	[Serializable]
	public class PokemonSettings
	{
		public float AttackRange;
		public float BaseSpeed;
		public float Distance;
	}

	[Serializable]
	public class PokemonStats
	{
		public int HitPoints;
		public int Attack;
		public int Defense;
		public int SpAttack;
		public int SpDefence;
		public int Speed;
	}

	[SerializeField] public PokemonSettings Settings = new PokemonSettings();
	[SerializeField] public PokemonStats Stats = new PokemonStats();

	public Transform Target;
	public Vector2 Destination;
	public bool Attacking;
	public List<Move_Controller> Moves;

	private float DistanceToTarget
	{ get { return Target == null ? int.MaxValue : (transform.position - Target.transform.position).magnitude; } }

	private DateTime _nextAttack = DateTime.Now;
	private int _nextMove;

	void Update()
	{
		if (Destination != null)
		{
			Destination = Attacking 
				? Target.position
				: ((transform.position - Target.position) * Settings.Distance) - Target.position;
			GetComponent<PolyNavAgent>().SetDestination(Destination);
		}

		if (_nextAttack < DateTime.Now && !Attacking)
		{
			_nextAttack = DateTime.Now.AddMilliseconds(Stats.Speed * Settings.BaseSpeed);
			Attacking = true;
			GetComponent<PolyNavAgent>().maxSpeed *= 2;
		}

		if (Attacking && DistanceToTarget < Settings.AttackRange)
		{
			Moves[_nextMove].Execute();

			GetComponent<PolyNavAgent>().maxSpeed /= 2;
			Attacking = false;
			_nextMove++;
			if (_nextMove >= Moves.Count) _nextMove = 0;
		}
	}
}
