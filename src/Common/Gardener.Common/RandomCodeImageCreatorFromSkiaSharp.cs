// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using SkiaSharp;
using System;

namespace Gardener.Common
{
    /// <summary>
    /// 创建校验码图片
    /// </summary>
    /// <remarks>
    /// SkiaSharp 实现
    /// </remarks>
    public class RandomCodeImageCreatorFromSkiaSharp
    {
        //背景颜色
        static readonly SKColor[] BackgroudColors = { SKColors.LightBlue, SKColors.LightCyan, SKColors.LightGray, SKColors.LightGreen, SKColors.LightPink, SKColors.LightSalmon };

        //字体颜色
        static readonly SKColor[] FontColors = { SKColors.Black, SKColors.Red, SKColors.DarkBlue, SKColors.Green, SKColors.Orange, SKColors.Brown, SKColors.DarkCyan, SKColors.Purple };

        //字体
        static readonly string[] Fonts = { "Consolas"};

        /// <summary>  
        /// 生成图像
        /// </summary>  
        /// <param name="code"></param>
        /// <param name="fontSize">基准字体大小，实际生成的字体大小以此为基准进行缩放</param>
        public static byte[] Create(string code, int fontSize = 18)
        {
            if (string.IsNullOrEmpty(code)) return new byte[0];

            Random random = new Random();
            byte[]? imageBuffer = null;
            int space = fontSize / 3;

            int width = code.Length * (fontSize + space);
            int height = (int)Math.Ceiling(fontSize * 1.5);
            using (SKBitmap image2d = new SKBitmap(width, height, SKColorType.Rgba8888, SKAlphaType.Opaque))
            {
                using (SKCanvas canvas = new SKCanvas(image2d))
                {
                    canvas.DrawColor(SKColors.White);
                    char[] codeArr = code.ToCharArray();
                    int disturbFontSize = 8;
                    //在随机位置画背景点  
                    for (int i = 0; i < codeArr.Length * 5; i++)
                    {
                        int x = random.Next(width - disturbFontSize);
                        int y = random.Next(height - disturbFontSize) + disturbFontSize;
                        int colorIndex = random.Next(BackgroudColors.Length);
                        int fontIndex = random.Next(Fonts.Length);
                        SKFontStyleSlant slant = (SKFontStyleSlant)random.Next(2);
                        SKTypeface typeface = SKTypeface.FromFamilyName(Fonts[fontIndex],SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, slant);
                        SKFont font = new SKFont(typeface, disturbFontSize);
                        using (SKPaint drawStyle = new SKPaint(font))
                        {
                            drawStyle.SetColor(BackgroudColors[colorIndex], SKColorSpace.CreateSrgb());
                            string disturCode= RandomCodeCreator.Create(CodeCharacterTypeEnum.NumberAndCharacter, 1);
                            canvas.DrawText(disturCode, x, y, drawStyle);
                        }
                    }
                    
                    for (int i = 0; i < codeArr.Length; i++)
                    {
                        int characterFontSize = random.Next(fontSize, (int)(fontSize * 1.5));
                        int fontIndex = random.Next(Fonts.Length);
                        SKFontStyleSlant slant = (SKFontStyleSlant)random.Next(2);
                        SKTypeface typeface = SKTypeface.FromFamilyName(Fonts[fontIndex], SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, slant);
                        SKFont font = new SKFont(typeface, characterFontSize);

                        int colorIndex = random.Next(FontColors.Length);
                        int x = i == 0 ? random.Next(space) : (((space + fontSize) * i) - random.Next(space * 2));
                        int y = random.Next((height - fontSize))+ fontSize;
                        using (SKPaint drawStyle = new SKPaint(font))
                        {
                            drawStyle.SetColor(FontColors[colorIndex], SKColorSpace.CreateSrgb());
                            canvas.DrawText(codeArr[i].ToString(), x, y, drawStyle);
                        }
                    }
                    using (SKImage img = SKImage.FromBitmap(image2d))
                    {
                        using (SKData p = img.Encode(SKEncodedImageFormat.Png, 100))
                        {
                            imageBuffer= p.ToArray();
                        }
                    }
                }
            }
            return imageBuffer;
        }

    }
}
