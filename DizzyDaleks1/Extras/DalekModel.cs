using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Dalek Model by: Benjee10
// Source: http://www.blendswap.com/user/Benjee10/blends

namespace DizzyDaleks1.Extras
{
	public class DalekModel
	{
		private readonly int _meshCount;
		private readonly Model _model;
		private Matrix _world;

		public DalekModel(Model model)
		{
			_meshCount = model.Meshes.Count;
			_model = model;
			_world = Matrix.Identity;
		}

		public void Draw(Camera camera)
		{
			// Copy any parent transforms.
			var transforms = new Matrix[_model.Bones.Count];
			_model.CopyAbsoluteBoneTransformsTo(transforms);

			for (var i = 0; i < _meshCount; i++)
			{
				foreach (var meshEffect in _model.Meshes[i].Effects)
				{
					var effect = (BasicEffect)meshEffect;
					effect.EnableDefaultLighting();

					effect.World = transforms[_model.Meshes[i].ParentBone.Index] * _world;

					camera.Display(effect);
				}
				_model.Meshes[i].Draw();
			}
		}

		public void Update(GameTime gameTime)
		{
			var modelAngleDelta = (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f;
			_world *= Matrix.CreateRotationY(modelAngleDelta);
		}
	}
}
