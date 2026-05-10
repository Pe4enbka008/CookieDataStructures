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


class CookieNode:
    def __init__(self, value, next_node=None):
        self.value = value
        self.next_node = next_node

    def __iter__(self):
        value_list = []
        walk_node = self
        while walk_node:
            value_list.append(walk_node.__str__())
            walk_node = walk_node.next_node
        return iter(value_list)

    def __str__(self):
        return self.value

    # getters-setters
    def get_value(self):
        return self.value

    def get_next(self):
        return self.next_node

    def set_value(self, value):
        self.value = value

    def set_next(self, next_node):
        self.next_node = next_node

    # end

