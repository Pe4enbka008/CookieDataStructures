using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/*
    CookieDataStructure
    Copyright (C) 2026 Pe4enbka008 (Helen Ivanova) 

    This code is free software: you can redistribute it and/or modify 
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or any later version. 

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

    See the GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program. If not, see <https://www.gnu.org/licenses/>.
*/


namespace smth
{
    /// <summary>
    /// Node for graph only!
    /// This CookieDataStructure requires CookieNode.cs and CookieNodeList.cs files!
    /// </summary>
    /// <typeparam name="T">Type of the node</typeparam>
    public class CookieGraphNode<T> 
    {
        /// <summary>
        /// Node value
        /// </summary>
        private T value;
        private CookieNodeList<CookieGraphNode<T>> neighbours;


        /// <summary>
        /// Class setter
        /// </summary>
        public CookieGraphNode() { this.value = default; this.neighbours = new(); }
        /// <summary>
        /// Class setter with variable
        /// </summary>
        public CookieGraphNode(T value) { this.value = value; this.neighbours = new(); }


        // Value:
        /// <summary>
        /// Return value of the node 
        /// </summary>
        /// <returns>The value</returns>
        public T Value { get { return GetValue(); } }

        /// <summary>
        /// The function gets the node value
        /// </summary>
        /// <returns>The value</returns>
        public T GetValue()
        { return this.value; }


        /// <summary>
        /// The function sets value 
        /// </summary>
        /// <param name="new_value">New value to set</param>
        public void SetValue(T new_value)
        { this.value = new_value; }



        // Next:
        /// <summary>
        /// Returns next node, also can set the next node 
        /// </summary>
        /// <returns>The value</returns>
        public CookieNodeList<CookieGraphNode<T>> Neighbours { get { return GetNeighbours(); } }

        /// <summary>
        /// The function returns next node
        /// </summary>
        /// <returns>Next node saved</returns>
        public CookieNodeList<CookieGraphNode<T>> GetNeighbours()
        { return this.neighbours; }


        /// <summary>
        /// The function saves next node
        /// </summary>
        /// <param name="new_value">The next node</param>
        public void SetNewNeighbour(CookieGraphNode<T> new_value)
        { if (!this.neighbours.Contains(new_value)) this.neighbours.Append(new_value); }

        /// <summary>
        /// The function saves next node
        /// </summary>
        /// <param name="new_value">The next node</param>
        public void SetNewNeighbour(T new_value)
        { this.SetNewNeighbour(new CookieGraphNode<T>(new_value)); }


        /// <summary>
        /// The function saves next node
        /// </summary>
        /// <param name="new_value">The next node</param>
        public void RemoveNeighbour(CookieGraphNode<T> new_value)
        { this.neighbours.Remove(new_value); }

        /// <summary>
        /// The function saves next node
        /// </summary>
        /// <param name="new_value">The next node</param>
        public void RemoveNeighbour(T new_value)
        { this.RemoveNeighbour(new CookieGraphNode<T>(new_value)); }


        // override:
        /// <summary>
        /// Returns string of the value saved here
        /// </summary>
        /// <returns>Value as a string</returns>
        public override string ToString()
        {
            string neighbours = "";
            foreach (var neighbour in this.neighbours)
                neighbours += neighbour.Value.ToString() + ", ";
            neighbours = neighbours.Length > 2 ? neighbours.Substring(0, neighbours.Length - 2) : "None";
            return $"{this.value} --> |{neighbours}|";
        } // override ToString


        public override bool Equals(object? obj)
        {
            if (obj is CookieGraphNode<T>) return this.Value.Equals(((CookieGraphNode<T>)obj).Value);
            if (obj is T) return this.Value.Equals((T)obj);
            return false;
        } // override Equals

    } // class CookieGraphNode




