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
    """
    :type nodes: CookieNode
    """
    def __init__(self, nodes=None):
        self.nodes = nodes

    def __str__(self, spliter=''):
        if not self.nodes:
            return '[]'

        return_value = '['
        walk_value = self.nodes
        while walk_value:
            return_value += walk_value
            walk_value = walk_value.GetNext()
            if walk_value:
                return_value += spliter

        return return_value + ']'

    # getters-setters:
    def is_empty(self):
        return not self.nodes

    def push(self, value):
        if self.is_empty():
            self.nodes = CookieNode(value)

    def pop(self):
        if self.is_empty():
            return None

        walk_value = self.nodes
        self.nodes = self.nodes.get_next()
        walk_value.set_next(None)
        return walk_value

    # end

