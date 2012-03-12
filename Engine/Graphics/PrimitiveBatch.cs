#region File Description
//-----------------------------------------------------------------------------
// PrimitiveBatch.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Engine.Graphics
{

	// PrimitiveBatch is a class that handles efficient rendering automatically for its
	// users, in a similar way to SpriteBatch. PrimitiveBatch can render lines, points,
	// and triangles to the screen. In this sample, it is used to draw a spacewars
	// retro scene.
	public class PrimitiveBatch : IDisposable
	{
		#region Constants and Fields

		// this constant controls how large the vertices buffer is. Larger buffers will
		// require flushing less often, which can increase performance. However, having
		// buffer that is unnecessarily large will waste memory.
		const int DefaultBufferSize = 500;

		// a block of vertices that calling AddVertex will fill. Flush will draw using
		// this array, and will determine how many primitives to draw from
		// positionInBuffer.
		VertexPositionColor[] vertices = new VertexPositionColor[DefaultBufferSize];

		// keeps track of how many vertices have been added. this value increases until
		// we run out of space in the buffer, at which time Flush is automatically
		// called.
		int positionInBuffer = 0;

		// a basic effect, which contains the shaders that we will use to draw our
		// primitives.
		BasicEffect basicEffect;

		// the device that we will issue draw calls to.
		GraphicsDevice device;

		// this value is set by Begin, and is the type of primitives that we are
		// drawing.
		PrimitiveType primitiveType;

		// how many verts does each of these primitives take up? points are 1,
		// lines are 2, and triangles are 3.
		int numVertsPerPrimitive;

		// how many verts are needed for the first primitive? This is relevant for 
		// strips; for lines it's 2 (numVertsPerPrimitive = 1), for triangles it's 3
		// (numVertsPerPrimitive = 1)
		int numInitialVertices;

		// hasBegun is flipped to true once Begin is called, and is used to make
		// sure users don't call End before Begin is called.
		bool hasBegun = false;

		bool isDisposed = false;

		#endregion

		// the constructor creates a new PrimitiveBatch and sets up all of the internals
		// that PrimitiveBatch will need.
		public PrimitiveBatch(GraphicsDevice graphicsDevice)
		{
			if (graphicsDevice == null)
			{
				throw new ArgumentNullException("graphicsDevice");
			}
			device = graphicsDevice;

			// set up a new basic effect, and enable vertex colors.
			basicEffect = new BasicEffect(graphicsDevice);
			basicEffect.VertexColorEnabled = true;

			// projection uses CreateOrthographicOffCenter to create 2d projection
			// matrix with 0,0 in the upper left.
			basicEffect.Projection = Matrix.CreateOrthographicOffCenter
				(0, graphicsDevice.Viewport.Width,
				graphicsDevice.Viewport.Height, 0,
				0, 1);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !isDisposed)
			{
				if (basicEffect != null)
					basicEffect.Dispose();

				isDisposed = true;
			}
		}

		// Begin is called to tell the PrimitiveBatch what kind of primitives will be
		// drawn, and to prepare the graphics card to render those primitives.
		public void Begin(PrimitiveType primitiveType)
		{
			if (hasBegun)
			{
				throw new InvalidOperationException
					("End must be called before Begin can be called again.");
			}

			this.primitiveType = primitiveType;

			// how many verts will each of these primitives require?
			this.numVertsPerPrimitive = NumVertsPerPrimitive(primitiveType);

			// how many verts will the first primitive require?
			this.numInitialVertices = NumInitialVertices(primitiveType);

			// tell our basic effect to begin.
			basicEffect.CurrentTechnique.Passes[0].Apply();

			// flip the error checking boolean. It's now ok to call AddVertex, Flush,
			// and End.
			hasBegun = true;
		}

		// AddVertex is called to add another vertex to be rendered. To draw a point,
		// AddVertex must be called once. for lines, twice, and for triangles 3 times.
		// If strips are used, each call to AddVertex after drawing the initial primitive
		// draws another primitive. this function can only be called once begin has been called.
		// if there is not enough room in the vertices buffer, Flush is called automatically.
		public void AddVertex(Vector2 vertex, Color color)
		{
			VertexPositionColor vertexPositionColor = new VertexPositionColor(
				new Vector3(vertex, 0),
				color);

			AddVertex(vertexPositionColor);
		}

		// AddVertex is called to add another vertex to be rendered. To draw a point,
		// AddVertex must be called once. for lines, twice, and for triangles 3 times.
		// If strips are used, each call to AddVertex after drawing the initial primitive
		// draws another primitive. this function can only be called once begin has been called.
		// if there is not enough room in the vertices buffer, Flush is called automatically.
		public void AddVertex(VertexPositionColor vertex)
		{
			if (!hasBegun)
			{
				throw new InvalidOperationException
					("Begin must be called before AddVertex can be called.");
			}

			// are we starting a new primitive? if so, and there will not be enough room
			// for a whole primitive, flush.
			bool newPrimitive = ((positionInBuffer % numVertsPerPrimitive) == 0);

			if (newPrimitive &&
				(positionInBuffer + numVertsPerPrimitive) >= vertices.Length)
			{
				Flush();
			}

			// once we know there's enough room, set the vertex in the buffer,
			// and increase position.
			vertices[positionInBuffer] = vertex;

			positionInBuffer++;
		}

		// End is called once all the primitives have been drawn using AddVertex.
		// it will call Flush to actually submit the draw call to the graphics card, and
		// then tell the basic effect to end.
		public void End()
		{
			if (!hasBegun)
			{
				throw new InvalidOperationException
					("Begin must be called before End can be called.");
			}

			// draw whatever the user wanted us to draw
			Flush();

			// we need to clear the buffer manually here if we have used strips,
			// because the call to Flush might have copied over some starting values
			positionInBuffer = 0;

			hasBegun = false;
		}

		// Flush is called to issue the draw call to the graphics card. Once the draw
		// call is made, positionInBuffer is reset, so that AddVertex can start over
		// at the beginning. End will call this to draw the primitives that the user
		// requested, and AddVertex will call this if there is not enough room in the
		// buffer.
		private void Flush()
		{
			if (!hasBegun)
			{
				throw new InvalidOperationException
					("Begin must be called before Flush can be called.");
			}

			// no work to do
			if (positionInBuffer == 0)
			{
				return;
			}

			// how many primitives will we draw?
			// this takes into account that the first primitive 
			// probably needs a different number of vertices
			int primitiveCount = (positionInBuffer - numInitialVertices) / numVertsPerPrimitive + 1;

			// submit the draw call to the graphics card
			device.DrawUserPrimitives<VertexPositionColor>(primitiveType, vertices, 0, primitiveCount);

			// for strips we need to copy over some previous values to the beginning of the buffer
			switch (primitiveType)
			{
				case PrimitiveType.LineStrip:
					// we store the last two vertices as new starting line					
					vertices[0] = vertices[positionInBuffer - 2];
					vertices[1] = vertices[positionInBuffer - 1];

					// start at the next "free" position
					positionInBuffer = 2;
					break;
				case PrimitiveType.TriangleStrip:
					// we store the last three vertices as new starting triangle
					vertices[0] = vertices[positionInBuffer - 3];
					vertices[1] = vertices[positionInBuffer - 2];
					vertices[2] = vertices[positionInBuffer - 1];

					// start at the next "free" position
					positionInBuffer = 3;
					break;
				default:
					// it's ok to reset positionInBuffer back to zero,
					// and write over any vertices that may have been set previously.
					positionInBuffer = 0;
					break;
			}
		}

		#region Helper functions

		// NumVertsPerPrimitive is a boring helper function that tells how many vertices
		// it will take to draw each kind of primitive.
		static private int NumVertsPerPrimitive(PrimitiveType primitive)
		{
			int numVertsPerPrimitive;
			switch (primitive)
			{
				case PrimitiveType.LineList:
					numVertsPerPrimitive = 2;
					break;
				case PrimitiveType.TriangleList:
					numVertsPerPrimitive = 3;
					break;
				case PrimitiveType.LineStrip:
				case PrimitiveType.TriangleStrip:
					numVertsPerPrimitive = 1;
					break;
				default:
					throw new InvalidOperationException("primitive type is not valid");
			}
			return numVertsPerPrimitive;
		}

		// NumInitialVertices is a boring helper function that tells how many vertices
		// it will take to draw the first primitive of a certain type
		static private int NumInitialVertices(PrimitiveType primitiveType)
		{
			int result;
			switch (primitiveType)
			{
				case PrimitiveType.LineList:
				case PrimitiveType.LineStrip:
					result = 2;
					break;
				case PrimitiveType.TriangleList:
				case PrimitiveType.TriangleStrip:
					result = 3;
					break;
				default:
					throw new InvalidOperationException("primitive type is not valid");
			}

			return result;
		}

		#endregion
	}
}