    /// <summary>
    /// This CookieDataStructure requires CookieNode.cs, CookieNodeList.cs, CookieTuple.cs and CookieQueue.cs files!
    /// Graph created and better-ed by Cookie :]
    /// </summary>
    /// <typeparam name="GraphType">Type of the node graph</typeparam>
    public class CookieGraph<GraphType> : IEnumerable<GraphType>
    {
        private CookieNodeList<CookieGraphNode<GraphType>> nodes;

        public CookieGraph()
        { this.nodes = new CookieNodeList<CookieGraphNode<GraphType>>(); }



        // Getters:

        public CookieNodeList<GraphType> Neighbours(GraphType value)
        {
            if (!this.Contains(value)) throw new KeyNotFoundException();
            CookieNodeList<GraphType> nodes = new CookieNodeList<GraphType>();
            foreach (var node in this.GetNode(value).Neighbours)
                nodes.Append(node.Value);
            return nodes;
        } // Neighbours


        private CookieGraphNode<GraphType> GetNode(GraphType value)
        {
            foreach (var node in this.nodes)
                if (node.Equals(value)) return node;
            return null;
        } // GetNode


        public CookieNodeList<CookieTuple<GraphType>> GetEdges()
        {
            var list = new CookieNodeList<CookieTuple<GraphType>>();
            foreach (var node in this.nodes)
                foreach (var edge in node.Neighbours)
                    list.Append(new CookieTuple<GraphType>(new GraphType[] { node.Value, edge.Value }));
            return list;
        } // GetEdges


        // Special case: Contains

        public bool Contains(GraphType value)
        { return this.nodes.Contains(GetNode(value)); }



        // Count

        public int VertexCount()
        { return this.nodes.Count; }

        /// <summary>
        /// Count of arrows in the graph (created connections)
        /// </summary>
        /// <returns></returns>
        public int EdgeCount()
        {
            int count = 0;
            foreach (var node in this.nodes)
                count += node.Neighbours.Count;
            return count;
        } // EdgeCount

        public int OutDegree(GraphType value)
        { return this.GetNode(value).Neighbours.Count; }

        public int InDegree(GraphType value)
        {
            int count = 0;
            CookieGraphNode<GraphType> node = new(value);
            foreach (var inner_node in nodes)
                if (inner_node.Neighbours.Contains(node))
                    count++;
            return count;
        } // InDegree



        // Adders:

        public void AddVertex(GraphType value)
        {
            if (this.Contains(value)) return;
            this.nodes.Append(new CookieGraphNode<GraphType>(value));
        } // AddVertex

        public void AddEdge(GraphType from, GraphType to)
        {
            if (!(this.Contains(from) && this.Contains(to))) return;
            this.GetNode(from).SetNewNeighbour(this.GetNode(to));
        } // AddVertex

        public void AddDoubleEdge(GraphType from, GraphType to)
        {
            if (!(this.Contains(from) && this.Contains(to))) return;
            this.GetNode(from).SetNewNeighbour(this.GetNode(to));
            this.GetNode(to).SetNewNeighbour(this.GetNode(from));
        } // AddDoubleEdge



        // Removers:

        public void RemoveVertex(GraphType value)
        {
            if (!this.Contains(value)) return;
            this.nodes.Remove(new CookieGraphNode<GraphType>(value));
            foreach (var inner_node in this.nodes)
                inner_node.RemoveNeighbour(value);
        } // RemoveVertex

        public void RemoveEdge(GraphType from, GraphType to)
        {
            if (!(this.Contains(from) && this.Contains(to))) return;
            this.GetNode(from).RemoveNeighbour(to);
        } // RemoveEdge

        public void RemoveDoubleEdge(GraphType from, GraphType to)
        {
            if (!(this.Contains(from) && this.Contains(to))) return;
            this.GetNode(from).RemoveNeighbour(to);
            this.GetNode(to).RemoveNeighbour(from);
        } // RemoveDoubleEdge

        public void Clear()
        { this.nodes.Clear(); }



        // Searches:

