using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DizzyDaleks1.Extras
{
	public class Camera
	{
		public Matrix View { get; private set; }
		public Matrix Projection { get; private set; }

		private Vector3 _position;
		private readonly Vector3 _positionTarget;
		private readonly Vector3 _up;

		private const int FarPlaneDistance = 1000;

		public Camera(Vector3 position, Vector3 target, Vector3 up, GameWindow window)
		{
			_position = position;
			_positionTarget = target;
			_up = up;

			View = Matrix.CreateLookAt(_position, _positionTarget, _up);
			SetProjectionMatrix(window.ClientBounds);
		}

		public void Display(BasicEffect effect)
		{
			effect.View = View;
			effect.Projection = Projection;
		}

		private void SetProjectionMatrix(Rectangle screen)
		{
			var screenAspect = screen.Width / (float)screen.Height;
			Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, screenAspect, 1, FarPlaneDistance);
		}

	}
}