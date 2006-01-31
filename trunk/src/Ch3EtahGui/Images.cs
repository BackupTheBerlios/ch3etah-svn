/*   Copyright 2004 Jacob Eggleston
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 *
 *   ========================================================================
 *
 *   File Created using SharpDevelop.
 *   User: Jacob Eggleston
 *   Date: 1/10/2004
 */

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Ch3Etah.Gui {
	public class Images {

				// ImageList.Images[int index] does not preserve alpha channel.
		private static Image[] images = null;
		private static ImageList imageList = null;
		private static int _count = 0;

		static Images() {
			LoadImages();
		}

		public static Icon CreateIcon(Image image) {
			return Icon.FromHandle(((Bitmap) image).GetHicon());
		}

		public static void LoadImages() {
			// TODO alpha channel PNG loader is not working on .NET Service RC1
			Stream bitmapStream = typeof (Images).Assembly.GetManifestResourceStream("Ch3Etah.Gui.ImageList16.png");
			Bitmap bitmap = new Bitmap(bitmapStream);
			int count = bitmap.Width/bitmap.Height;
			imageList = null;
			images = new Image[count * 2];
			Rectangle rectangle = new Rectangle(0, 0, bitmap.Height, bitmap.Height);
			for (int i = 0; i < count; i++) {
				images[i] = bitmap.Clone(rectangle, bitmap.PixelFormat);
				images[count + i] = GetFadedIcon(images[i]);
				rectangle.X += bitmap.Height;
			}
			bitmapStream.Close();
			_count = count;
		}

		public static ImageList GetImageList() {
			if (imageList == null) {
				imageList = new ImageList();
				imageList.ImageSize = images[0].Size;
				imageList.ColorDepth = ColorDepth.Depth24Bit;
				imageList.TransparentColor = Color.Magenta;
				foreach (Image image in images) {
					imageList.Images.Add(image);
				}
			}
			return imageList;
		}

		public static Image ByIndex(int i) {
			return images[i];
		}

		
		private static Image GetFadedIcon(Image icon)
		{
			ColorMatrix cm = new ColorMatrix();
			cm.Matrix00 = cm.Matrix11 = cm.Matrix22 = 1.0f;
			cm.Matrix33 = 0.15f;

			System.Drawing.Imaging.ImageAttributes attr = new System.Drawing.Imaging.ImageAttributes();
			attr.SetColorMatrix(cm);

			Bitmap fadedbmp = new Bitmap(icon.Width, icon.Height);
			Graphics g = Graphics.FromImage(fadedbmp);
			Brush br = new SolidBrush(Color.FromArgb(255, 255, 255));
			g.FillRectangle(br, 0, 0, fadedbmp.Width, fadedbmp.Height);
			g.DrawImage(
				icon
				, new Rectangle(0, 0, icon.Width, icon.Height)
				, 0, 0, icon.Width, icon.Height
				, GraphicsUnit.Pixel
				, attr);
			return fadedbmp;
		}


		public static int Count
		{
			get { return _count; }
		}

		#region Indexes

		public class Indexes {
			public static int New {
				get { return 0; }
			}

			public static int Open {
				get { return 1; }
			}

			public static int Save {
				get { return 2; }
			}

			public static int Cut {
				get { return 3; }
			}

			public static int Copy {
				get { return 4; }
			}

			public static int Paste {
				get { return 5; }
			}

			public static int Delete {
				get { return 6; }
			}

			public static int Properties {
				get { return 7; }
			}

			public static int Undo {
				get { return 8; }
			}

			public static int Redo {
				get { return 9; }
			}

			public static int Preview {
				get { return 10; }
			}

			public static int Print {
				get { return 11; }
			}

			public static int Search {
				get { return 12; }
			}

			public static int ReSearch {
				get { return 13; }
			}

			public static int Help {
				get { return 14; }
			}

			public static int ZoomIn {
				get { return 15; }
			}

			public static int ZoomOut {
				get { return 16; }
			}

			public static int Back {
				get { return 17; }
			}

			public static int Forward {
				get { return 18; }
			}

			public static int Favorites {
				get { return 19; }
			}

			public static int AddToFavorites {
				get { return 20; }
			}

			public static int Stop {
				get { return 21; }
			}

			public static int Refresh {
				get { return 22; }
			}

			public static int Home {
				get { return 23; }
			}

			public static int Edit {
				get { return 24; }
			}

			public static int Tools {
				get { return 25; }
			}

			public static int Tiles {
				get { return 26; }
			}

			public static int Icons {
				get { return 27; }
			}

			public static int List {
				get { return 28; }
			}

			public static int Details {
				get { return 29; }
			}

			public static int Pane {
				get { return 30; }
			}

			public static int Culture {
				get { return 31; }
			}

			public static int Languages {
				get { return 32; }
			}

			public static int History {
				get { return 33; }
			}

			public static int Mail {
				get { return 34; }
			}

			public static int Parent {
				get { return 35; }
			}

			public static int FolderProperties {
				get { return 36; }
			}

			public static int FolderClosed {
				get { return 37; }
			}

			public static int FolderOpen {
				get { return 38; }
			}

			public static int DocumentText {
				get { return 39; }
			}

			public static int ArrowGreen {
				get { return 40; }
			}

			public static int DocumentArrowGreen {
				get { return 41; }
			}

			public static int DocumentCh3Etah {
				get { return 42; }
			}

			public static int Ch3Etah {
				get { return 43; }
			}

			public static int DataSourceFolder {
				get { return 44; }
			}

			public static int DataSource {
				get { return 45; }
			}

			public static int DataSourceNew {
				get { return 46; }
			}
			
			public static int Entity {
				get { return 47; }
			}
			
			public static int EntityField{
				get { return 48; }
			}
			
			public static int EntityIndex {
				get { return 49; }
			}
			
			public static int Output {
				get { return 50; }
			}
			
			public static int XmlMode {
				get { return 51; }
			}
			
			public static int DesignMode {
				get { return 52; }
			}
			
		}

		#endregion Indexes

		#region Static properties

		public static Image New {
			get { return images[0]; }
		}

		public static Image Open {
			get { return images[1]; }
		}

		public static Image Save {
			get { return images[2]; }
		}

		public static Image Cut {
			get { return images[3]; }
		}

		public static Image Copy {
			get { return images[4]; }
		}

		public static Image Paste {
			get { return images[5]; }
		}

		public static Image Delete {
			get { return images[6]; }
		}

		public static Image Properties {
			get { return images[7]; }
		}

		public static Image Undo {
			get { return images[8]; }
		}

		public static Image Redo {
			get { return images[9]; }
		}

		public static Image Preview {
			get { return images[10]; }
		}

		public static Image Print {
			get { return images[11]; }
		}

		public static Image Search {
			get { return images[12]; }
		}

		public static Image ReSearch {
			get { return images[13]; }
		}

		public static Image Help {
			get { return images[14]; }
		}

		public static Image ZoomIn {
			get { return images[15]; }
		}

		public static Image ZoomOut {
			get { return images[16]; }
		}

		public static Image Back {
			get { return images[17]; }
		}

		public static Image Forward {
			get { return images[18]; }
		}

		public static Image Favorites {
			get { return images[19]; }
		}

		public static Image AddToFavorites {
			get { return images[20]; }
		}

		public static Image Stop {
			get { return images[21]; }
		}

		public static Image Refresh {
			get { return images[22]; }
		}

		public static Image Home {
			get { return images[23]; }
		}

		public static Image Edit {
			get { return images[24]; }
		}

		public static Image Tools {
			get { return images[25]; }
		}

		public static Image Tiles {
			get { return images[26]; }
		}

		public static Image Icons {
			get { return images[27]; }
		}

		public static Image List {
			get { return images[28]; }
		}

		public static Image Details {
			get { return images[29]; }
		}

		public static Image Pane {
			get { return images[30]; }
		}

		public static Image Culture {
			get { return images[31]; }
		}

		public static Image Languages {
			get { return images[32]; }
		}

		public static Image History {
			get { return images[33]; }
		}

		public static Image Mail {
			get { return images[34]; }
		}

		public static Image Parent {
			get { return images[35]; }
		}

		public static Image FolderProperties {
			get { return images[36]; }
		}

		public static Image FolderClosed {
			get { return images[37]; }
		}

		public static Image FolderOpen {
			get { return images[38]; }
		}

		public static Image DocumentText {
			get { return images[39]; }
		}

		public static Image ArrowGreen {
			get { return images[40]; }
		}

		public static Image DocumentArrowGreen {
			get { return images[41]; }
		}

		public static Image DocumentCh3Etah {
			get { return images[42]; }
		}

		public static Image Ch3Etah {
			get { return images[43]; }
		}

		public static Image DataSourceFolder {
			get { return images[44]; }
		}

		public static Image DataSource {
			get { return images[45]; }
		}

		public static Image DataSourceNew {
			get { return images[46]; }
		}

		public static Image Entity {
			get { return images[47]; }
		}
			
		public static Image EntityField{
			get { return images[48]; }
		}
			
		public static Image EntityIndex {
			get { return images[49]; }
		}
			
		public static Image Output {
			get { return images[50]; }
		}
			
		public static Image XmlMode {
			get { return images[51]; }
		}
			
		public static Image DesignMode {
			get { return images[52]; }
		}
			
		#endregion Static properties
	}
}