        /// <summary>
        /// DFS - Go as deep as possible before going sideways
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public CookieNodeList<GraphType> DepthFirstSearch(GraphType from)
        {
            if (!Contains(from)) throw new KeyNotFoundException();
            CookieNodeList<GraphType> visited = new(), return_value = new(); 
            DepthFirstSearch(this.GetNode(from), visited, return_value);
            return return_value;
        } // DepthFirstSearch
        private static void DepthFirstSearch(CookieGraphNode<GraphType> from, CookieNodeList<GraphType> visited, CookieNodeList<GraphType> nodes)
        {
            if (from == null || visited.Contains(from.Value)) return;
            visited.Add(from.Value);

            nodes.Append(from.Value);
            foreach (var neighbour in from.Neighbours)
                DepthFirstSearch(neighbour, visited, nodes);
        } // DepthFirstSearch


        /// <summary>
        /// BFS - Visit level by level
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public CookieNodeList<GraphType> BreadthFirstSearch(GraphType from)
        {
            if (!Contains(from)) throw new KeyNotFoundException();

            CookieNodeList<GraphType> visited = new();
            visited.Add(from);

            CookieQueue<CookieGraphNode<GraphType>> walk_queue = new();
            walk_queue.Insert(GetNode(from));

            CookieNodeList<GraphType> return_value = new();

            while (!walk_queue.IsEmpty())
            {
                var current = walk_queue.Remove;
                return_value.Append(current.Value);

                foreach (var neighbour in current.Neighbours)
                    if (!visited.Contains(neighbour.Value))
                    {
                        visited.Add(neighbour.Value);
                        walk_queue.Insert(neighbour);
                    } // if
            } // while

            return return_value;
        } // BreadthFirstSearch


        public IEnumerator<string> GetGraph()
        {
            foreach (var node in this.nodes)
                foreach (var neighbour in node.Neighbours)
                    yield return $"{node.Value} --> {neighbour.Value}";
        } // GetGraph


        // Pathers:

        public bool HasPath(GraphType from, GraphType to)
        {
            if (!(Contains(from) && Contains(to))) return false;
            CookieNodeList<GraphType> visited = new();
            return HasPath(GetNode(from), to, visited);
        } // HasPath
        private static bool HasPath(CookieGraphNode<GraphType> current, GraphType target, CookieNodeList<GraphType> visited)
        {
            if (current == null) return false;
            if (current.Equals(target)) return true;
            if (visited.Contains(current.Value)) return false;
            visited.Add(current.Value);

            foreach (var neighbour in current.Neighbours)
                if (HasPath(neighbour, target, visited))
                    return true;
            return false;
        } // HasPath


        public CookieNodeList<GraphType> ShortestPath(GraphType from, GraphType to)
        {
            if (!Contains(from) || !Contains(to)) throw new KeyNotFoundException();
            throw new NotImplementedException();
        } // ShortestPath


        public bool HasCycle()
        {
            throw new NotImplementedException();
        } // HasCycle
        private static bool HasCycle(CookieGraphNode<GraphType> nodes)
        {
            throw new NotImplementedException();
        } // HasCycle


        public bool IsTree()
        {
            throw new NotImplementedException();
        } // IsTree
        private static bool IsTree(CookieGraphNode<GraphType> nodes)
        {
            throw new NotImplementedException();
        } // IsTree




        // override

        // object

        /// <summary>
        /// override for ToString to - *'value', 'value', 'value', ...*
        /// </summary>
        /// <returns>string of the class</returns>
        public override string ToString()
        { return ToString(", "); }

        /// <summary>
        /// override for ToString to - *'value'{split}'value'{split}'value'{split} ...*
        /// </summary>
        /// <returns>string of the class</returns>
        public string ToString(string split)
        {
            if (this.nodes.IsEmpty()) return "**";
            string str = "*";
            foreach (var node in this.nodes)
                str += node.ToString() + split;
            return str.Substring(0, str.Length - split.Length) + "*";
        } // override ToString


        // IEnumerable
        public IEnumerator<GraphType> GetEnumerator()
        { 
            foreach (var node in this.nodes)
                yield return node.Value;
        } // GetEnumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    } // CookieGraph
}
