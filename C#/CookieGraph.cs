using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

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



/*
CookieDataStructure: CookieGraphNode contains
    GetValue - public function
    GetNeighbours - public function
    SetValue - public function
    SetNewNeighbour - public function
    RemoveNeighbour - public function
    ToString - public function
    Equals - public function

CookieDataStructure: CookieGraph contains
    Neighbours - public function
    GetNode - private function
    GetEdges - public function
    Contains - public function
    VertexCount - public function
    EdgeCount - public function
    OutDegree - public function
    InDegree - public function
    AddVertex - public function
    AddEdge - public function
    AddDoubleEdge - public function
    RemoveVertex - public function
    RemoveEdge - public function
    RemoveDoubleEdge - public function
    Clear - public function
    DepthFirstSearch - public function (and matching private recursive overload)
    BreadthFirstSearch - public function
    GetGraph - public function
    HasPath - public function (and matching private recursive overload)
    ShortestPath - public function
    HasCycle - public function (and matching private recursive overload)
    IsTree - public function
    ToString - public function (2 overloads: no-arg, with split string)
    GetEnumerator - public function (and IEnumerable.GetEnumerator implementation)
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
        private T value;
        private CookieNodeList<CookieGraphNode<T>> neighbours;


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
        public void RemoveNeighbour(CookieGraphNode<T> new_value)
        { this.neighbours.Remove(new_value); }



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


        /// <summary>
        /// Checks equality against another CookieGraphNode (by value) or a raw value of type T
        /// </summary>
        /// <param name="obj">A CookieGraphNode&lt;T&gt; or a value of type T to compare against</param>
        /// <returns>True if the value matches; otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (obj is CookieGraphNode<T>) return this.Value.Equals(((CookieGraphNode<T>)obj).Value);
            if (obj is T) return this.Value.Equals((T)obj);
            return false;
        } // override Equals

    } // class CookieGraphNode




    /// <summary>
    /// This CookieDataStructure requires CookieNode.cs, CookieNodeList.cs, CookieTuple.cs, CookieQueue.cs and CookieDict.cs files!
    /// Graph created and better-ed by Cookie :]
    /// </summary>
    /// <typeparam name="GraphType">Type of the node graph</typeparam>
    public class CookieGraph<GraphType> : IEnumerable<GraphType>
    {
        private CookieNodeList<CookieGraphNode<GraphType>> nodes;


        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieGraph()
        { this.nodes = new CookieNodeList<CookieGraphNode<GraphType>>(); }



        // Getters:

        /// <summary>
        /// Returns the values of all neighbours (outgoing edges) of a vertex
        /// </summary>
        /// <param name="value">Vertex to look up</param>
        /// <returns>List of neighbouring values</returns>
        /// <exception cref="KeyNotFoundException">If the vertex doesn't exist</exception>
        public CookieNodeList<GraphType> Neighbours(GraphType value)
        {
            if (!this.Contains(value)) throw new CookieValueNotFoundException(value);
            CookieNodeList<GraphType> nodes = new CookieNodeList<GraphType>();
            foreach (var node in this.GetNode(value).Neighbours)
                nodes.Append(node.Value);
            return nodes;
        } // Neighbours


        /// <summary>
        /// Finds the internal graph node holding a given value
        /// </summary>
        /// <param name="value">Value to look for</param>
        /// <returns>The matching CookieGraphNode, or null if not found</returns>
        private CookieGraphNode<GraphType> GetNode(GraphType value)
        {
            foreach (var node in this.nodes)
                if (node.Equals(value)) return node;
            return null;
        } // GetNode


        /// <summary>
        /// Returns every edge in the graph as (from, to) tuples
        /// </summary>
        /// <returns>List of edges, each a 2-element CookieTuple</returns>
        public CookieNodeList<CookieTuple<GraphType>> GetEdges()
        {
            var list = new CookieNodeList<CookieTuple<GraphType>>();
            foreach (var node in this.nodes)
                foreach (var edge in node.Neighbours)
                    list.Append(new CookieTuple<GraphType>(new GraphType[] { node.Value, edge.Value }));
            return list;
        } // GetEdges


        // Special case: Contains

        /// <summary>
        /// Checks if a vertex with the given value exists in the graph
        /// </summary>
        /// <param name="value">Value to look for</param>
        /// <returns>True if found</returns>
        public bool Contains(GraphType value)
        { return this.nodes.Contains(GetNode(value)); }



        // Count

        /// <summary>
        /// Returns the number of vertices in the graph
        /// </summary>
        /// <returns>Vertex count</returns>
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

        /// <summary>
        /// Returns the number of outgoing edges from a vertex
        /// </summary>
        /// <param name="value">Vertex to check</param>
        /// <returns>Number of outgoing edges</returns>
        public int OutDegree(GraphType value)
        { return this.GetNode(value).Neighbours.Count; }

        /// <summary>
        /// Returns the number of incoming edges to a vertex
        /// </summary>
        /// <param name="value">Vertex to check</param>
        /// <returns>Number of incoming edges</returns>
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

        /// <summary>
        /// Adds a new vertex to the graph, if it doesn't already exist
        /// </summary>
        /// <param name="value">Value of the new vertex</param>
        public void AddVertex(GraphType value)
        {
            if (this.Contains(value)) return;
            this.nodes.Append(new CookieGraphNode<GraphType>(value));
        } // AddVertex

        /// <summary>
        /// Adds a directed edge from one vertex to another
        /// </summary>
        /// <param name="from">Source vertex</param>
        /// <param name="to">Destination vertex</param>
        public void AddEdge(GraphType from, GraphType to)
        {
            if (!(this.Contains(from) && this.Contains(to))) return;
            this.GetNode(from).SetNewNeighbour(this.GetNode(to));
        } // AddVertex

        /// <summary>
        /// Adds edges in both directions between two vertices (undirected edge)
        /// </summary>
        /// <param name="from">First vertex</param>
        /// <param name="to">Second vertex</param>
        public void AddDoubleEdge(GraphType from, GraphType to)
        {
            if (!(this.Contains(from) && this.Contains(to))) return;
            this.GetNode(from).SetNewNeighbour(this.GetNode(to));
            this.GetNode(to).SetNewNeighbour(this.GetNode(from));
        } // AddDoubleEdge



        // Removers:

        /// <summary>
        /// Removes a vertex and every edge pointing to or from it
        /// </summary>
        /// <param name="value">Vertex to remove</param>
        public void RemoveVertex(GraphType value)
        {
            if (!this.Contains(value)) return;
            this.nodes.Remove(new CookieGraphNode<GraphType>(value));
            foreach (var inner_node in this.nodes)
                inner_node.RemoveNeighbour(new (value));
        } // RemoveVertex

        /// <summary>
        /// Removes the directed edge from one vertex to another
        /// </summary>
        /// <param name="from">Source vertex</param>
        /// <param name="to">Destination vertex</param>
        public void RemoveEdge(GraphType from, GraphType to)
        {
            if (!(this.Contains(from) && this.Contains(to))) return;
            this.GetNode(from).RemoveNeighbour(new (to));
        } // RemoveEdge

        /// <summary>
        /// Removes edges in both directions between two vertices
        /// </summary>
        /// <param name="from">First vertex</param>
        /// <param name="to">Second vertex</param>
        public void RemoveDoubleEdge(GraphType from, GraphType to)
        {
            if (!(this.Contains(from) && this.Contains(to))) return;
            this.GetNode(from).RemoveNeighbour(new(to));
            this.GetNode(to).RemoveNeighbour(new(from));
        } // RemoveDoubleEdge

        /// <summary>
        /// Wipes the graph clean (removes all vertices and edges)
        /// </summary>
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
            if (!Contains(from)) throw new CookieValueNotFoundException(from);
            CookieNodeList<GraphType> visited = new(), return_value = new(); 
            DepthFirstSearch(this.GetNode(from), visited, return_value);
            return return_value;
        } // DepthFirstSearch
        /// <summary>
        /// Recursively visits a node and all its unvisited neighbours, depth-first
        /// </summary>
        /// <param name="from">Node to visit from</param>
        /// <param name="visited">Set of values already visited</param>
        /// <param name="nodes">Accumulator list of values visited in order</param>
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
            if (!Contains(from)) throw new CookieValueNotFoundException(from);

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


        /// <summary>
        /// Yields a human-readable line for every edge in the graph
        /// </summary>
        /// <returns>Enumerator of strings like "from --> to"</returns>
        public IEnumerator<string> GetGraph()
        {
            foreach (var node in this.nodes)
                foreach (var neighbour in node.Neighbours)
                    yield return $"{node.Value} --> {neighbour.Value}";
        } // GetGraph


        // Pathers:

        /// <summary>
        /// Checks if a path exists from one vertex to another, following directed edges
        /// </summary>
        /// <param name="from">Starting vertex</param>
        /// <param name="to">Target vertex</param>
        /// <returns>True if a path exists</returns>
        public bool HasPath(GraphType from, GraphType to)
        {
            if (!(Contains(from) && Contains(to))) return false;
            CookieNodeList<GraphType> visited = new();
            return HasPath(GetNode(from), to, visited);
        } // HasPath
        /// <summary>
        /// Recursively searches for a path from the current node to the target value
        /// </summary>
        /// <param name="current">Node currently being visited</param>
        /// <param name="target">Value being searched for</param>
        /// <param name="visited">Set of values already visited</param>
        /// <returns>True if the target is reachable from here</returns>
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


        /// <summary>
        /// Finds the shortest path (fewest edges) between two vertices using BFS
        /// </summary>
        /// <param name="from">Starting vertex</param>
        /// <param name="to">Target vertex</param>
        /// <returns>List of vertices from 'from' to 'to', inclusive</returns>
        /// <exception cref="KeyNotFoundException">If either vertex doesn't exist</exception>
        /// <exception cref="Exception">If no path exists between the vertices</exception>
        public CookieNodeList<GraphType> ShortestPath(GraphType from, GraphType to)
        {
            if (!Contains(from))
                throw new CookieValueNotFoundException(from);
            else if (!Contains(to))
                throw new CookieValueNotFoundException(to);

            CookieQueue<CookieGraphNode<GraphType>> queue = new();
            queue.Insert(GetNode(from));

            CookieNodeList<GraphType> visited = new();
            visited.Add(from);

            CookieDict<GraphType, GraphType> parent_list = new();

            while (!queue.IsEmpty())
            {
                var current_node = queue.Remove;
                if (current_node.Value.Equals(to)) break;

                foreach (var neighbour in current_node.Neighbours)
                    if (!visited.Contains(neighbour.Value))
                    {
                        visited.Add(neighbour.Value);
                        parent_list[neighbour.Value] = current_node.Value;
                        queue.Insert(neighbour);
                    } // if
            } // while

            if (!visited.Contains(to)) throw new CookieStructureArgumentException($"no path from {from} to {to}");
            CookieNodeList<GraphType> result = new();

            GraphType current_value = to;
            result.Add(current_value);

            while (!current_value.Equals(from))
            {
                current_value = parent_list[current_value];
                result.Add(current_value);
            } // while 
            return result;
        } // ShortestPath



        /// <summary>
        /// Checks if the graph contains a cycle (a path that loops back to a previously visited vertex)
        /// KNOWN LIMITATION!!!
        /// for an undirected graph built with AddDoubleEdge, HasCycle() always returns true
        /// </summary>
        /// <returns>True if a cycle is found</returns>
        public bool HasCycle()
        {
            foreach (CookieGraphNode<GraphType> root in this.nodes)
                if (HasCycle(root, new())) return true;
            return false;
        } // HasCycle
        /// <summary>
        /// Recursively checks if revisiting an already-visited node is reachable from the current node
        /// </summary>
        /// <param name="current">Node currently being visited</param>
        /// <param name="visited">Set of values already visited along this path</param>
        /// <returns>True if a previously visited value is reached again</returns>
        private static bool HasCycle(CookieGraphNode<GraphType> current, CookieNodeList<GraphType> visited)
        {
            if (current == null) return false;
            if (visited.Contains(current.Value)) return true;
            visited.Add(current.Value);

            foreach (var neighbour in current.Neighbours)
                if (HasCycle(neighbour, visited.Copy()))
                    return true;
            return false;
        } // HasCycle


        /// <summary>
        /// Checks if the graph is a tree: no cycles, and every vertex reachable from some root
        /// </summary>
        /// <returns>True if the graph forms a tree</returns>
        public bool IsTree()
        {
            if (HasCycle()) return false;
            foreach (CookieGraphNode<GraphType> root in this.nodes)
                if (this.DepthFirstSearch(root.Value).Count == VertexCount()) 
                    return true;
            return false;
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
        /// <summary>
        /// Returns an enumerator over all vertex values in the graph (enables foreach)
        /// </summary>
        /// <returns>An enumerator over the graph's vertex values</returns>
        public IEnumerator<GraphType> GetEnumerator()
        { 
            foreach (var node in this.nodes)
                yield return node.Value;
        } // GetEnumerator
        /// <summary>
        /// Non-generic IEnumerable.GetEnumerator implementation, forwards to the generic version
        /// </summary>
        /// <returns>An enumerator over the graph's vertex values</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    } // CookieGraph
}
