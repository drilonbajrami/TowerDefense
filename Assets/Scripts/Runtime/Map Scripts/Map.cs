using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Runtime
{


    /// <summary>
    /// Class for generating maps.
    /// </summary>
    public static class Map
    {
        public static int[,] Generate(MapSettings settings)
        {
            (int row, int col) startCell = (Mathf.Clamp(settings.HomeTile.row, 1, settings.Height - 2),
                                            Mathf.Clamp(settings.HomeTile.col, 1, settings.Width - 2));
            (int row, int col) endCell = (Mathf.Clamp(settings.SpawnTile.row, 1, settings.Height - 2),
                                          Mathf.Clamp(settings.SpawnTile.col, 1, settings.Width - 2));

            Stack<Node> path = new Stack<Node>();

            int pathLength = (int)(settings.Width * settings.Height * 0.25f);

            while(path.Count < pathLength)
                path = GeneratePath(settings.Width, settings.Height, startCell, endCell);
            

            int[,] map = new int[settings.Height, settings.Width];

            // Start
            map[settings.HomeTile.row, settings.HomeTile.col] = (int)TileType.Home_Tile;

            // Path
            if(path.Count > 0) 
                foreach (Node cell in path)
                    map[cell.Index.row, cell.Index.col] = (int)TileType.Walk_Tile;

            // End
            map[settings.SpawnTile.row, settings.SpawnTile.col] = (int)TileType.Spawn_Tile;

            return map;
        }

        /// <summary>
        /// A recursive backtracker that generates a maze and returns its solution path.
        /// </summary>
        /// <param name="width">The width of the map.</param>
        /// <param name="height">The height of the map.</param>
        /// <param name="startPoint">Path starting node index.</param>
        /// <param name="endPoint">Path ending node index.</param>
        /// <returns>A single path from the given start point to the end point.</returns>
        private static Stack<Node> GeneratePath(int width, int height, (int row, int col) startPoint, (int row, int col) endPoint)
        {
            Node[,] grid = new Node[height, width];
            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                    grid[row, col] = new Node((row, col));

            // Cache the path as well
            Stack<Node> path = new Stack<Node>();
            path.Push(grid[startPoint.row, startPoint.col]);

            // Start of algorithm
            Stack<Node> stack = new Stack<Node>();
            stack.Push(grid[startPoint.row, startPoint.col]);
            stack.Peek().Visited = true;

            // Generates a maze through the recursive backtracker algorithm,
            // And stores the only the nodes that make up the path from the given start point to the end point.
            while (stack.Count != 0) {
                (int row, int col) topCell = stack.Peek().Index;
                Node cell = grid.GetRandomAdjacentNode(topCell.row, topCell.col);

                if (cell != null) {
                    // Add the connection node to the path.
                    path.Push(grid.GetConnectionNode(stack.Peek().Index, cell.Index));
                    path.Push(cell);
                    cell.Visited = true;
                    stack.Push(cell);

                    if (cell.Index == endPoint)
                        return path;
                }
                else {
                    stack.Pop();

                    // Remove the last path and connection nodes.
                    path.Pop();
                    path.Pop();
                }
            }

            return path;
        }

        /// <summary>
        /// Returns a random adjacent node that has not been visited yet.
        /// </summary>
        /// <param name="grid">The grid of nodes.</param>
        /// <param name="row">The row of current node's index.</param>
        /// <param name="col">The column of current node's index.</param>
        /// <returns>Unvisited node if there is one, null if none.</returns>
        private static Node GetRandomAdjacentNode(this Node[,] grid, int row, int col)
        {
            List<Node> adjacentNodes = new List<Node>();
            if (row + 2 < grid.GetLength(0) - 1 && !grid[row + 2, col].Visited) adjacentNodes.Add(grid[row + 2, col]);
            if (row - 2 > 0 && !grid[row - 2, col].Visited) adjacentNodes.Add(grid[row - 2, col]);
            if (col + 2 < grid.GetLength(1) - 1 && !grid[row, col + 2].Visited) adjacentNodes.Add(grid[row, col + 2]);
            if (col - 2 > 0 && !grid[row, col - 2].Visited) adjacentNodes.Add(grid[row, col - 2]);
            return adjacentNodes.Count > 0 ? adjacentNodes[Random.Range(0, adjacentNodes.Count)] : null;
        }

        /// <summary>
        /// Returns the connection node between the two given nodes.
        /// </summary>
        /// <param name="grid">The grid of nodes.</param>
        /// <param name="nodeA">Start node.</param>
        /// <param name="nodeB">End node.</param>
        /// <returns>The connection node inbetween start and end nodes.</returns>
        private static Node GetConnectionNode(this Node[,] grid, (int row, int col) nodeA, (int row, int col) nodeB)
        {
            int nRow = nodeA.row - (nodeA.row - nodeB.row) / 2;
            int nCol = nodeA.col - (nodeA.col - nodeB.col) / 2;
            return grid[nRow, nCol];
        }
    }

    /// <summary>
    /// Stores index and visited flag for a node in a grid.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Index of the node within the grid.
        /// </summary>
        public (int row, int col) Index { get; private set; }

        /// <summary>
        /// Flag for checking if node has been visited yet or not.
        /// </summary>
        public bool Visited { get; set; } = false;

        /// <summary>
        /// Constructor, <see cref="Visited"/> is set to false by default.
        /// </summary>
        public Node((int row, int col) index) => Index = index;
    }
}
