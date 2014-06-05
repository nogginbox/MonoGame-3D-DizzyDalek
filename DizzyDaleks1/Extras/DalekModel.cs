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
		private float _angle;
		private readonly int _meshCount;
		private readonly Matrix[] _meshRotations;
		private readonly Model _model;
		private readonly Texture2D _texture;
		private Matrix _world;

		public DalekModel(Model model, Texture2D texture)
		{
			_meshCount = model.Meshes.Count;
			_model = model;
			_texture = texture;
			_world = Matrix.Identity;

			_meshRotations = new Matrix[_meshCount];
			for (var i = 0; i < _meshCount; i++)
			{
				_meshRotations[i] = Matrix.Identity;
			}
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

					// Textures
					effect.TextureEnabled = true;
					effect.Texture = _texture;

					effect.World =  transforms[_model.Meshes[i].ParentBone.Index] * _meshRotations[i] * _world;

					// Lighting
					effect.DirectionalLight0.Direction = new Vector3(-2,0,-1);
					effect.DirectionalLight0.DiffuseColor = Color.Red.ToVector3 ();

					camera.Display(effect);
				}
				_model.Meshes[i].Draw();
			}
		}

		public void Update(GameTime gameTime)
		{
			var modelAngleDelta = (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f;
			_angle += modelAngleDelta;

			_world = Matrix.CreateTranslation(new Vector3(0, 0, 4))
				* Matrix.CreateRotationY(_angle);

			for (var i = 0; i < _meshCount; i++)
			{
				_meshRotations[i] *= Matrix.CreateRotationY(-modelAngleDelta * i);
			}
		}
	}
}
