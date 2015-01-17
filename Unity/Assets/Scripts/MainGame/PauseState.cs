using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PauseState : MonoBehaviour {
	private interface IPausable {
		void Pause();

		void Resume();
	}
	
	private class PausableRidgidBody2D : IPausable {
		private Rigidbody2D target;
		private Vector2 velocity;
		private float angularVelocity;
		
		public PausableRidgidBody2D(Rigidbody2D target) {
			this.target = target;
		}
		
		public void Pause() {
			if (!target.IsSleeping()) {
				velocity = target.velocity;
				angularVelocity = target.angularVelocity;
				target.Sleep();
			}
		}
		
		public void Resume() {
			if (target.IsSleeping()) {
				target.WakeUp();
				target.velocity = velocity;
				target.angularVelocity = angularVelocity;
			}
		}
	}

	private class PausableBehaviour : IPausable {
		private Behaviour target;
		private Animation animation;
		private PausableRidgidBody2D ridgid;

		public PausableBehaviour(Behaviour target) {
			this.target = target;
			animation = target.gameObject.animation;
			if (target.gameObject.rigidbody2D != null) {
				ridgid = new PausableRidgidBody2D(target.gameObject.rigidbody2D);
			}
		}

		public void Pause() {
			if (target.enabled) {
				target.enabled = false;
				if (ridgid != null) {
					ridgid.Pause();
				}
				if (animation != null) {
					animation.Stop();
				}
			}
		}

		public void Resume() {
			if (!target.enabled) {
				target.enabled = true;
				if (ridgid != null) {
					ridgid.Resume();
				}
				if (animation != null) {
					animation.Play();
				}
			}
		}
	}

	public bool paused { get; private set; }
	private List<IPausable> pauseObjects;

	void PauseSwitch() {
		if (paused) {
			Resume();
		} else {
			Pause();
		}
	}

	public void Pause() {
		if (paused) {
			return;
		}

		var behaviour = UnityEngine.Object.FindObjectsOfType<Player>().Where(b => b.enabled).Select<Player, IPausable>(b => new PausableBehaviour(b));
		var items = UnityEngine.Object.FindObjectsOfType<Item>().Where(b => b.enabled).Select<Item, IPausable>(b => new PausableBehaviour(b));
		var gameSystem = new PausableBehaviour(UnityEngine.Object.FindObjectOfType<CreateItems>());

		pauseObjects = behaviour.Concat(items).Concat(new [] { gameSystem }).ToList();
		pauseObjects.ForEach(p => p.Pause());
		paused = true;
	}

	public void Resume() {
		if (!paused) {
			return;
		}

		pauseObjects.ForEach(p => p.Resume());
		pauseObjects = null;
		paused = false;
	}
}
