using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DizzyDaleks1.Extras
{
	public class TexturePlane
	{
		private readonly GraphicsDevice _graphicsDevice;

		// Vertex data
		private readonly VertexPositionTexture[] _verts;
		private readonly VertexBuffer _vertexBuffer;

		// Effect
		private readonly BasicEffect _effect;

		private readonly Matrix _world;

		// Texture info
		private readonly Texture2D _texture;

		public TexturePlane(GraphicsDevice graphicsDevice, Texture2D texture, Matrix? world = null)
		{
			_graphicsDevice = graphicsDevice;
			_texture = texture;
		    _world = world ?? Matrix.Identity;

			var halfWidth = texture.Width / 2;

			// Initialize vertices
			_verts = new VertexPositionTexture[4];
			_verts[0] = new VertexPositionTexture(new Vector3(-halfWidth, 0, halfWidth), new Vector2(0, 1));
			_verts[1] = new VertexPositionTexture(new Vector3(-halfWidth, 0, -halfWidth), new Vector2(0, 0));
			_verts[2] = new VertexPositionTexture(new Vector3(halfWidth,  0, halfWidth), new Vector2(1, 1));
			_verts[3] = new VertexPositionTexture(new Vector3(halfWidth,  0, -halfWidth), new Vector2(1, 0));

			// Set vertex data in VertexBuffer
			_vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionTexture), _verts.Length, BufferUsage.None);
			_vertexBuffer.SetData(_verts);

			// Initialize the BasicEffect
			_effect = new BasicEffect(graphicsDevice);
		}

		public void Draw(Camera camera)
		{
			// Set the vertex buffer on the GraphicsDevice
			_graphicsDevice.SetVertexBuffer(_vertexBuffer);

			camera.Display (_effect);
			_effect.World = _world;

			// Add texture
			_effect.Texture = _texture;
			_effect.TextureEnabled = true;

			_effect.CurrentTechnique.Passes[0].Apply();
			_graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, _verts, 0, 2);
		}
	}
}
