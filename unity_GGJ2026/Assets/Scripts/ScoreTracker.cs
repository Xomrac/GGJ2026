using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

namespace DefaultNamespace
{

	public enum Scores
	{
		S,
		A,
		B,
		C,
		D,
		F
	}
	public static class ScoreTracker
	{
		public static List<Scores> _scores = new();
		public static Scores GetScoreForPercentage(float percentage)
		{
			if (percentage >= 0.9f) return Scores.S;
			if (percentage >= 0.8f) return Scores.A;
			if (percentage >= 0.7f) return Scores.B;
			if (percentage >= 0.6f) return Scores.C;
			if (percentage >= 0.5f) return Scores.D;
			return Scores.F;
		}

		public static void AddScore(float score)
		{
			_scores.Add(GetScoreForPercentage(score));
		}
		
	}

}