using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheTuringTournament
{
    

    class Plane
    {
        GraphicsDeviceManager graphicsDevice;

        VertexPositionTexture[] planeVerts;
        public Texture2D texture;
        BasicEffect effect;

        public Plane(GraphicsDeviceManager graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            effect = new BasicEffect(graphicsDevice.GraphicsDevice);
            planeVerts = new VertexPositionTexture[6];
        }

        public void setVerts(VertexPositionTexture[] verts)
        {
            planeVerts[0].Position = verts[0].Position;
            planeVerts[1].Position = verts[1].Position;
            planeVerts[2].Position = verts[2].Position;
            planeVerts[4].Position = verts[4].Position;
            planeVerts[3].Position = verts[3].Position;
            planeVerts[5].Position = verts[5].Position;
            // Mapping the texture
            planeVerts[0].TextureCoordinate = verts[0].TextureCoordinate;
            planeVerts[1].TextureCoordinate = verts[1].TextureCoordinate;
            planeVerts[2].TextureCoordinate = verts[2].TextureCoordinate;
            planeVerts[3].TextureCoordinate = verts[3].TextureCoordinate;
            planeVerts[4].TextureCoordinate = verts[4].TextureCoordinate;
            planeVerts[5].TextureCoordinate = verts[5].TextureCoordinate;
            //planeVerts = verts;
        }

        

        public void Draw(Matrix view, Matrix projection)
        {
            effect.World = Matrix.Identity;
            effect.View = view;
            effect.Projection = projection;

            effect.TextureEnabled = true;
            effect.Texture = texture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphicsDevice.GraphicsDevice.DrawUserPrimitives(
                    // We’ll be rendering two trinalges
                    PrimitiveType.TriangleList,
                    // The array of verts that we want to render
                    planeVerts,
                    // The offset, which is 0 since we want to start 
                    // at the beginning of the floorVerts array
                    0,
                    // The number of triangles to draw
                    2);
            }

        }


    }
}
