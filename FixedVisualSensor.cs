using RAIN.Entities.Aspects;
using RAIN.Perception.Sensors;
using RAIN.Serialization;

[RAINSerializableClass]
public class FixedVisualSensor : VisualSensor
{
	public override void Sense(string aAspectName, MatchType aMatchType)
	{
		if (aMatchType == MatchType.BEST)
		{
			base.Sense(aAspectName, MatchType.ALL);

			float tBestScore = float.MaxValue;
			RAINAspect tBestAspect = null;

			for (int i = 0; i < _matches.Count; i++)
			{
				float tScore = Score(_matches[i]);
				if (tScore < tBestScore)
				{
					tBestScore = tScore;
					tBestAspect = _matches[i];
				}
			}

			_matches.Clear();
			if (tBestAspect != null)
				_matches.Add(tBestAspect);
		}
		else
			base.Sense(aAspectName, aMatchType);
	}
}