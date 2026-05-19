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
from CookieNodeList import CookieNode


class CookieStack:
    def __init__(self, nodes=None):
        """Constructor for CookieStack
        :param nodes: start nodes
        :type nodes: CookieNode | None
        :return: nothing
        :rtype: None"""
        self.nodes = nodes

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
        :type value: Any
        :return: nothing
        :rtype: None"""
        self.nodes = CookieNode(value, self.nodes)

    def pop(self):
        """LIFO (LAST IN ; FIRST OUT) - gets the front value ; could be null
        :return: value to pop
        :rtype: Any"""
        if self.is_empty():
            return None

        node = self.nodes
        self.nodes = self.nodes.get_next()
        node.set_next(None)
        return node.get_value()

    def get_top(self):
        """Returns the top value of the stack
        :return: the top value of the stack
        :rtype: Any"""
        return None if self.nodes is None else self.nodes.get_next()

    def copy(self):
        """Returns a copy of the stack
        :return: the copy of the stack
        :rtype: CookieNode | None"""
        if not self.nodes:
            return None

        return_value = CookieNode(self.nodes.value)
        current = return_value
        walk_value = self.nodes

        while walk_value:
            current.set_next(CookieNode(walk_value.value))
            current = current.get_next()
            walk_value = walk_value.get_next()
        return return_value

    def clear(self):
        """Clears the stack
        :return: nothing
        :rtype: None"""
        self.nodes = None

    # end


class CookieQueue:
    def __init__(self, nodes=None):
        """Constructor for CookieQueue
        :param nodes: start nodes
        :type nodes: CookieNode | None
        :return: nothing
        :rtype: None"""
        self.nodes = nodes
        self.end = nodes
        if self.end is None:
            while self.end.get_next() is not None:
                self.end = self.end.get_next()  # get to the last

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
        :type value: Any
        :return: nothing
        :rtype: None"""
        if self.is_empty():
            self.nodes = CookieNode(value)
            self.end = self.nodes
            return
        self.end.set_next(CookieNode(value))
        self.end = self.end.get_next()

    def remove(self):
        """LFIFO (FIRST IN ; FIRST OUT) - gets the front value ; could be null
        :return: value to pop
        :rtype: Any"""
        if self.is_empty():
            return None

        node = self.nodes
        if node.get_next() is None:
            self.end = None
        self.nodes = self.nodes.get_next()
        node.set_next(None)
        return node.get_value()

    def get_top(self):
        """Returns the top value of the stack
        :return: the top value of the stack
        :rtype: Any"""
        return None if self.nodes is None else self.nodes.get_next()

    def copy(self):
        """Returns a copy of the stack
        :return: the copy of the stack
        :rtype: CookieNode | None"""
        if not self.nodes:
            return None

        return_value = CookieNode(self.nodes.value)
        current = return_value
        walk_value = self.nodes

        while walk_value:
            current.set_next(CookieNode(walk_value.value))
            current = current.get_next()
            walk_value = walk_value.get_next()
        return return_value

    def clear(self):
        """Clears the stack
        :return: nothing
        :rtype: None"""
        self.nodes = None
        self.end = None

    # end

