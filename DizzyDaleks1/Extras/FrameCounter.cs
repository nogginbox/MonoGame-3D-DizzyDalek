using System;
using System.Linq;
using System.Collections.Generic;

namespace DizzyDaleks1
{
	public class FrameCounter
	{
		public FrameCounter()
		{
		}

		public long TotalFrames { get; private set; }
		public float TotalSeconds { get; private set; }
		public float AverageFramesPerSecond { get; private set; }
		public float CurrentFramesPerSecond { get; private set; }

		public const int MAXIMUM_SAMPLES = 100;

		private Queue<float> _sampleBuffer = new Queue<float>();

		public bool Update(double deltaTime)
		{
			CurrentFramesPerSecond = 1.0f / (float)deltaTime;

			_sampleBuffer.Enqueue(CurrentFramesPerSecond);

			if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
			{
				_sampleBuffer.Dequeue();
				AverageFramesPerSecond = _sampleBuffer.Average(i => i);
			} 
			else
			{
				AverageFramesPerSecond = CurrentFramesPerSecond;
			}

			TotalFrames++;
			TotalSeconds += (float)deltaTime;
			return true;
		}
	}
}

