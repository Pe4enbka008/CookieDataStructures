"""
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
"""
from collections.abc import Iterable
from CookieCuteStack import CookieQueue


class CookieBinNode(Iterable):
    def __init__(self, value, left_node=None, right_node=None):
        """Constructor for CookieNode
        :param value: value of the node
        :type value: Any
        :param left_node: left node
        :type left_node: CookieBinNode | None
        :param right_node: right node
        :type right_node: CookieBinNode | None
        :return: nothing
        :rtype: None"""
        self.value = value
        self.left_node = left_node
        self.right_node = right_node

    def __iter__(self):
        """Returns elements one by one for 'for' loops
        :return: value of the nodes
        :rtype: Any"""
        walk_node = self
        while walk_node:
            yield walk_node.value
            walk_node = walk_node.right_node

    def __str__(self):
        """Returns string value of the nodes
        :return: string value of the nodes
        :rtype: str"""
        return str(self.value)

    # end


class CookieHoldingLineHelper:
    """2_linked_list are CookieBinNode, so CookieHoldingLineHelper is fully static class"""

    # prints:

    @staticmethod
    def print_2_linked_list_recursive(bin_node, start_printing):
        """Read full line from left to right
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :param start_printing: if to start reading
        :type start_printing: bool
        :return: string of the line
        :rtype: str"""
        return_value = ''
        if bin_node is None:
            pass
        elif start_printing:
            return_value = str(bin_node) + " -> " + CookieHoldingLineHelper.print_2_linked_list_recursive(bin_node.right_node, True)
        else:
            return_value = CookieHoldingLineHelper.print_2_linked_list_recursive(bin_node.left_node, bin_node.left_node.left_node is None)
        return return_value

    @staticmethod
    def print_2_linked_list(bin_node):
        """Returns full line from left to right
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :return: string of the line
        :rtype: str"""
        return 'null -> ' + CookieHoldingLineHelper.print_2_linked_list_recursive(bin_node, bin_node.left_node is None) + "null"

    @staticmethod
    def print_2_linked_list_right(bin_node):
        """Returns line from right of the current node
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :return: string of the line
        :rtype: str"""
        return CookieHoldingLineHelper.print_2_linked_list_recursive(bin_node, True) + "null"

    @staticmethod
    def print_2_linked_list_left_recursive(bin_node):
        """Read line from left of the current node
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :return: string of the line
        :rtype: str"""
        if bin_node is not None:
            return CookieHoldingLineHelper.print_2_linked_list_left_recursive(bin_node.left_node) + " <- " + str(bin_node)
        return ''

    @staticmethod
    def print_2_linked_list_left(bin_node):
        """Returns line from left of the current node
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :return: string of the line
        :rtype: str"""
        return "null" + CookieHoldingLineHelper.print_2_linked_list_left_recursive(bin_node)

    # adds:

    @staticmethod
    def add_right(bin_node, value):
        """Adds a value to right of the node
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :param value: value to add
        :type value: Any
        :return: string of the line
        :rtype: str"""
        if bin_node is not None:
            temp = value if isinstance(value, CookieBinNode) else CookieBinNode(value)

            temp.right_node = bin_node.right_node
            bin_node.right_node = temp
            temp.left_node = bin_node
            if temp.right_node is not None:
                temp.right_node.left_node = temp

    @staticmethod
    def add_left(bin_node, value):
        """Adds a value to left of the node
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :param value: value to add
        :type value: CookieBinNode | Any
        :return: string of the line
        :rtype: str"""
        if bin_node is not None:
            temp = value if isinstance(value, CookieBinNode) else CookieBinNode(value)

            temp.left_node = bin_node.left_node
            bin_node.left_node = temp
            temp.right_node = bin_node
            if temp.left_node is not None:
                temp.left_node.right_node = temp

    @staticmethod
    def add_to_head(bin_node, value):
        """Adds a value to most left of the node line
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :param value: value to add
        :type value: CookieBinNode | Any
        :return: string of the line
        :rtype: str"""
        if bin_node is None:
            return
        left = CookieHoldingLineHelper.get_leftest(bin_node)
        new_node = value if isinstance(value, CookieBinNode) else CookieBinNode(value)
        left.left_node = new_node
        new_node.right_node = left

    @staticmethod
    def add_to_tail(bin_node, value):
        """Adds a value to most right of the node line
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :param value: value to add
        :type value: CookieBinNode | Any
        :return: string of the line
        :rtype: str"""
        if bin_node is None:
            return
        right = CookieHoldingLineHelper.get_rightest(bin_node)
        new_node = value if isinstance(value, CookieBinNode) else CookieBinNode(value)
        right.right_node = new_node
        new_node.left_node = right

    # getters:

    @staticmethod
    def get_leftest(bin_node):
        """Adds a value to most left of the node line
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :return: leftest binary node
        :rtype: CookieBinNode"""
        if bin_node is None:
            return None
        if bin_node.left_node is None:
            return bin_node
        return CookieHoldingLineHelper.get_leftest(bin_node.left_node)

    @staticmethod
    def get_leftest_value(bin_node):
        """Adds a value to most left of the node line
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :return: leftest binary node value
        :rtype: Any"""
        return CookieHoldingLineHelper.get_leftest(bin_node).value()

    @staticmethod
    def get_rightest(bin_node):
        """Adds a value to most left of the node line
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :return: rightest binary node
        :rtype: CookieBinNode"""
        if bin_node is None:
            return None
        if bin_node.right_node is None:
            return bin_node
        return CookieHoldingLineHelper.get_rightest(bin_node.right_node)

    @staticmethod
    def get_rightest_value(bin_node):
        """Adds a value to most left of the node line
        :param bin_node: binary node
        :type bin_node: CookieBinNode
        :return: rightest binary node value
        :rtype: Any"""
        return CookieHoldingLineHelper.get_rightest(bin_node).value

    # end


