using System;
using System.Collections.Generic;
using Cosmos.System.Graphics;
using Sys = Cosmos.System;
using System.Drawing;
using Cosmos.System;

namespace ProjectOS.Graphics
{
    public class GUI
    {
        // Canvas for drawing
        private Canvas canvas;

        // Pen for drawing points
        private Pen pen;

        // List to store information about drawn pixels
        private List<Tuple<Sys.Graphics.Point, Color>> savedPixels;

        // TabBar for GUI
        private TabBar tabBar;

        // Previous state of the mouse
        private MouseState prevMouseState;

        // Coordinates to track mouse position
        private UInt32 pX, pY;

        // Constructor for GUI class
        public GUI()
        {
            // Initialize the canvas
            this.canvas = FullScreenCanvas.GetFullScreenCanvas();

            // Clear the canvas with a blue background
            this.canvas.Clear(Color.Blue);

            // Initialize a white pen for drawing points
            this.pen = new Pen(Color.White);

            // Initialize the list to store pixel information
            this.savedPixels = new List<Tuple<Sys.Graphics.Point, Color>>();

            // Initialize the TabBar with the canvas
            this.tabBar = new TabBar(this.canvas);

            // Set screen dimensions for the mouse manager
            MouseManager.ScreenHeight = (UInt32)this.canvas.Mode.Rows;
            MouseManager.ScreenWidth = (UInt32)this.canvas.Mode.Columns;
        }

        // Method to handle GUI inputs
        public void handleGUIInputs()
        {
            // Check if the mouse has not moved
            if (this.pX == MouseManager.X && this.pY == MouseManager.Y)
            {
                // If the mouse is on the edge of the screen, prevent it from going out of bounds
                if (MouseManager.X < 2 || MouseManager.Y < 2 || MouseManager.X > (MouseManager.ScreenWidth - 2) || MouseManager.Y > (MouseManager.ScreenHeight - 2))
                    return;

                // Update mouse coordinates
                this.pX = MouseManager.X;
                this.pY = MouseManager.Y;

                // Define points around the mouse position
                Sys.Graphics.Point[] points = new Sys.Graphics.Point[]
                {
                    new Sys.Graphics.Point((Int32)MouseManager.X, (Int32)MouseManager.Y),
                    new Sys.Graphics.Point((Int32)MouseManager.X + 1, (Int32)MouseManager.Y),
                    new Sys.Graphics.Point((Int32)MouseManager.X - 1, (Int32)MouseManager.Y),
                    new Sys.Graphics.Point((Int32)MouseManager.X, (Int32)MouseManager.Y + 1),
                    new Sys.Graphics.Point((Int32)MouseManager.X, (Int32)MouseManager.Y - 1)
                };

                // Restore the color of previously saved pixels
                foreach (Tuple<Sys.Graphics.Point, Color> pixelData in this.savedPixels)
                    this.canvas.DrawPoint(new Pen(pixelData.Item2), pixelData.Item1);

                // Clear the list of saved pixels
                this.savedPixels.Clear();

                // Draw points around the mouse position and save their original colors
                foreach (Sys.Graphics.Point p in points)
                {
                    this.savedPixels.Add(new Tuple<Sys.Graphics.Point, Color>(p, this.canvas.GetPointColor(p.X, p.Y)));
                    this.canvas.DrawPoint(this.pen, p);
                }
            }

            // Beep when the left mouse button is clicked
            if (MouseManager.MouseState == MouseState.Left && this.prevMouseState != MouseState.Left)
                System.Console.Beep();

            // Update the previous mouse state
            this.prevMouseState = MouseManager.MouseState;
        }
    }
}
