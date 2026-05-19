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
from CookieNode import CookieNode


class CookieStack:
    def __init__(self, value=None):
        """Constructor for CookieStack
        :param value: start nodes
        :type value: CookieNode | CookieStack | Any
        :return: nothing
        :rtype: None"""
        self.nodes = None
        if isinstance(value, CookieNode):
            self.nodes = value
        elif isinstance(value, CookieStack):
            for node in value.nodes:
                self.push(node.get_value())
        elif value is not None:
            self.nodes = CookieNode(value)

    def __str__(self):
        """Returns string value of the nodes
        :return: string value of the nodes
        :rtype: str"""
        return self.to_str(', ')

    def to_str(self, spliter):
        """Returns string value of the nodes
        :param spliter: spliter to put in between of the values
        :type spliter: str
        :return: string value of the nodes
        :rtype: str"""
        if not self.nodes:
            return '[]'

        return_value = '['
        walk_value = self.nodes
        while walk_value:
            return_value += str(walk_value)
            walk_value = walk_value.get_next()
            if walk_value:
                return_value += spliter

        return return_value + ']'

    def is_empty(self):
        """Returns true if the stack is empty
        :return: true if the stack is empty
        :rtype: bool"""
        return not self.nodes

    # getters-setters:

    def push(self, value):
        """FILO (FIRST IN ; LAST OUT) - puts the value in front
        :param value: value to push
        :type value: CookieNode | CookieStack | Any
        :return: nothing
        :rtype: None"""
        if isinstance(value, CookieNode):
            value.next_node = self.nodes
            self.nodes = value
        elif isinstance(value, CookieStack):
            for node in value.nodes:
                self.push(node.get_value())
        else:
            self.nodes = CookieNode(value, self.nodes)

    def pop(self):
        """LIFO (LAST IN ; FIRST OUT) - gets the front value ; could be null
        :return: value to pop
        :rtype: Any"""
        if self.is_empty():
            return None

        node = self.nodes
        self.nodes = self.nodes.next_node
        node.next_node = None
        return node.value

    def get_top(self):
        """Returns the top value of the stack
        :return: the top value of the stack
        :rtype: Any"""
        return None if self.nodes is None else self.nodes.value

    def copy(self):
        """Returns a copy of the stack
        :return: the copy of the stack
        :rtype: CookieStack"""
        if not self.nodes:
            return None

        return_value = CookieNode(self.nodes.value)
        current = return_value
        walk_value = self.nodes.next_node

        while walk_value:
            current.set_next(CookieNode(walk_value.value))
            current = current.get_next()
            walk_value = walk_value.get_next()

        return CookieStack(return_value)

    def clear(self):
        """Clears the stack
        :return: nothing
        :rtype: None"""
        self.nodes = None

    # length:
    def __len__(self):
        """Returns length of the stack
        :return: length
        :rtype: int"""
        if self.is_empty():
            return 0
        return sum(1 for _ in self.nodes)

    # reverse:
    def __reversed__(self):
        """reverses a stack
        :return: reversed stack
        :rtype: CookieStack"""
        if self is None:
            return None
        if len(self) <= 1:
            return self.copy()

        current = self.nodes
        rev = CookieStack()
        while current:
            rev.push(current.get_value())
            current = current.get_next()
        return rev

    # end


class CookieQueue:
    def __init__(self, value=None):
        """Constructor for CookieQueue
        :param value: start nodes
        :type value: CookieNode | CookieQueue | Any
        :return: nothing
        :rtype: None"""
        self.nodes = None
        self.end = None
        if isinstance(value, CookieNode):
            self.nodes = value
            self.end = self.nodes
        elif isinstance(value, CookieQueue):
            for node in value.nodes:
                new_node = CookieNode(node.get_value())
                self.insert(new_node)
                self.end = new_node
        elif value is not None:
            self.nodes = CookieNode(value)
            self.end = self.nodes

    def __str__(self):
        """Returns string value of the nodes
        :return: string value of the nodes
        :rtype: str"""
        return self.to_str(', ')

    def to_str(self, spliter):
        """Returns string value of the nodes
        :param spliter: spliter to put in between of the values
        :type spliter: str
        :return: string value of the nodes
        :rtype: str"""
        if not self.nodes:
            return '[]'

        return_value = '['
        walk_value = self.nodes
        while walk_value:
            return_value += str(walk_value)
            walk_value = walk_value.get_next()
            if walk_value:
                return_value += spliter

        return return_value + ']'

    def is_empty(self):
        """Returns true if the stack is empty
        :return: true if the stack is empty
        :rtype: bool"""
        return not self.nodes and not self.end

    # getters-setters:

    def insert(self, value):
        """FIFO (FIRST IN ; FIRST OUT) - puts the value in front
        :param value: value to push
        :type value: CookieNode | CookieQueue | Any
        :return: nothing
        :rtype: None"""
        # inserting another queue
        if isinstance(value, CookieQueue):
            current = value.nodes

            while current is not None:
                self.insert(current.value)  # fun recursion
                current = current.next_node
            return

        new_node = value if isinstance(value, CookieNode) else CookieNode(value)

        if self.is_empty():
            self.nodes = new_node
            self.end = new_node
            return

        self.end.next_node = new_node
        self.end = new_node

    def remove(self):
        """LFIFO (FIRST IN ; FIRST OUT) - gets the front value ; could be null
        :return: value to pop
        :rtype: Any"""
        if self.is_empty():
            return None

        node = self.nodes
        if node.next_node is None:
            self.end = None
        self.nodes = self.nodes.next_node
        node.next_node = None
        return node.value

    def get_top(self):
        """Returns the top value of the stack
        :return: the top value of the stack
        :rtype: Any"""
        return None if self.nodes is None else self.nodes.value

    def copy(self):
        """Returns a copy of the stack
        :return: the copy of the stack
        :rtype: CookieQueue"""
        if not self.nodes:
            return None

        return_value = CookieNode(self.nodes.value)
        current = return_value
        walk_value = self.nodes.next_node

        while walk_value:
            current.set_next(CookieNode(walk_value.value))
            current = current.get_next()
            walk_value = walk_value.get_next()
        return CookieQueue(return_value)

    def clear(self):
        """Clears the stack
        :return: nothing
        :rtype: None"""
        self.nodes = None
        self.end = None

    # length:
    def __len__(self):
        """Returns length of the queue
        :return: length
        :rtype: int"""
        if self.is_empty():
            return 0
        return sum(1 for _ in self.nodes)

    # reverse:
    def __reversed__(self):
        """reverses a queue
        :return: reversed queue
        :rtype: CookieQueue"""
        if self is None:
            return None
        if len(self) <= 1:
            return self.copy()

        current = self.nodes
        rev_stack = CookieStack()
        while current:
            rev_stack.push(current.get_value())
            current = current.get_next()

        rev = CookieQueue()
        while not rev_stack.is_empty():
            rev.insert(rev_stack.pop())
        return rev

    # end

