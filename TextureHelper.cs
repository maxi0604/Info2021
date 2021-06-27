using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Info2021
{
    static class TextureHelper
    {
        // https://gamedev.stackexchange.com/questions/11584/xna-splitting-one-large-texture-into-an-array-of-smaller-textures
        public static Texture2D[] Split(Texture2D original, int partWidth, int partHeight, out int xCount, out int yCount)
        {
            yCount = original.Height / partHeight + (partHeight % original.Height == 0 ? 0 : 1);
            xCount = original.Height / partHeight + (partHeight % original.Height == 0 ? 0 : 1);
            Texture2D[] r = new Texture2D[xCount * yCount];//Number of parts = (area of original) / (area of each part).
            int dataPerPart = partWidth * partHeight;//Number of pixels in each of the split parts

            //Get the pixel data from the original texture:
            Color[] originalData = new Color[original.Width * original.Height];
            original.GetData<Color>(originalData);

            int index = 0;
            for (int y = 0; y < yCount * partHeight; y += partHeight)
                for (int x = 0; x < xCount * partWidth; x += partWidth)
                {
                    //The texture at coordinate {x, y} from the top-left of the original texture
                    Texture2D part = new Texture2D(original.GraphicsDevice, partWidth, partHeight);
                    //The data for part
                    Color[] partData = new Color[dataPerPart];

                    //Fill the part data with colors from the original texture
                    for (int py = 0; py < partHeight; py++)
                        for (int px = 0; px < partWidth; px++)
                        {
                            int partIndex = px + py * partWidth;
                            //If a part goes outside of the source texture, then fill the overlapping part with Color.Transparent
                            if (y + py >= original.Height || x + px >= original.Width)
                                partData[partIndex] = Color.Transparent;
                            else
                                partData[partIndex] = originalData[(x + px) + (y + py) * original.Width];
                        }

                    //Fill the part with the extracted data
                    part.SetData<Color>(partData);
                    //Stick the part in the return array:                    
                    r[index++] = part;
                }
            //Return the array of parts.
            return r;
        }
    }
}