class CookieTree:
    def __init__(self, root=None):
        """Constructor for CookieTree
        :param root: root value
        :type root: CookieBinNode | None | Any
        :return: nothing
        :rtype: None"""
        if root is None:
            self.root = None
            return
        self.root = root if isinstance(root, CookieBinNode) else CookieBinNode(root)

    # insert:

    def insert(self, value):
        """Insert a value into the tree
        :param value: value to insert
        :type value: Any
        :return: nothing
        :rtype: None"""
        self.root = self.insert_recursive(self.root, value)

    @staticmethod
    def insert_recursive(root, value):
        """Static insert a value into the tree
        :param root: binary node to insert into
        :type root:  CookieBinNode | None
        :param value: value to insert
        :type value: Any
        :return: new inserted binary node
        :rtype: CookieBinNode"""
        if root is None:
            return CookieBinNode(value)
        if root.value > value:
            root.left_node = CookieTree.insert_recursive(root.left_node, value)
        elif root.value < value:
            root.right_node = CookieTree.insert_recursive(root.right_node, value)
        return root

    # remove:

    def remove(self, value):
        """Remove a value into the tree
        :param value: value to remove
        :type value: Any
        :return: nothing
        :rtype: None"""
        self.root = self.remove_recursive(self.root, value)

    @staticmethod
    def remove_recursive(root, value):
        """Static remove a value into the tree
        :param root: binary node to remove from
        :type root:  CookieBinNode | None
        :param value: value to remove
        :type value: Any
        :return: new removed binary node
        :rtype: CookieBinNode"""
        if root is None:
            return root

        if root.value > value:
            root.left_node = CookieTree.remove_recursive(root.left_node, value)
        elif root.value < value:
            root.right_node = CookieTree.remove_recursive(root.right_node, value)
        else:
            right_branch = CookieTree(root.right_node)
            left_branch = CookieTree(root.left_node)

            for tree_value in right_branch:
                left_branch.insert(tree_value)
            root = left_branch.root

        return root

    # funzies:

    def search(self, value):
        """Find a value in the tree
        :param value: value to search for
        :type value: Any
        :return: if requested value found
        :rtype: bool"""
        return self.search_recursive(self.root, value)

    @staticmethod
    def search_recursive(root, value):
        """Static find a value in the tree
        :param root: binary node to search
        :type root:  CookieBinNode | None
        :param value: value to search for
        :type value: Any
        :return: if requested value found
        :rtype: bool"""
        if root is None:
            return False
        if root.value == value:
            return True
        found = CookieTree.search_recursive(root.right_node, value)
        if root.value > value:
            found = CookieTree.search_recursive(root.left_node, value)
        return found

    def length(self):
        """Return number of elements in the tree
        :return: number of values
        :rtype: int"""
        return self.count_elements_recursive(self.root)

    @staticmethod
    def count_elements_recursive(root):
        """Static count elements in a tree
        :param root: binary node to search
        :type root:  CookieBinNode | None
        :return: number of values
        :rtype: int"""
        if root is None:
            return 0
        return 1 + CookieTree.count_elements_recursive(root.left_node) + CookieTree.count_elements_recursive(root.right_node)

    def count(self, value):
        """Return number of specific elements in the tree
        :return: number of values
        :rtype: int"""
        return self.count_element_recursive(self.root, value)

    @staticmethod
    def count_element_recursive(root, value):
        """Static count of specific elements in a tree
        :param root: binary node to search
        :type root:  CookieBinNode | None
        :return: number of values
        :rtype: int"""
        if root is None:
            return 0
        add = 1 if root.value == value else 0
        return add + CookieTree.count_element_recursive(root.left_node, value) + CookieTree.count_element_recursive(root.right_node, value)

    def count_leaves(self):
        """Return number of leaves in a tree
        :return: number of values
        :rtype: int"""
        return self.count_leaves_recursive(self.root)

    @staticmethod
    def count_leaves_recursive(root):
        """Static count of leaves in a tree
        :param root: binary node to search
        :type root:  CookieBinNode | None
        :return: number of values
        :rtype: int"""
        if root is None:
            return 0
        add = 1 if root.left_node == root.right_node is None else 0
        return add + CookieTree.count_leaves_recursive(root.left_node) + CookieTree.count_leaves_recursive(root.right_node)

    def get_height(self):
        """Return number of height of the tree
        :return: number of values
        :rtype: int"""
        return self.get_height_recursive(self.root)

    @staticmethod
    def get_height_recursive(root):
        """Static count of height of a tree
        :param root: binary node to search
        :type root:  CookieBinNode | None
        :return: number of values
        :rtype: int"""
        if root is None:
            return 0
        count_left = 1 + CookieTree.get_height_recursive(root.left_node)
        count_right = 1 + CookieTree.get_height_recursive(root.right_node)
        return max(count_left, count_right)

    # tree types:

    def is_full(self):
        """Research if the tree is a full tree
        :return: if the tree is a full tree
        :rtype: bool"""
        return self.is_full_recursive(self.root)

    @staticmethod
    def is_full_recursive(root):
        """Static research if the tree is a full tree
        :param root: binary node to search
        :type root: CookieBinNode | None
        :return: if the tree is a full tree
        :rtype: bool"""
        if root is None:
            return True
        check = (
            (root.right_node is None or root.left_node is None)
            and
            not (root.right_node is None and root.left_node is None)
        )

        return not check and CookieTree.is_full_recursive(root.left_node) and CookieTree.is_full_recursive(root.right_node)

    def is_complete(self):
        """Research if the tree is a complete tree
        :return: if the tree is a full tree
        :rtype: bool"""
        return self.is_complete_recursive(self.root)

    @staticmethod
    def is_complete_recursive(root):
        """Static research if the tree is a full tree
        :param root: binary node to search
        :type root:  CookieBinNode | None
        :return: if the tree is a full tree
        :rtype: bool"""
        if root is None:
            return False

        check_list = CookieQueue(root)
        reached_empty = False
        while not check_list.is_empty():
            current = check_list.remove()
            if current.left_node is not None:
                if reached_empty:
                    return False
                check_list.insert(current.left_node)
            else:
                reached_empty = True

            if current.right_node is not None:
                if reached_empty:
                    return False
                check_list.insert(current.right_node)
            else:
                reached_empty = True

        return True

    def is_spanning_tree(self):
        return self.is_spanning_tree_recursive(self.root)

    @staticmethod
    def is_spanning_tree_recursive(root):
        """Returns the tree in 'pre-order' order
        :param root: left node
        :type root: CookieBinNode
        :return: string
        :rtype: str"""
        if root is None:
            return True

        if root.left_node is not None:
            if root.value < root.left_node.value:
                return False

        if root.right_node is not None:
            if root.value > root.right_node.value:
                return False

        return CookieTree.is_spanning_tree_recursive(root.left_node) and CookieTree.is_spanning_tree_recursive(root.right_node)

    # iteration:

    def __iter__(self):
        yield from self.in_order_yield(self.root)

    def in_order_yield(self, root):
        """Yields the tree in 'in-order' order
        :param root: left node
        :type root: CookieBinNode
        :return: nothing
        :rtype: None"""
        if root is None:
            return
        yield from self.in_order_yield(root.left_node)
        yield root.value
        yield from self.in_order_yield(root.right_node)

    # prints:

    def in_order_traversal(self):
        """Returns the tree in 'in-order' order
        :return: string
        :rtype: str"""
        return self.in_order_recursive(self.root)

    @staticmethod
    def in_order_recursive(root):
        """Returns the tree in 'in-order' order
        :param root: left node
        :type root: CookieBinNode
        :return: string
        :rtype: str"""
        return_value = ""
        if root is not None:
            return_value += CookieTree.in_order_recursive(root.left_node)
            return_value += str(root.value) + " "
            return_value += CookieTree.in_order_recursive(root.right_node)
        return return_value

    def pre_order_traversal(self):
        """Returns the tree in 'pre-order' order
        :return: string
        :rtype: str"""
        return self.pre_order_recursive(self.root)

    @staticmethod
    def pre_order_recursive(root):
        """Returns the tree in 'pre-order' order
        :param root: left node
        :type root: CookieBinNode
        :return: string
        :rtype: str"""
        return_value = ""
        if root is not None:
            return_value += str(root.value) + " "
            return_value += CookieTree.pre_order_recursive(root.left_node)
            return_value += CookieTree.pre_order_recursive(root.right_node)
        return return_value

    def post_order_traversal(self):
        """Returns the tree in 'post-order' order
        :return: string
        :rtype: str"""
        return self.post_order_recursive(self.root)

    @staticmethod
    def post_order_recursive(root):
        """Returns the tree in 'post-order' order
        :param root: left node
        :type root: CookieBinNode
        :return: string
        :rtype: str"""
        return_value = ""
        if root is not None:
            return_value += CookieTree.post_order_recursive(root.left_node)
            return_value += CookieTree.post_order_recursive(root.right_node)
            return_value += str(root.value) + " "
        return return_value

    # end

