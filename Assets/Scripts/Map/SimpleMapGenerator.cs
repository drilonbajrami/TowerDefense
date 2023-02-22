using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Map
{
    /// <summary>
    /// Simple map generator.
    /// </summary>
    public class SimpleMapGenerator : MonoBehaviour, IMapGenerator
    {
        /// <summary>
        /// Stores index and visited flag for a node in a grid.
        /// </summary>
        private class Node
        {
            /// <summary>
            /// Index of the node within the grid.
            /// </summary>
            public (int row, int col) Index { get; private set; } = (-1, -1);

            /// <summary>
            /// Flag for checking if node has been visited yet or not.
            /// </summary>
            public bool Visited { get; set; } = false;

            /// <summary>
            /// Constructor, <see cref="Visited"/> is set to false by default.
            /// </summary>
            public Node((int row, int col) index) => Index = index;
        }

        /// <summary>
        /// Random generator.
        /// </summary>
        private readonly System.Random _random = new();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ITile[,] GenerateMap(IMapSettings settings)
        {
            (int row, int col) HomeNode = (settings.Height - 2, 0);
            (int row, int col) SpawnNode = (1, settings.Width - 1);

            (int row, int col) pathStartNode = (HomeNode.row, HomeNode.col + 1);
            (int row, int col) pathEndNode = (SpawnNode.row, SpawnNode.col - 1);

            // The path's tiles should consist at least 30% of all usable tiles for path generation.
            int usableTileCount = (settings.Width - 2) * (settings.Height - 2);
            int pathLength = (int)(0.3f * usableTileCount);

            Stack<Node> path = new();
            while (path.Count < pathLength)
                path = GeneratePath(settings.Width, settings.Height, pathStartNode, pathEndNode);

            // Create map and assign values.
            ITile[,] map = new ITile[settings.Height, settings.Width];

            for (int row = 0; row < settings.Height; row++)
                for (int col = 0; col < settings.Width; col++)
                    map[row, col] = new Tile(row, col);

            // Home and Spawn tiles.
            map[HomeNode.row, HomeNode.col].SetZone(Zone.Home);
            map[SpawnNode.row, SpawnNode.col].SetZone(Zone.Spawn);

            // Path tiles.
            if (path.Count > 0)
                foreach (Node cell in path)
                    map[cell.Index.row, cell.Index.col].SetZone(Zone.Path);

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
        private Stack<Node> GeneratePath(int width, int height, (int row, int col) startPoint, (int row, int col) endPoint)
        {
            Node[,] grid = new Node[height, width];
            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                    grid[row, col] = new Node((row, col));

            // Cache the path as well
            Stack<Node> path = new();
            path.Push(grid[startPoint.row, startPoint.col]);

            // Start of algorithm
            Stack<Node> stack = new Stack<Node>();
            stack.Push(grid[startPoint.row, startPoint.col]);
            stack.Peek().Visited = true;

            // Generates a maze through the recursive backtracker algorithm,
            // And stores the only the nodes that make up the path from the given start point to the end point.
            while (stack.Count != 0) {
                (int row, int col) topCell = stack.Peek().Index;
                Node cell = GetRandomAdjacentNode(grid, topCell.row, topCell.col, width, height);

                if (cell != null) {
                    // Add the connection node to the path.
                    path.Push(GetConnectionNode(grid, stack.Peek().Index, cell.Index));
                    path.Push(cell);
                    cell.Visited = true;
                    stack.Push(cell);

                    if (cell.Index == endPoint) return path;
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
        /// <param name="width">The width of the map.</param>
        /// <param name="height">The height of the map.</param>
        /// <returns>Unvisited node if there is one, null if none.</returns>
        private Node GetRandomAdjacentNode(Node[,] grid, int row, int col, int width, int height)
        {
            List<Node> adjacentNodes = new();
            if (row + 2 < height - 1 && !grid[row + 2, col].Visited) adjacentNodes.Add(grid[row + 2, col]);
            if (row - 2 > 0 && !grid[row - 2, col].Visited) adjacentNodes.Add(grid[row - 2, col]);
            if (col + 2 < width - 1 && !grid[row, col + 2].Visited) adjacentNodes.Add(grid[row, col + 2]);
            if (col - 2 > 0 && !grid[row, col - 2].Visited) adjacentNodes.Add(grid[row, col - 2]);
            return adjacentNodes.Count > 0 ? adjacentNodes[_random.Next(0, adjacentNodes.Count)] : null;
        }

        /// <summary>
        /// Returns the connection node between the two given nodes.
        /// </summary>
        /// <param name="grid">The grid of nodes.</param>
        /// <param name="nodeA">Start node.</param>
        /// <param name="nodeB">End node.</param>
        /// <returns>The connection node inbetween start and end nodes.</returns>
        private Node GetConnectionNode(Node[,] grid, (int row, int col) nodeA, (int row, int col) nodeB)
        {
            int nRow = nodeA.row - (nodeA.row - nodeB.row) / 2;
            int nCol = nodeA.col - (nodeA.col - nodeB.col) / 2;
            return grid[nRow, nCol];
        }
    }
}