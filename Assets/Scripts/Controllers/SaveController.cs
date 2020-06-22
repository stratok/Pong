using UnityEngine;

public class SaveController : MonoBehaviour
{
	private int m_MaxSingleScore;
	private Color m_BallColor;

	public int MaxSingleScore 
	{ 
		get => m_MaxSingleScore;
		set
		{
			m_MaxSingleScore = value;
			PlayerPrefs.SetInt("MaxSingleScore", m_MaxSingleScore);
		}
	}

	public Color BallColor
	{
		get => m_BallColor;
		set
		{
			m_BallColor = value;
			SaveColor(m_BallColor);
		}
	}

	public void Awake()
	{
		m_MaxSingleScore = PlayerPrefs.GetInt("MaxSingleScore", 0);
		m_BallColor = RestoreColor();
	}

	private void SaveColor(Color color) 
	{
		PlayerPrefs.SetFloat("ColorR", color.r);
		PlayerPrefs.SetFloat("ColorG", color.g);
		PlayerPrefs.SetFloat("ColorB", color.b);
		PlayerPrefs.SetFloat("ColorA", color.a);
	}

	private Color RestoreColor() 
	{
		var color = new Color
		{
			r = PlayerPrefs.GetFloat("ColorR", 1),
			g = PlayerPrefs.GetFloat("ColorG", 1),
			b = PlayerPrefs.GetFloat("ColorB", 1),
			a = PlayerPrefs.GetFloat("ColorA", 1)
		};

		return color;
	}
}
