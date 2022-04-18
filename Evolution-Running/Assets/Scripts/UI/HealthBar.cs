using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Slider slider;
	public Gradient gradient;
	public Image fill;
	public Image heart;
	public Animation heartJumpAnimation;
	public ParticleSystem particleSystem;

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

	public void SetHealth(int health)
	{
		slider.value = health;
		heartJumpAnimation.Play();
		particleSystem.Play();
		particleSystem.transform.position -= new Vector3(0, 0, 1);
		if (health == 0)
		{
			heart.color = Color.black;
		}

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
