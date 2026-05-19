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


class CookieNode(Iterable):
    def __init__(self, value, next_node=None):
        """Constructor for CookieNode
        :param value: value of the node
        :type value: Any
        :param next_node: next node
        :type next_node: CookieNode | None
        :return: nothing
        :rtype: None"""
        self.value = value
        self.next_node = next_node

    def __iter__(self):
        """Returns elements one by one for 'for' loops
        :return: value of the nodes
        :rtype: Any"""
        walk_node = self
        while walk_node:
            yield walk_node.value
            walk_node = walk_node.next_node

    def __str__(self):
        """Returns string value of the nodes
        :return: string value of the nodes
        :rtype: str"""
        return str(self.value)

    # end

