//--------------------------------------------------------------------
//  ___________  ___  ___                  _____                      
// |____ |  _  \ |  \/  |                 |  __ \                     
//     / / | | | | .  . | ___  _ __   ___ | |  \/ __ _ _ __ ___   ___ 
//     \ \ | | | | |\/| |/ _ \| '_ \ / _ \| | __ / _` | '_ ` _ \ / _ \
// .___/ / |/ /  | |  | | (_) | | | | (_) | |_\ \ (_| | | | | | |  __/
// \____/|___/   \_|  |_/\___/|_| |_|\___/ \____/\__,_|_| |_| |_|\___|
//
//--------------------------------------------------------------------
using DizzyDaleks1.Extras;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;


namespace DizzyDaleks1
{

	public class DalekGame : Game
	{
		GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
		Camera _camera;

		// Content
		Texture2D _dragonTexture;
		TexturePlane _slide;
		//DalekModel _dalek;

		public DalekGame()
		{
			_graphics = new GraphicsDeviceManager(this)
			{
				SupportedOrientations = DisplayOrientation.LandscapeLeft
					| DisplayOrientation.LandscapeRight
			};
			
			Content.RootDirectory = "Content";

			_graphics.IsFullScreen = true;
		}

		protected override void Initialize ()
		{
			_camera = new Camera(new Vector3(0, 15, 15),
				Vector3.Zero,
				Vector3.Up,
				Window);
			base.Initialize ();
		}

		protected override void LoadContent ()
		{
			_spriteBatch = new SpriteBatch (_graphics.GraphicsDevice);

			_dragonTexture = Content.Load<Texture2D> ("Dragon");

			var slideTexture = Content.Load<Texture2D> ("Slide");
			var scaleSlide = Matrix.CreateScale (0.05f);
			_slide = new TexturePlane (_graphics.GraphicsDevice, slideTexture, scaleSlide);
		}

		protected override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
		}

		protected override void Draw (GameTime gameTime)
		{
			_graphics.GraphicsDevice.Clear (Color.DarkGreen);

			_spriteBatch.Begin ();
			_spriteBatch.Draw (_dragonTexture, new Vector2 (100, 100), Color.White);
			_spriteBatch.End ();

			_slide.Draw (_camera);

			base.Draw (gameTime);
		}
	}
}

/* Useful 3D Graphics Device reset code **

// Reset for 3D drawing
GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
GraphicsDevice.BlendState = BlendState.Opaque;
GraphicsDevice.DepthStencilState = DepthStencilState.Default;

 */